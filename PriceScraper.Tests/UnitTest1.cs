using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace PriceScraper.Tests;

public class Tests
{
    private IWebDriver driver;
    private WebDriverWait wait;

    [SetUp]
    public void Setup()
    {
        var options = new FirefoxOptions();
        options.AddArgument("--headless");
    
        driver = new FirefoxDriver(options);
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        driver.Navigate().GoToUrl("https://www.saucedemo.com");
    }

    [Test]
    public void SauseDemo_LoadsSuccesfully()
    {
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        Assert.That(driver.Title, Is.EqualTo("Swag Labs"));
        
        var loginButton = driver.FindElement(By.Id("login-button"));
        Assert.That(loginButton.Displayed, Is.True);
    }

    [Test]
    public void ValidLogin_RedirectsToInventoryPage()
    {
        Login("standard_user", "secret_sauce");
        Assert.That(driver.Url, Is.EqualTo("https://www.saucedemo.com/inventory.html"));
    }


    [TearDown]
    public void TearDown()
    {
        driver?.Quit();
    }

    private void Login(string username, string password)
    {
        driver.FindElement(By.Id("user-name")).SendKeys(username);
        driver.FindElement(By.Id("password")).SendKeys(password);
        
        driver.FindElement(By.Id("login-button")).Click();
    }
}
