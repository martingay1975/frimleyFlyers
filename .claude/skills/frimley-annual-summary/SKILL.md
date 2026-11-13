---
name: frimley-annual-summary
description: Use when Martin asks for the Frimley Flyers annual summary, a year-in-review, end-of-year parkrun round-up, a positive one-liner per runner about their year, races mentioned in the Flyers Messenger chat, or the End of Season presentation-evening Google Form.
---

# Frimley Flyers Annual Summary (and End of Season form)

The yearly workflow, as run for 2026: one upbeat one-liner per Flyer about their year
(parkrun stats + other races from the Messenger chat), published in the End of Season Google
Form used for Flyer of the Year voting and the presentation evening.

Working directory: `R:\git\frimleyFlyers`. Athlete data: `site/res/json/athletes/{Name}.json`.
Supporting files in this folder:

| File | Purpose |
|---|---|
| `annual_stats.py` | per-athlete stats for the year |
| `messenger_trawl.md` | how to pull the year's races from the Flyers Chat Group |
| `google_form.md` | form template link, structure, theme, editing techniques |
| `crop_header.py`, `serve_image.py` | header image crop + upload helper |
| `assets/` | club trainers photo and the cropped form header |

## Step 1: Refresh the parkrun data

Run `run-monthly-stats.bat` (see the `frimley-monthly-report` skill) so every athlete json is
current. Notes from 2026:
- parkrun can return **HTTP 405** if hit too fast; the block lifted after ~10-15 minutes.
  Wait and retry; don't hammer it.
- To include people not on the active roster (e.g. Bob Turner, Rufus Frew, Tom Churchill,
  Ben Gay in 2026), uncomment them in `FF.DataEntry/Utils/AthletesManager.cs` for the run,
  then put the roster back. Ask Martin who to include.
- To refresh just one athlete's json (e.g. someone whose file is stale), use
  `dotnet run --project FF.Console/FF.Console.csproj -c Release -- --only "Ben Gay"`.
  It fetches only that person and leaves the shared stats/league/season files untouched.
  They still need to be uncommented in the roster for the run.
- A file whose latest run is weeks older than everyone else's wasn't refreshed: say so rather
  than summarising stale data.

## Step 2: Compute the stats

```
python -I .claude/skills/frimley-annual-summary/annual_stats.py [athletesDir] [year]
```

Defaults: the athletes folder and the **current** year (pass the year explicitly if running
in January for the previous year). Per athlete it prints: quickest time of the year (venue,
date), last year's best, "quickest since" year, all-time PB; parkrun count vs last year and
"most since"; tourist runs vs last year; new venues; milestones reached. Last line: the date
the data runs to.

Definitions the script applies:
- **Tourist run**: any run away from Frimley Lodge. Exception: **Chris Peddle's home is
  Littlehampton Prom** (`HOME_OVERRIDES` in the script).
- **Quickest since YYYY**: YYYY is the most recent earlier year with a time at least as fast.
  "ALL-TIME PB" means nothing earlier was as fast.
- **Milestones**: 50, 100, 250, 500, 600, 700, 800, 900, 1000, for total parkruns and for
  Frimley Lodge runs, where the milestone run itself was this year.
- Junior parkruns are excluded; the json `pb` flag is ignored (it's set on every run).

## Step 3: Other races from the Messenger chat

Follow `messenger_trawl.md`: scroll the Flyers Chat Group back to 1 January with the Chrome
tools, filter for races, and read each race day to see who ran. Credit only confirmed runs;
flag anything uncertain.

## Step 4: Write the one-liners

One bullet per athlete with runs this year, alphabetical, name in bold:
- **Always include their quickest time of the year** (with venue).
- Pick the most positive angles: all-time PB / quickest since YYYY, more parkruns or most
  since YYYY / most ever, more tourist runs, notable new venues; landmarks like 200/300/400
  parkruns or 100 venues are fine as extra positives.
- **Weave milestones and races into the same line**, bold, with dates, e.g.
  "Celebrated her **600th parkrun** (9 May) and ran the **London Marathon** (26 Apr)…".
  No separate milestones or races section.
- Upbeat only: never criticise a slower year. If nothing improved, celebrate consistency,
  venues, or a time close to their best.
- British English, concise. Don't leave working markers (like "†") in the final text.

After the list, briefly note: the date the data runs to, anyone skipped, uncertain chat
attributions, and data that looks wrong and wasn't used (2026: David Peddle's 2025 best of
18:51 looked like an error).

## Step 5: The End of Season Google Form

Follow `google_form.md`: copy last year's form, update the year and event details, and put
the one-liners in the **"Some achievements by you all in YYYY"** Title-and-description block
at the **bottom of page 1** (keep the form a single page). Confirm with Martin before
publishing, and check the respondent view afterwards.

## Common mistakes

- Counting Chris Peddle's Littlehampton runs as tourist runs.
- Reading "quickest since 2024" as "slower than 2024": it means the fastest since 2024.
- Putting the achievements in a separate form section: it becomes page 2, hidden until
  respondents answer the required questions.
- Setting `textContent` on a Forms field: it doesn't save; use `execCommand('insertText')`.
