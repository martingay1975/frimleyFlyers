"""Render the two monthly league table images."""
import csv
import os
from pathlib import Path

import matplotlib.pyplot as plt
from matplotlib.patches import Rectangle

ROOT = Path(r"R:/git/frimleyFlyers/site/res/json")

HEADER_BG = "#4472C4"
HEADER_FG = "white"
ALT_BG = "#E2EFDA"
WHITE = "white"
BORDER = "#B4B4B4"


def render_table(headers, rows, out_path, col_widths, bold_cols, header_rows=None,
                 col_align=None, scale=1.0):
    """Render a table to a JPG.

    headers: list of header rows. Each header row is a list of (text, colspan) tuples.
    rows: list of data row lists.
    col_widths: relative column widths (sum normalized to 1).
    bold_cols: set of column indexes to render bold.
    col_align: list of 'left'|'center'|'right' per column. Defaults to 'left' for col 1, else 'center'.
    """
    n_cols = len(rows[0])
    if col_align is None:
        col_align = ["center"] * n_cols
        if n_cols >= 2:
            col_align[1] = "left"

    total_w = sum(col_widths)
    norm_w = [w / total_w for w in col_widths]
    col_x = [0.0]
    for w in norm_w:
        col_x.append(col_x[-1] + w)

    n_header_rows = len(headers)
    n_data_rows = len(rows)
    row_h = 0.04
    fig_w = 11 * scale
    fig_h = (n_header_rows + n_data_rows) * row_h * 14 * scale

    fig, ax = plt.subplots(figsize=(fig_w, fig_h))
    ax.set_xlim(0, 1)
    ax.set_ylim(0, n_header_rows + n_data_rows)
    ax.axis("off")
    ax.invert_yaxis()

    # Header
    for hi, header_row in enumerate(headers):
        x_cursor = 0
        col_index = 0
        for text, span in header_row:
            x0 = col_x[col_index]
            x1 = col_x[col_index + span]
            y0 = hi
            ax.add_patch(Rectangle((x0, y0), x1 - x0, 1, facecolor=HEADER_BG,
                                   edgecolor=BORDER, linewidth=0.6))
            ax.text((x0 + x1) / 2, y0 + 0.5, text, ha="center", va="center",
                    color=HEADER_FG, fontsize=11 * scale, fontweight="bold")
            col_index += span

    # Data rows
    for ri, row in enumerate(rows):
        y0 = n_header_rows + ri
        bg = ALT_BG if ri % 2 == 1 else WHITE
        for ci, val in enumerate(row):
            x0 = col_x[ci]
            x1 = col_x[ci + 1]
            ax.add_patch(Rectangle((x0, y0), x1 - x0, 1, facecolor=bg,
                                   edgecolor=BORDER, linewidth=0.4))
            align = col_align[ci]
            if align == "left":
                tx = x0 + 0.005
                ha = "left"
            elif align == "right":
                tx = x1 - 0.005
                ha = "right"
            else:
                tx = (x0 + x1) / 2
                ha = "center"
            weight = "bold" if ci in bold_cols else "normal"
            ax.text(tx, y0 + 0.5, str(val), ha=ha, va="center",
                    fontsize=10 * scale, fontweight=weight, color="black")

    plt.subplots_adjust(left=0, right=1, top=1, bottom=0)
    fig.savefig(out_path, dpi=150, bbox_inches="tight", pad_inches=0.05,
                facecolor="white")
    plt.close(fig)
    print(f"Wrote {out_path}")


def fmt_pct(s):
    s = s.strip()
    return s


