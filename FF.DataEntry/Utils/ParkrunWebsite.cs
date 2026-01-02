using FF.DataEntry.Dto;
using HtmlAgilityPack;

namespace FF.DataEntry.Utils
{
    public class ParkrunWebsite
    {
        public async Task<List<ParkrunRun>> GetAllAsync(string parkrunId, Action<string>? updateProgressAction = null)
        {
            updateProgressAction?.Invoke("Getting parkrun page");

            string url = $"https://www.parkrun.org.uk/parkrunner/{parkrunId}/all/";
            HttpClient httpClient = HttpClientFactory.Create();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

            // Core headers
            request.Headers.TryAddWithoutValidation(
                "Accept",
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7"
            );

            request.Headers.TryAddWithoutValidation(
                "Accept-Language",
                "en-GB,en-US;q=0.9,en;q=0.8"
            );

            request.Headers.TryAddWithoutValidation("Cache-Control", "no-cache");
            request.Headers.TryAddWithoutValidation("Pragma", "no-cache");
            request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
            request.Headers.TryAddWithoutValidation("Priority", "u=0, i");

            // User-Agent
            request.Headers.TryAddWithoutValidation(
                "User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/143.0.0.0 Safari/537.36"
            );

            // Sec-CH headers
            request.Headers.TryAddWithoutValidation(
                "Sec-CH-UA",
                "\"Google Chrome\";v=\"143\", \"Chromium\";v=\"143\", \"Not A(Brand\";v=\"24\""
            );
            request.Headers.TryAddWithoutValidation("Sec-CH-UA-Mobile", "?0");
            request.Headers.TryAddWithoutValidation("Sec-CH-UA-Platform", "\"Windows\"");

            // Fetch metadata headers
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "document");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "navigate");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "none");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-User", "?1");


            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request);
            httpResponseMessage.EnsureSuccessStatusCode();
            string html = await httpResponseMessage.Content.ReadAsStringAsync();
            List<ParkrunRun> parkrunRunList = new List<ParkrunRun>();

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            HtmlNodeCollection htmlTBodyList = htmlDoc.DocumentNode.SelectNodes("//tbody");
            HtmlNode htmlTBody = htmlTBodyList[2];
            if (htmlTBody == null)
            {
                throw new InvalidOperationException("Unable to see the parkrunners content");
            }

            List<HtmlNode> rows = htmlTBody.Elements("tr").ToList();
            for (int rowIndex = 0; rowIndex < rows.Count(); rowIndex++)
            {
                HtmlNode row = rows[rowIndex];
                updateProgressAction?.Invoke($" {rowIndex + 1} of {rows.Count()}");
                List<HtmlNode> columns = row.Elements("td").ToList();
                ParkrunRun parkrunRun = new ParkrunRun();

                try
                {
                    parkrunRun.Event = columns[0].InnerText;
                    parkrunRun.Date = DateTime.Parse(columns[1].InnerText);
                    parkrunRun.EventNo = Convert.ToInt32(columns[2].InnerText);
                    parkrunRun.Position = Convert.ToInt32(columns[3].InnerText);
                    parkrunRun.RaceTime = TimeSpan.Parse("00:" + columns[4].InnerText);
                    parkrunRun.AgeGrading = Convert.ToDecimal(columns[5].InnerText.Split("%")[0]);
                    parkrunRun.Pb = !string.IsNullOrEmpty(columns[6].InnerText);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }

                // only include parkruns that don't include the word "junior"
                if (parkrunRun.Event.IndexOf(" junior") == -1)
                {
                    parkrunRunList.Add(parkrunRun);
                }
            }

            return parkrunRunList;
        }
    }
}
