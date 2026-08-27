using System.Reflection;
using PriceScraper;

DBContext.Initialize();

var scraper = new GasScraper();

// ask usewr for url || address || station number || create List of pre-selected stations

var station = scraper.GetStation("https://leprixdugaz.ca/station/monteregie/bcp-iv-service-station-lp/1633");

if (station != null)
{
    Console.WriteLine($"Station : {station.GetName(station)} - Gas Price => {station.GetPrice(station)} ¢/L");
    DBContext.SavePrice(station);
}
else
{
    Console.WriteLine("Station not found");
}