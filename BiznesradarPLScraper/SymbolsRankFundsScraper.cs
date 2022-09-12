using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiznesradarPLScraper
{
    internal class SymbolsRankFundsScraper
    {
        public async Task<object> Scrape(
            string url = "https://www.biznesradar.pl/symbols-rank/funds")
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
            var rankSymbol = x.QuerySelector("td.rank-symbol");
            var quote = x.QuerySelector("td.rank-quote");
            var result = new
            {
                Rank = x.QuerySelector("td.rank-rank")?.TextContent.Trim(),
                SymbolText = rankSymbol?.TextContent.Trim(),
                SymbolHref = rankSymbol?
                    .Children?
                    .FirstOrDefault(x => x.TagName == "A")?
                    .Attributes["href"]?
                    .Value,
                QuoteValue = rankSymbol
                    ?.Children
                    ?.FirstOrDefault(x => x.ClassList.Contains("q_ch_act"))
                    ?.TextContent.Trim(),
                QuoteChange = rankSymbol
                    ?.Children
                    ?.FirstOrDefault(x => x.ClassList.Contains("q_ch_per"))
                    ?.TextContent.Trim(),
                ChangeOverOneDay = x.QuerySelector("td.rank-ch1d")?.TextContent.Trim(),
                ChangeOverOneWeek = x.QuerySelector("td.rank-ch1w")?.TextContent.Trim(),
                ChangeOverOneMonth = x.QuerySelector("td.rank-ch1mo")?.TextContent.Trim(),
            };
            return result;
        }
    }
}