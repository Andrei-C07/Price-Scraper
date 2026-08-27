using System.ComponentModel.DataAnnotations;
using System.Reflection;
using PriceScraper;

DBContext.Initialize();

var scraper = new GasScraper();

// ask usewr for url || address || station number || create List of pre-selected stations

List<string> urls = new List<String>
{
    "https://leprixdugaz.ca/station/monteregie/bcp-iv-service-station-lp/1633",
    "https://leprixdugaz.ca/station/monteregie/couche-tard-264/1714",
    "https://leprixdugaz.ca/station/monteregie/couche-tard-256/1722",
    "https://leprixdugaz.ca/station/monteregie/depanneur-vidal-inc/1767",
    "https://leprixdugaz.ca/station/monteregie/9550-3389-quebec-inc/1617"
};

foreach (string url in urls)
{
    var station = scraper.GetStation(url);

    if (station != null)
    {
        Console.WriteLine($"Station : {station.GetName(station)} - Gas Price => {station.GetPrice(station)} ¢/L");
        DBContext.SavePrice(station);
    }
    else
    {
        Console.WriteLine("Station not found");
    }
}