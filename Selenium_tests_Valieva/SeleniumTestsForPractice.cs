using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium_tests_Valieva;

public class SeleniumTestsForPractice
{
    [Test]
    public void Authorization()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
        var driver = new ChromeDriver(options);
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/");
        Thread.Sleep(3000);
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("user");
        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("1q2w3e4r%T");
        Thread.Sleep(3000);
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        Thread.Sleep(3000);
        var currentUrl = driver.Url;
        Assert.That(currentUrl=="https://staff-testing.testkontur.ru/news");
        driver.Quit();
    }
}