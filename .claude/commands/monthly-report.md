# Monthly Report Generator

Generate an upbeat newsletter-style summary of the Frimley Flyers League monthly event.

## Input Files

Read these three files from `site/res/json/`:

1. **Monthly results CSV** — filename format `{MM}-parkrun {Mon DD}.csv`. Pick the latest by date. Columns: Position, Name, Location ("Home" = Frimley Lodge, else tourist), Time (5km), % (vs last year's best; negative = improvement), Pts.

2. **Runner achievements CSV** — `byParkrunCount.csv`. Columns include: ParkrunsCount, FrimleyLodgeCount, LatestRunIsQuickestSince. Merge with results by Name.

3. **Season standings CSV** — `raceData2026.json-spotlight.csv`. Sorted by Overall Pts descending.

## Points Reference

| Position | Pts |
|----------|-----|
| 1st | 21 |
| 2nd | 17 |
| 3rd | 14 |
| 4th | 12 |
| 5th | 10 |
| 6th | 8 |
| 7th | 6 |
| 8th | 4 |
| 9th | 2 |
| 10th | 1 |

+5 bonus for any runner with negative % (improvement).

## Analysis Steps

1. Load and merge the monthly results with runner achievements by name.
2. Identify:
   - Total number of runners this month
   - Top 5 runners (by position)
   - Runners with negative % (faster than last year)
   - Runners whose ParkrunsCount or FrimleyLodgeCount is divisible by 50 (milestones)
   - Runners whose LatestRunIsQuickestSince is >6 months before the event date ("quickest for a long time")
   - Other runners who scored points beyond top 5

## Output Format

Produce a clear, upbeat, newsletter-style summary:

### Top Performers This Month

- State total number of runners.
- Highlight top 5: name, parkrun location, % improvement.
- Integrate milestone achievements and "quickest for a long time" mentions inline — no separate sections for these.
- Briefly note those with negative % values.
- Use lively, positive tone with short paragraphs or bullet points.

After the top 5:
- Short list of other point-scorers, e.g. "Also scoring this month: Matthew Knight (13 pts), Jen Knight (6 pts), ..."
- Highlight any milestone achievements (e.g. "Fiona Keane-Munday reached 150 parkruns").

### Season Standings

- Summarise the overall standings using the spotlight CSV.
- Remark on the top 5-6 positions. There are 11 total rounds through the year.

## Style

- Upbeat, friendly, club-newsletter tone
- Light emoji sparingly (e.g. 🏃‍♀️🎉💪)
- Focus on community achievement and improvement
- Concise but celebratory — about 4-6 short paragraphs total
- No separate "Quickest for a Long Time" or "Summary Vibes" sections
- No duplicated achievements
