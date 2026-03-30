---
name: frimley-monthly-report
description: Generate the monthly Frimley Flyers League report from parkrun CSV data — produces two formatted Excel spreadsheets (last-month-only-formatted.xlsx and last-month-all-formatted.xlsx) plus a social media summary for Messenger. Use this skill whenever the user asks for the monthly report, league results, parkrun summary, race report, Frimley Flyers report, formatted spreadsheets from race data, or a newsletter/social media post based on monthly parkrun results. Also trigger when they mention "the monthly league thing", posting results to the group, or processing the latest parkrun CSV into Excel and a summary. Do NOT trigger for: general CSV manipulation, building web pages, editing templates, or one-off data queries about runners.
---

# Frimley Flyers Monthly League Report

This skill produces three outputs from the latest monthly parkrun data:

1. **last-month-only-formatted.jpg** — screenshot-style image of the single month's results table
2. **last-month-all-formatted.jpg** — screenshot-style image of the overall season standings table
3. **Social media summary** — an upbeat Messenger post covering top performers, milestones, and standings

All paths are relative to the working directory (`R:\git\frimleyFlyers`).

---

## Step 0: Identify the latest monthly CSV

Monthly result files live in `site/res/json/` and follow the naming pattern:

```
{MM}-parkrun {Mon} {DD}.csv
```

Examples: `01-parkrun Jan 31.csv`, `03-parkrun Mar 28.csv`

Pick the file with the highest month number (MM). This is the current month's data. Extract the month name and date from the filename for use in the summary.

---

## Step 1: Create last-month-only-formatted.jpg

Render the monthly results as a table image using Python (matplotlib or PIL).

- **Data source:** the latest monthly CSV identified in Step 0
- **Output:** `site/res/json/last-month-only-formatted.jpg`

### Table columns

| (Position) | Name | Location | Time | % | Pts |

### Styling

- Font: Arial or similar sans-serif
- Header row: bold, dark background (#4472C4 or similar), white text
- Data rows: alternating white and light green (#E2EFDA) backgrounds
- Position, Name, and Pts columns: bold text
- % column: formatted as percentage with 2 decimal places (e.g. -13.05%)
- Cell padding and borders for clean table appearance
- Image should be tight-cropped to the table with no excess whitespace

---

## Step 2: Create last-month-all-formatted.jpg

Render the overall season standings as a table image using Python (matplotlib or PIL).

- **Data source:** `site/res/json/raceData2026.json-spotlight.csv`
- **Output:** `site/res/json/last-month-all-formatted.jpg`

### Table structure

Two header rows:

**Row 1 (section headers):** blank | "Overall" spanning cols 2-5 | month names (January, February, etc.)

**Row 2 (column headers):** (position) | Name | Pts | Home Pts | Tourist Pts | 2025 Best | Pts (per month...)

### Styling

- Include a position column (1, 2, 3...) as the first column, same as Step 1
- Same styling as Step 1 (dark header, alternating white/light green rows, bold Name and Pts)
- Only include month columns up to and including the current month (no empty future months)
- Centre-align numeric columns
- Column header is "2025 Best" (not "Prev Best")
- Image should be tight-cropped to the table with no excess whitespace

---

## Step 3: Generate the social media summary

This is output directly in the conversation (not saved to file). It merges data from three sources:

### Input files

1. **Monthly results CSV** (from Step 0) — columns: Position, Name, Location, Time, %, Pts
2. **Runner stats:** `site/res/json/byParkrunCount.csv` — linked by Name
3. **Season standings:** `site/res/json/raceData2026.json-spotlight.csv` — sorted by overall Pts

### Points scoring reference

| Position | Points |
|----------|--------|
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

A runner with a negative % (improvement on last year) gets +5 bonus points on top.

### Overall season scoring

Best 5 home scores + best 2 tourist scores = overall total. 11 rounds total (Jan-Nov). Most points by end of November wins.

### Analysis to perform

1. Merge monthly CSV with byParkrunCount.csv by Name.
2. Identify:
   - Total number of runners this month
   - Top 5 runners (by Position column)
   - Runners with negative % (faster than previous year's best)
   - Milestone runners (from byParkrunCount.csv only, never byMilestone.csv):
     - **Just passed**: ParkrunsCount or FrimleyLodgeCount is divisible by 50 (e.g. 500, 250) — celebrate it
     - **About to reach**: ParkrunsCount or FrimleyLodgeCount is 1 less than a multiple of 50 (e.g. 499, 249) — mention they'll reach it next time
   - "Quickest for a long time": where LatestRunIsQuickestSince is more than 6 months before the event date
   - Other point-scorers (positions 6-10)
3. From the spotlight CSV, note the overall season standings — especially the top 5-6 positions.

### Output format

Produce one cohesive summary in this structure:

**Opening line:** State the month, event date, and total runner count.

**Top Performers This Month**
- Highlight the top 5 runners with their name, parkrun location, % performance, and their time in brackets — e.g. "Phoebe Harvey stormed to first place at Frimley Lodge with a massive -13.05% (33:06)".
- Weave in any milestones or "quickest for a long time" achievements inline — do not create separate sections for these.
- Note if runners had negative % values (improvements).
- Use short paragraphs or bullet points.

**Other point-scorers:**
A single line listing remaining point-scorers, e.g. "Also scoring this month: Name (X pts), Name (Y pts), ..."

**Milestones:**
If any runner hit or is about to hit a milestone, mention it briefly after the point-scorers. Use byParkrunCount.csv only (never byMilestone.csv). Check for both "just passed" (divisible by 50) and "about to reach" (1 away from a multiple of 50, e.g. 499 → "will reach 500 next time").

**Season standings paragraph:**
A short paragraph summarising the overall standings from the spotlight CSV — who leads, who's close behind, any interesting battles. Reference the "best 5 home + best 2 tourist" scoring system if relevant.

### Tone and style

- Upbeat, friendly, club-newsletter style
- Light emoji use: 🏃‍♀️ 🎉 💪 🏆 (sparingly, for energy)
- Focus on community achievement and improvement
- Concise but celebratory — about 4-6 short paragraphs total
- Do not describe any score as "perfect" unless it is 26 (the maximum: 21 pts for 1st place + 5 bonus for improvement). 22 pts is a strong score, not a perfect one.
- Always sign off the post with "Happy Flying 🏃‍♀️"
- "Home" location means Frimley Lodge parkrun; anything else is a tourist run

### Do NOT include

- A separate "Quickest for a Long Time" section — integrate inline
- A separate "Summary Vibes" section
- Duplicated achievements (mention each achievement once only)
- Negative or critical commentary about anyone's performance
- Any data from byMilestone.csv — always use byParkrunCount.csv for milestone checks