def render_monthly():
    src = ROOT / "04-parkrun Apr 25.csv"
    rows = []
    with open(src, newline="", encoding="utf-8") as f:
        reader = csv.DictReader(f)
        for r in reader:
            rows.append([
                r["Position"].strip(),
                r["Name"].strip(),
                r["Location"].strip(),
                r["Time"].strip(),
                fmt_pct(r["%"]),
                r["Pts"].strip(),
            ])

    headers = [[("Position", 1), ("Name", 1), ("Location", 1), ("Time", 1),
                ("%", 1), ("Pts", 1)]]
    col_widths = [0.09, 0.30, 0.28, 0.11, 0.12, 0.10]
    bold_cols = {0, 1, 5}
    col_align = ["center", "left", "left", "center", "right", "center"]
    out = ROOT / "last-month-only-formatted.jpg"
    render_table(headers, rows, out, col_widths, bold_cols, col_align=col_align)


def render_overall():
    src = ROOT / "raceData2026.json-spotlight.csv"
    with open(src, newline="", encoding="utf-8") as f:
        all_rows = list(csv.reader(f))

    section_row = all_rows[0]
    col_row = all_rows[1]
    data = all_rows[2:]

    # Determine month columns up to current month (April = index 4 onward,
    # since columns are: Name, Pts, Home Pts, Tourist Pts, Prev Best, then months)
    # Section row: ['', 'Overall', '', '', '', 'January', 'February', 'March', 'April']
    month_names = []
    for i in range(5, len(section_row)):
        if section_row[i].strip():
            month_names.append(section_row[i].strip())

    # Filter month columns to those with at least one non-empty value
    month_cols = list(range(5, 5 + len(month_names)))
    used_months = []
    used_indexes = []
    for mi, ci in enumerate(month_cols):
        if any(r[ci].strip() for r in data):
            used_months.append(month_names[mi])
            used_indexes.append(ci)

    # Build display rows with position
    display_rows = []
    for pos, r in enumerate(data, start=1):
        row = [
            str(pos),
            r[0].strip(),                 # Name
            r[1].strip(),                 # Overall Pts
            r[2].strip(),                 # Home Pts
            r[3].strip(),                 # Tourist Pts
            r[4].strip(),                 # Prev Best (rename to 2025 Best)
        ]
        for ci in used_indexes:
            v = r[ci].strip()
            row.append(v if v else "")
        display_rows.append(row)

    n_month_cols = len(used_months)

    # Two header rows
    header_row1 = [("", 1), ("Overall", 4), ("", 1)]
    for m in used_months:
        header_row1.append((m, 1))
    # Actually we want 2025 Best to also have a blank section header above it
    # Let's restructure: position(blank), Overall spans 4 (Name, Pts, Home Pts, Tourist Pts)...
    # but spec says: "blank | Overall spanning cols 2-5 | month names"
    # cols: 1 position, 2 Name, 3 Pts, 4 Home Pts, 5 Tourist Pts, 6 2025 Best, 7+ months
    # "Overall" spans cols 2-5 (Name, Pts, Home Pts, Tourist Pts)? Or 2-5 means 4 cols starting at 2?
    # Re-read: "blank | "Overall" spanning cols 2-5 | month names" — yes Name through Tourist Pts.
    # 2025 Best has its own (blank) above it.
    header_row1 = [("", 1), ("Overall", 4), ("", 1)]
    for m in used_months:
        header_row1.append((m, 1))

    header_row2 = [
        ("", 1),
        ("Name", 1),
        ("Pts", 1),
        ("Home Pts", 1),
        ("Tourist Pts", 1),
        ("2025 Best", 1),
    ]
    for _ in used_months:
        header_row2.append(("Pts", 1))

    headers = [header_row1, header_row2]

    # Column widths
    col_widths = [0.05, 0.20, 0.07, 0.09, 0.10, 0.10]
    month_w = (1 - sum(col_widths)) / max(n_month_cols, 1)
    for _ in range(n_month_cols):
        col_widths.append(month_w)

    n_total_cols = 6 + n_month_cols
    bold_cols = {0, 1, 2}
    col_align = ["center", "left", "center", "center", "center", "center"]
    for _ in range(n_month_cols):
        col_align.append("center")

    out = ROOT / "last-month-all-formatted.jpg"
    render_table(headers, display_rows, out, col_widths, bold_cols,
                 col_align=col_align)


if __name__ == "__main__":
    render_monthly()
    render_overall()
