
using BiznesradarPLScraper;

var task = await new DywidendyScraper()
    .Scrape("https://www.biznesradar.pl/dywidendy/indeks:WIGdiv,2021,3,2");

Console.WriteLine("done");