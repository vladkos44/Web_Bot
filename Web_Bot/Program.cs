using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Bot
{
    class Program
    {
        //public By vote = By.XPath("//div[text()=\"ГОЛОСОВАТЬ\"]");
        //public By kapCha = By.ClassName("rc-anchor-center-item rc-anchor-checkbox-label");

        static void Main(string[] args)
        {
            KillChrome();

            IWebDriver driver;
            IWebElement webElement;

            By vote = By.XPath("//div[text()=\"ГОЛОСОВАТЬ\"]");
            By kapCha = By.CssSelector("div.recaptcha-checkbox-spinner");
            By kapChaCheckBox = By.CssSelector("div.recaptcha-checkbox-spinner");

            var options = new ChromeOptions();

            options.AddArguments("--user-data-dir=C:\\Users\\1\\AppData\\Local\\Google\\Chrome\\User Data");
            ///////options.AddArguments("--no - sandbox");
            //options.AddArguments("--headless=new");
            //options.AddArguments("--disable-gpu");
            options.AddArguments("--window-size=1920,1080");

            driver = new ChromeDriver(options);

            //webElement = driver.FindElement(By.XPath("//div[text()=\"ГОЛОСОВАТЬ\"]"));
            //driver.Manage().Window.Maximize(); //на полныйй экран
            driver.Navigate().GoToUrl("https://zombyland.wargm.ru/server/61439/votes");

            //webElement = driver.FindElement(By.XPath("//div[text()=\"ГОЛОСОВАТЬ\"]"));

            System.Threading.Thread.Sleep(10000);

            ClickVote(driver, webElement = driver.FindElement(vote));

            System.Threading.Thread.Sleep(5000);

            //CheckElement(driver, webElement, kapCha);

            if (CheckElement(driver, webElement, kapCha))
            {
                CreateScreenshot(driver, CreateDirectory());

                webElement = driver.FindElement(kapChaCheckBox);
                System.Threading.Thread.Sleep(5000);
                webElement.Click();

                CreateScreenshot(driver, CreateDirectory());
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.CssSelector("iframe[name^='a-'][src^='https://www.google.com/recaptcha/api2/anchor?']")));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='recaptcha-checkbox goog-inline-block recaptcha-checkbox-unchecked rc-anchor-checkbox']/div[@class='recaptcha-checkbox-checkmark']"))).Click();

            if (CheckElement(driver, webElement, vote))
            {
                ClickVote(driver, webElement = driver.FindElement(vote));
            }



                System.Threading.Thread.Sleep(5000);
            //***************************************

            driver.Navigate().GoToUrl("https://zombyland.wargm.ru/server/60694/votes");

            System.Threading.Thread.Sleep(10000);

            ClickVote(driver, webElement = driver.FindElement(vote));

            System.Threading.Thread.Sleep(5000);

            CheckElement(driver, webElement, kapCha);

            if (CheckElement(driver, webElement, kapCha))
            {
                CreateScreenshot(driver, CreateDirectory());
                
                webElement = driver.FindElement(kapChaCheckBox);

                System.Threading.Thread.Sleep(3000);

                webElement.Click();

                CreateScreenshot(driver, CreateDirectory());
            }

            System.Threading.Thread.Sleep(5000);
            //*********
            driver.Quit();
        }

        static void ClickVote(IWebDriver driver, IWebElement webElement)
        {
            System.Threading.Thread.Sleep(GetRandomTime());

            CreateScreenshot(driver, CreateDirectory());

            webElement.Click();
                        
            CreateScreenshot(driver, CreateDirectory());

        }

        static void KillChrome()
        {
            Process[] chromeInstances = Process.GetProcessesByName("chrome");
            foreach (Process item in chromeInstances)
            {
                item.Kill();
            }

        }

        static string CreateDirectory()
        {
            string DirectoryDatetime = String.Format("{0:yyyy-MM-dd_hh}", DateTime.Now);

            string folderPath = @"C:\!!Screen Web_Bot\" + DirectoryDatetime + @"\";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;            
        }

        static void CreateScreenshot(IWebDriver driver, string folderPath)
        {
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            string friendlyDatetime = String.Format("{0:yyyy-MM-dd_hh-mm-ss}", DateTime.Now);
            screenshot.SaveAsFile(folderPath + friendlyDatetime + ".jpg", ScreenshotImageFormat.Jpeg);
        }

        static int GetRandomTime()
        {
            //Создание объекта для генерации чисел (с указанием начального значения)
            Random rnd = new Random();

            //Получить случайное число 
            int value = rnd.Next(10000, 20000);

            //Вернуть полученное значение
            return value;
        }

        //static bool CheckKapCha(IWebDriver driver, IWebElement webElement)
        //{
        //    try
        //    {
        //        driver.FindElement(By.ClassName("rc-anchor-center-item rc-anchor-checkbox-label"));
        //        return true;
        //    }
        //    catch (NoSuchElementException)
        //    {

        //        return false;
        //    }
            
        //}

        //static void KapChaClik(IWebDriver driver,IWebElement webElement)
        //{
           
        //    if (CheckKapCha(webElement))
        //    {
        //        webElement = driver.FindElement(By.ClassName("recaptcha-checkbox-border"));
        //        webElement.Click();
        //    }
            
        //}

        static bool CheckElement(IWebDriver driver,IWebElement webElement,By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {

                return false;
            }

        }

    }
}
