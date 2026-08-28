@echo off
rem One-shot monthly workflow: load season json, fetch parkrun data,
rem produce stats + top trumps, save season json. No UI.
rem Optional args pass straight through, e.g.:
rem   run-monthly-stats.bat --skip-toptrumps
rem   run-monthly-stats.bat "d:\some\other\json\folder"
dotnet run --project "%~dp0FF.Console\FF.Console.csproj" -c Release -- %*
exit /b %ERRORLEVEL%
