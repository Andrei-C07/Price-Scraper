using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace PriceScraper;

public class GasScraper
{
    public Decimal? GetPrice(string url)
    {
        using var driver = new FirefoxDriver();
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        driver.Navigate().GoToUrl(url);

        var priceBlock = wait.Until(d =>
            d.FindElement(By.XPath("//div[contains(text(),'aujourd')]/preceding-sibling::div[1]"))
        );

        string priceText = priceBlock.Text;
        
        if (String.IsNullOrEmpty(priceText))
            return null;

        return Decimal.Parse(priceText.Replace("¢", "").Trim());
    }

    public String? GetAddress(string url)
    {
        using var driver = new FirefoxDriver();
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        driver.Navigate().GoToUrl(url);

        var addressLink = driver.FindElement(By.CssSelector("a[href*='google.com/maps/dir']"));
        string address = addressLink.FindElement(By.TagName("span")).Text;

        return address;
    }

    public String? GetName(string url)
    {
        using var driver = new FirefoxDriver();
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));   

        driver.Navigate().GoToUrl(url);

        string stationName = driver.FindElement(By.TagName("h1")).Text;

        if (String.IsNullOrEmpty(stationName))
            return null;

        return stationName;
    }
}