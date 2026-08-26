using System.Reflection;
using PriceScraper;

DBContext.initialize();

var scraper = new GasScraper();

// ask usewr for url || address || station number || create List of pre-selected stations

var station = scraper.GetStation("https://leprixdugaz.ca/station/monteregie/couche-tard-256/1722");

if (station != null)
{
    Console.WriteLine($"Gas Price => {station.GetPrice()} ¢/L");
}
else
{
    Console.WriteLine("Price not found for current gas station");
}