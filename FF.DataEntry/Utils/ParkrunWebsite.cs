using FF.DataEntry.Dto;
using HtmlAgilityPack;

namespace FF.DataEntry.Utils
{
    public class ParkrunWebsite
    {
        public async Task<List<ParkrunRun>> GetAllAsync(string parkrunId, Action<string>? updateProgressAction = null)
        {
            updateProgressAction?.Invoke("Getting parkrun page");

            var url = $"https://www.parkrun.org.uk/parkrunner/{parkrunId}/all/";
            var httpClient = HttpClientFactory.Create();
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            request.Headers.Add("Accept-Language", "en-US,en;q=0.9");
            request.Headers.Add("Cache-Control", "no-cache");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Pragma", "no-cache");
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");

            var httpResponseMessage = await httpClient.SendAsync(request);
            httpResponseMessage.EnsureSuccessStatusCode();
            var html = await httpResponseMessage.Content.ReadAsStringAsync();
            var parkrunRunList = new List<ParkrunRun>();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var htmlTBodyList = htmlDoc.DocumentNode.SelectNodes("//tbody");
            var htmlTBody = htmlTBodyList[2];
            if (htmlTBody == null)
            {
                throw new InvalidOperationException("Unable to see the parkrunners content");
            }

            var rows = htmlTBody.Elements("tr").ToList();
            for (var rowIndex = 0; rowIndex < rows.Count(); rowIndex++)
            {
                var row = rows[rowIndex];
                updateProgressAction?.Invoke($" {rowIndex + 1} of {rows.Count()}");
                var columns = row.Elements("td").ToList();
                var parkrunRun = new ParkrunRun();

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
                parkrunRunList.Add(parkrunRun);
            }

            return parkrunRunList;
        }
    }
}
