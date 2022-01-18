using BiznesradarPLScraper;
using Newtonsoft.Json;

var sat = await new SygnalyAnalizyTechnicznejScraper()
    .Scrape();
Console.WriteLine(JsonConvert.SerializeObject(sat, Formatting.Indented));

var dywidendy = await new DywidendyScraper()
    .Scrape("https://www.biznesradar.pl/dywidendy/indeks:WIGdiv,2021,3,2");
Console.WriteLine(JsonConvert.SerializeObject(dywidendy, Formatting.Indented));

Console.WriteLine("done");