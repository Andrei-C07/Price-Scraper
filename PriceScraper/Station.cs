using OpenQA.Selenium.BiDi.BrowsingContext;

namespace PriceScraper;

public class Station
{
    private string name;
    private string url;
    private string address;
    private decimal price;

    public Station(string name, string url, string address, decimal price)
    {   
        name = this.name;
        url = this.url;
        address = this.address;
        price = this.price;
    }

    public Decimal GetPrice()
    {
        return price;
    }
}