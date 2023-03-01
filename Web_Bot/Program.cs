using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        

        static void Main(string[] args)
        {
            KillChrome();

            IWebDriver driver;
            IWebElement webElement;


            var options = new ChromeOptions();

            options.AddArguments("--user-data-dir=C:\\Users\\1\\AppData\\Local\\Google\\Chrome\\User Data");
            //options.AddArguments("--no - sandbox");
            options.AddArguments("--headless=new");
            options.AddArguments("--disable-gpu");
            //options.AddArguments("--window-size=1920,1080");

            driver = new ChromeDriver(options);

            //driver.Manage().Window.Maximize(); //на полныйй экран
            driver.Navigate().GoToUrl("https://zombyland.wargm.ru/server/61439/votes");
            //driver.Navigate().GoToUrl("https://google.ru");

            System.Threading.Thread.Sleep(GetRandomTime());

            CreateScreenshot(driver, CreateDirectory());

            webElement = driver.FindElement(By.XPath("//div[text()=\"ГОЛОСОВАТЬ\"]"));
            webElement.Click();

            System.Threading.Thread.Sleep(GetRandomTime());

            CreateScreenshot(driver, CreateDirectory());

            //*********

            System.Threading.Thread.Sleep(GetRandomTime());

            driver.Navigate().GoToUrl("https://zombyland.wargm.ru/server/60694/votes");
            //driver.Navigate().GoToUrl("https://google.ru");

            System.Threading.Thread.Sleep(GetRandomTime());

            CreateScreenshot(driver, CreateDirectory());

            webElement = driver.FindElement(By.XPath("//div[text()=\"ГОЛОСОВАТЬ\"]"));
            webElement.Click();

            System.Threading.Thread.Sleep(GetRandomTime());

            CreateScreenshot(driver, CreateDirectory());

            //*********
            driver.Quit();
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
            int value = rnd.Next(40000, 80000);

            //Вернуть полученное значение
            return value;
        }

        public bool CheckKapCha(IWebElement webElement)
        {
            try
            {
                webElement.FindElement(By.ClassName("rc-anchor-center-item rc-anchor-checkbox-label"));
                return true;
            }
            catch (NoSuchElementException)
            {

                return false;
            }
            
        }

        public void KapChaClik(IWebDriver driver,IWebElement webElement)
        {
           
            if (CheckKapCha(webElement))
            {
                webElement = driver.FindElement(By.ClassName("recaptcha-checkbox-border"));
                webElement.Click();
            }
            
        }

    }
}
