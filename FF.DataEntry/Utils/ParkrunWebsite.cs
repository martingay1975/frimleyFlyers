using FF.DataEntry.Dto;
using HtmlAgilityPack;

namespace FF.DataEntry.Utils
{
    public class ParkrunWebsite
    {
        public async Task<List<ParkrunRun>> GetAllAsync(string parkrunId)
        {
            var parkrunRunList = new List<ParkrunRun>();
            var url = $"https://www.parkrun.org.uk/parkrunner/{parkrunId}/all/";
            using (var puppeteer = new PuppeteerHelper())
            {
                await puppeteer.StartAsync();
                await puppeteer.OpenAsync(url, async (page, response) =>
                {
                    //var innerHtml = await puppeteer.GetInnerHtmlAsync(page, "table :last-of-type");
                    var htmlBody = await puppeteer.GetHtmlBodyAsync(page);
                    var html = $"<!DOCTYPE html><html>{htmlBody}</html>";
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(html);

                    var htmlTBodyList = htmlDoc.DocumentNode.SelectNodes("//tbody");
                    var htmlTBody = htmlTBodyList[2];
                    if (htmlTBody == null)
                    {
                        throw new InvalidOperationException("Unable to see the parkrunners content");
                    }

                    
                    var rows = htmlTBody.Elements("tr");
                    foreach (var row in rows)
                    {
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
                });

                return parkrunRunList;
            }
        }
    }
}
