using FF.DataEntry.Api;

// One-shot, no-UI run of the monthly workflow:
// 1) load season json  2) fetch parkrun data (persisting athlete jsons)
// 3) stats + league csvs + top trumps  4) save season json
//
// Usage: FF.Console [jsonFolderPath] [--skip-toptrumps] [--skip-fetch]
string folderPath = @"r:\git\frimleyFlyers\site\res\json";
bool skipTopTrumps = false;
bool skipFetch = false;
foreach (string arg in args)
{
    if (string.Equals(arg, "--skip-toptrumps", StringComparison.OrdinalIgnoreCase))
    {
        skipTopTrumps = true;
    }
    else if (string.Equals(arg, "--skip-fetch", StringComparison.OrdinalIgnoreCase))
    {
        skipFetch = true;
    }
    else
    {
        folderPath = arg;
    }
}

string filePath = Path.Combine(folderPath, $"raceData{Manager.Year}.json");
if (!File.Exists(filePath))
{
    Console.Error.WriteLine($"Season file not found: {filePath}");
    return 1;
}

Console.WriteLine($"Loading {filePath}");
Manager manager = new Manager();
await manager.InitAsync(filePath);
string basePath = manager.GetBasePath(filePath);

if (skipFetch)
{
    Console.WriteLine("Skipping parkrun fetch (using athlete data already on disk)");
}
else
{
    Console.WriteLine("Fetching parkrun data for each athlete");
    List<string> seasonsAthletes = manager.AthletesManager.Athletes.Select(athlete => athlete.Name).ToList();
    await manager.AthletesManager.PopulateWithParkrunListAsync(basePath, seasonsAthletes, true);
    Console.WriteLine($"Fetched parkrun data for {seasonsAthletes.Count} athletes");
}

Console.WriteLine("Producing stats");
Stats.GetByMilestone(manager.AthletesManager.Athletes, basePath);
Stats.GetByTotalParkruns(manager.AthletesManager.Athletes, basePath);
manager.CreateFFLeagueCsv(filePath);

if (skipTopTrumps)
{
    Console.WriteLine("Skipping top trumps");
}
else
{
    Console.WriteLine("Producing top trumps");
    await Stats.GetTopTrumps(manager.AthletesManager.Athletes, basePath);
}

await manager.SaveAsync(filePath);
Console.WriteLine($"Saved {filePath}");
Console.WriteLine("Done");
return 0;
