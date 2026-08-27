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
        this.name = name;
        this.url = url;
        this.address = address;
        this.price = price;
    }

    public Decimal GetPrice(Station s)
    {
        return s.price;
    }

    public String GetName(Station s)
    {
        return s.name;
    }

    public String GetAddress(Station s)
    {
        return s.address;
    }

    public String GetUrl(Station s)
    {
        return s.url;
    }
}