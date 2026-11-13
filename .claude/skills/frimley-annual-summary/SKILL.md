---
name: frimley-annual-summary
description: Use when Martin asks for the Frimley Flyers annual summary, a year-in-review, end-of-year parkrun round-up, or a positive one-liner per runner about their parkrun year (times, number of parkruns, tourist runs, milestones).
---

# Frimley Flyers Annual Summary

One upbeat one-liner per athlete about their parkrun year, compared with previous years.

Working directory: `R:\git\frimleyFlyers`. Athlete data: `site/res/json/athletes/{Name}.json`.

## Step 1: Make sure the data is fresh

The stats are only as current as the athlete json files. If they haven't been refreshed
recently, run `run-monthly-stats.bat` first (see the `frimley-monthly-report` skill).
Any athlete whose file wasn't refreshed (no recent runs while others have them) — say so
rather than summarising stale data.

## Step 2: Compute the stats

```
python -I .claude/skills/frimley-annual-summary/annual_stats.py
```

Optional args: `[athletesDir] [year]` (defaults: the athletes folder, current year).
Per athlete it prints: quickest time of the year (venue, date), last year's best, "quickest
since" year, all-time PB; parkrun count vs last year and "most since"; tourist runs vs last
year; new venues; milestones reached this year. The last line gives the date the data runs to.

Definitions the script already applies:
- **Tourist run** — any run away from Frimley Lodge. Exception: **Chris Peddle's home is
  Littlehampton Prom**, so his runs there are not tourist runs.
- **Quickest since YYYY** — YYYY is the most recent earlier year with a time at least as
  fast. "ALL-TIME PB" means nothing earlier was as fast.
- **Milestones** — 50, 100, 250, 500, 600, 700, 800, 900, 1000 runs, for both total
  parkruns and Frimley Lodge runs, where the milestone run itself was this year.
- Junior parkruns are excluded.

## Step 3: Write the one-liners

One bullet per athlete with runs this year, alphabetical, name in bold:

- **Always include their quickest time of the year** (with venue).
- Pick the most positive angles from: quicker than previous years (all-time PB / quickest
  since YYYY), more parkruns than last year or most since YYYY / most ever, more tourist
  runs, notable new venues.
- **Weave any milestone into the same one-liner**, bold, with the date — e.g.
  "Celebrated her **600th parkrun** (9 May)…". No separate milestones section.
- Upbeat and positive only — never criticise a slower year. If nothing improved, celebrate
  consistency, venues, or a time close to their best.
- British English; concise.

After the list, briefly note: the date the data runs to, anyone skipped (no runs this year,
or not on the roster), and any data that looks wrong and wasn't used — e.g. a best time far
quicker than the rest of their history (David Peddle's 2025 shows 18:51, likely a data
error).

## Common mistakes

- Counting Chris Peddle's Littlehampton runs as tourist runs.
- Using the per-run `pb` flag in the json — it's set on every run, so it's meaningless;
  work PBs out from times (the script does).
- Reading "quickest since 2024" as "slower than 2024" — it means the fastest since 2024.
