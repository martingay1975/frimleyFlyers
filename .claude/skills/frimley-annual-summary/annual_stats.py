"""Per-athlete annual parkrun stats for the Frimley Flyers annual summary.

Usage: python annual_stats.py [athletesDir] [year]
Defaults: R:/git/frimleyFlyers/site/res/json/athletes, current year.
"""
import datetime
import glob
import json
import os
import sys
from collections import Counter

sys.stdout.reconfigure(encoding="utf-8")

ATHLETES_DIR = sys.argv[1] if len(sys.argv) > 1 else r"R:/git/frimleyFlyers/site/res/json/athletes"
YEAR = str(sys.argv[2] if len(sys.argv) > 2 else datetime.date.today().year)
MILESTONES = [50, 100, 250, 500, 600, 700, 800, 900, 1000]
FRIMLEY_LODGE = "Frimley Lodge"
# Runs at an athlete's home parkrun are not tourist runs. Everyone is Frimley Lodge except:
HOME_OVERRIDES = {"Chris Peddle": "Littlehampton Prom"}


def secs(t):
    h, m, s = (int(x) for x in t.split(":"))
    return h * 3600 + m * 60 + s


def fmt(s):
    return f"{s // 60}:{s % 60:02d}"


def milestones_in_year(sorted_runs):
    """Milestones whose Nth run fell in YEAR, e.g. ['600th parkrun (2026-05-02)']."""
    return [
        f"{m} ({sorted_runs[m - 1]['date'][:10]})"
        for m in MILESTONES
        if len(sorted_runs) >= m and sorted_runs[m - 1]["date"][:4] == YEAR
    ]


latest_run_overall = ""
for path in sorted(glob.glob(os.path.join(ATHLETES_DIR, "*.json"))):
    data = json.load(open(path, encoding="utf-8"))
    runs = sorted(
        (r for r in data["parkrunRunList"] if "junior" not in r["event"].lower()),
        key=lambda r: r["date"])
    name = data["name"]
    if not runs:
        continue
    latest_run_overall = max(latest_run_overall, runs[-1]["date"][:10])
    this = [r for r in runs if r["date"][:4] == YEAR]
    prev = [r for r in runs if r["date"][:4] < YEAR]
    if not this:
        print(f"SKIP {name}: no {YEAR} runs (last run {runs[-1]['date'][:10]})")
        continue

    home = HOME_OVERRIDES.get(name, FRIMLEY_LODGE)
    by_year = Counter(r["date"][:4] for r in runs)
    last_year = str(int(YEAR) - 1)

    # Quickest of the year, and the most recent earlier year with something at least as fast.
    best = min(this, key=lambda r: secs(r["raceTime"]))
    best_s = secs(best["raceTime"])
    best_by_year = {}
    for r in prev:
        y, s = r["date"][:4], secs(r["raceTime"])
        best_by_year[y] = min(s, best_by_year.get(y, s))
    faster_years = [y for y, s in best_by_year.items() if s <= best_s]
    quickest_since = max(faster_years) if faster_years else "ALL-TIME PB"
    all_time_pb = min(secs(r["raceTime"]) for r in runs)

    count = len(this)
    most_since = max((y for y, c in by_year.items() if y < YEAR and c >= count), default="MOST EVER")
    tourist = [r for r in this if r["event"] != home]
    tourist_last = [r for r in runs if r["date"][:4] == last_year and r["event"] != home]
    new_venues = sorted({r["event"] for r in this} - {r["event"] for r in prev})
    venues_total = len({r["event"] for r in runs})
    fl_runs = [r for r in runs if r["event"] == FRIMLEY_LODGE]

    print(f"--- {name}")
    print(f"  quickest {YEAR}: {fmt(best_s)} at {best['event']} on {best['date'][:10]}"
          f" | {last_year} best: {fmt(best_by_year[last_year]) if last_year in best_by_year else '-'}"
          f" | quickest since: {quickest_since} | all-time PB: {fmt(all_time_pb)}")
    print(f"  parkruns {YEAR}: {count} ({last_year}: {by_year.get(last_year, 0)}, most since: {most_since})"
          f" | career total: {len(runs)} | Frimley Lodge total: {len(fl_runs)}")
    print(f"  tourist {YEAR}: {len(tourist)} ({last_year}: {len(tourist_last)}) | home: {home}"
          f" | new venues: {len(new_venues)} (career venues: {venues_total}, before {YEAR}: {venues_total - len(new_venues)})"
          f"{': ' + ', '.join(new_venues) if new_venues else ''}")
    print(f"  milestones {YEAR}: parkrun total {milestones_in_year(runs) or '-'}"
          f" | Frimley Lodge {milestones_in_year(fl_runs) or '-'}")

print(f"\nData runs to {latest_run_overall}")
