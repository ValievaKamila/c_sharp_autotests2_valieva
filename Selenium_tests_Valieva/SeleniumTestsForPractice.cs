using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace Selenium_tests_Valieva;

public class SeleniumTestsForPractice
{
    [SetUp]
    //предусловия
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3); //неявное ожидание
        Autorization();
    }

    public ChromeDriver driver; //ChromeDriver это тип объявляемой переменной
    [Test]
    //проверка, что авторизация проходит успешно
    public void Authorization()
    {
        var currentUrl = driver.Url;
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/news");
    }
    [Test]
    // проверка, что навигационное меню работает и можем перейти в Сообщества
    public void NavigationTest()
    {
        var slideMenu=driver.FindElement(By.CssSelector("[data-tid='SidebarMenuButton']"));
        slideMenu.Click();
        var community=driver.FindElements(By.CssSelector("[data-tid='Community']")).First(element => element.Displayed);
        community.Click();
        var communityTitle = driver.FindElements(By.CssSelector("[data-tid='Title']"));
        Assert.That(driver.Url=="https://staff-testing.testkontur.ru/communities", "Ожидалось увидеть следующий урл: https://staff-testing.testkontur.ru/communities");
    }
    
    [Test]
    //проверка, что послезавтра ни у кого нет Дня рождения
    public void BirthdaysList()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/news");
        var tabs = driver.FindElement(By.CssSelector("[data-tid='Tabs']")); 
        tabs.Click();
        var dayAfterTomorrow = driver.FindElements(By.CssSelector("[data-tid='Item']")).Last(element => element.Displayed);  
        dayAfterTomorrow.Click();
        var birthList = driver.FindElement(By.CssSelector("[data-tid='BirthdaysList']"));
        birthList.Text.Should().Contain("Нет никого");
    }

    [Test]
    //проверка, что User состоит в сообществах
    public void CounterNotNull()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities?activeTab=isMember");
        var сommunitiesCounter = driver.FindElement(By.CssSelector("[data-tid='CommunitiesCounter']"));
        сommunitiesCounter.Text.Should().NotContain("0 сообществ");
    }
    
    [Test]
    //проверка, что аватарка в выпадающем меню нужного размера
    public void SizeAvatar()
    {
        var avatar = driver.FindElement(By.CssSelector("[data-tid='Avatar']"));
        avatar.GetAttribute("size").Should().Contain("40");
    }

    public void Autorization()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/");
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("user");
        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("1q2w3e4r%T");    
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3)); //явное ожидание
        wait.Until(ExpectedConditions.UrlContains("https://staff-testing.testkontur.ru/news"));
    }
    
    [TearDown] // методы под этим атрибутом (методом) выполняются при любом завершении теста (средство NUnit)
    public void TearDown()
    {
        driver.Quit();  
    }
}