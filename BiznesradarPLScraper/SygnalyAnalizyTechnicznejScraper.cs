using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiznesradarPLScraper
{
    public class SygnalyAnalizyTechnicznejScraper
    {
        public async Task<object> Scrape(
            string url = "https://www.biznesradar.pl/sygnaly-analizy-technicznej/")
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            var result = ParsePage(document);
            return result;
        }

        private object ParsePage(IDocument document)
        {
            var rows = document
                .QuerySelectorAll("table.qTableFull tr")
                .Select(x => ParseRow(x))
                .ToList();
            return rows;
        }

        private object ParseRow(IElement x)
        {
            var symbol = x.QuerySelector("td.symbol");
            var date = x.QuerySelector("td.date");
            var close = x.QuerySelector("td.close");
            var change = x.QuerySelector("td.change");
            var mc = x.QuerySelector("td.mc");
            var signal = x.QuerySelector("td.signal");
            var value = x.QuerySelector("td.value");
            var alert = x.QuerySelector("td.alert");
            var at = x.QuerySelector("td.at");
            var result = new
            {
                SymbolText = symbol?.TextContent.Trim(),
                SymbolHref = symbol?
                    .Children?
                    .FirstOrDefault(x => x.TagName == "A")?
                    .Attributes["href"]?
                    .Value,
                Date = date?.TextContent.Trim(),
                Close = close?.TextContent.Trim(),
                Change = change?.TextContent.Trim(),
                MC = mc?.TextContent.Trim(),
                Signal = signal?.TextContent.Trim(),
                Value = value?.TextContent.Trim(),
                Alert = alert?.TextContent.Trim(),
                AT = at?.TextContent.Trim(),
                IsFirst = x.ClassList.Contains("group-first")
            };
            return result;
        }
    }
}