using PuppeteerSharp;

namespace FF.DataEntry.Utils
{
    internal class PuppeteerHelper : IDisposable
    {
        private IBrowser browser;

        public async Task StartAsync()
        {
            await new BrowserFetcher().DownloadAsync();
            Console.WriteLine("Got Chrome Driver");

            // Create an instance of the browser and configure launch options
            this.browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Timeout = (int)TimeSpan.FromSeconds(20).TotalMilliseconds
            });
            Console.WriteLine("Created Browser Instance");
        }

        public async Task OpenAsync(string url, Func<IPage, IResponse, Task> run)
        {
            using (var page = await this.browser.NewPageAsync())
            {
                await page.SetExtraHttpHeadersAsync(new Dictionary<string, string>
                {
                    { "Accept" , "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8"},
                    {"Accept-Language", "en-US,en;q=0.9" },
                    {"Cache-Control", "no-cache" },
                    {"Connection", "keep-alive" },
                    {"Pragma", "no-cache" },
                    {"Upgrade-Insecure-Requests", "1" },
                    {"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36" }
                });

                try
                {
                    var response = await page.GoToAsync(url);
                    await run(page, response);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{url} = {ex.ToString()}");
                    throw;
                }
            }
        }

        public async Task HitButtonAsync(IPage page, string selector, bool wait = true)
        {
            await page.WaitForSelectorAsync(selector);
            await page.ClickAsync(selector);

            if (wait)
            {
                await Task.Delay(500);
            }
        }

        public async Task<string> GetInnerHtmlAsync(IPage page, string selector)
        {
            await page.WaitForSelectorAsync(selector);
            await Task.Delay(1000);
            //await page.EvaluateExpressionAsync<string>("window.scroll(0,1500);");
            //await page.ScreenshotAsync("c:\\temp\\abc.jpg");
            var innerHTMLjs = $"document.querySelector('{selector}').innerHTML;";
            var innerHTML = await page.EvaluateExpressionAsync<string>(innerHTMLjs);
            return innerHTML;
        }

        public async Task<string> GetHtmlBodyAsync(IPage page)
        {
            await Task.Delay(1000);
            var innerHTMLjs = $"document.body.outerHTML;";
            var innerHTML = await page.EvaluateExpressionAsync<string>(innerHTMLjs);
            return innerHTML;
        }

        public async Task SelectOptionAsync(IPage page, string selector, string value)
        {
            await page.WaitForSelectorAsync(selector);
            await page.FocusAsync(selector);
            var select = await page.QuerySelectorAsync(selector);
            await select.SelectAsync(value);
        }

        public async Task EnterTextAsync(IPage page, string selector, string value)
        {
            await page.WaitForSelectorAsync(selector);
            await page.TypeAsync(selector, value);
        }

        public void Dispose()
        {
            if (browser != null)
            {
                browser.CloseAsync().Wait();
                browser.DisposeAsync();
            }
        }
    }
}
