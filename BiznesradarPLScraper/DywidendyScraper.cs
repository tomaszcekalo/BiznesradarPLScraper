using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BiznesradarPLScraper
{
    public class DywidendyScraper
    {
        public async Task<object> Scrape(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            var dywidendy = ParseDywidendy(document);
            return dywidendy;
        }

        private object ParseDywidendy(IDocument document)
        {
            var rows = document
                .QuerySelectorAll("table.qTableFull tr")
                .Select(x => ParseRow(x))
                .ToList();
            return rows;
        }

        private object ParseRow(IElement x)
        {
            var cells = x.QuerySelectorAll("td");
            if(cells.Count()>0)
            {
                var result = new
                {
                    Profil=cells[0],
                    Rok = cells[1]
                        .TextContent,
                    DataWZA=cells[2]
                        .TextContent,
                    LastDay=cells[3]
                        .TextContent,
                    PayoutDay=cells[4]
                        .TextContent,
                    PerStock=cells[5]
                        .TextContent
                        .Trim(),
                    Percentage=cells[6]
                        .TextContent
                        .Trim(),
                    Status=cells[7]
                        .TextContent
                        .Trim()
                };
                return result;
            }
            return null; 
        }
    }
}
