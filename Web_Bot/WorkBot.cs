using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;



namespace Web_Bot
{
    class WorkBot
    {
        IWebDriver driver;
        IWebElement webElement;
        WebDriverWait wait;

        By _vote = By.XPath("//div[text()=\"ГОЛОСОВАТЬ\"]");
        By _kapCha = By.CssSelector("div.recaptcha-checkbox-spinner");
        By _kapChaCheckBox = By.CssSelector("div.recaptcha-checkbox-checkmark");
        By _googleKapCha = By.CssSelector("iframe[name^='a-'][src^='https://www.google.com/recaptcha/api2/anchor?']");

        string _url;

        public WorkBot(string argProf)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--user-data-dir=C:\\Users\\1\\AppData\\Local\\Google\\Chrome\\User Data");
            options.AddArgument(argProf);
            //options.AddArgument("--profile-directory=Profile 1");
            //options.AddArguments("--headless=new");
            //options.AddArguments("--disable-gpu");
            options.AddArguments("--window-size=1920,1080");
            driver = new ChromeDriver(options);            
        }

        public void Vote(string _url)
        {
            try
            {
                driver.Navigate().GoToUrl(_url);

                System.Threading.Thread.Sleep(5000);

                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(12));

                if (CheckElement(wait, _vote))
                {
                    CreateScreenshot(CreateDirectory());

                    //webElement = wait.Until(ExpectedConditions.ElementToBeClickable(_vote));
                    webElement = driver.FindElement(_vote);
                    webElement.Click();
                }
                                
                var frame = wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(_googleKapCha));

                if (frame != null)
                {
                    CreateScreenshot(CreateDirectory());

                    Actions action = new Actions(driver);
                    action.MoveToElement(webElement = driver.FindElement(_kapChaCheckBox)).Click().Perform();
                }
                
                if (CheckElement(wait, _vote))
                {
                    CreateScreenshot(CreateDirectory());

                    IWebElement submitButton = driver.FindElement(_vote);
                    submitButton.Click();
                }

                //CreateScreenshot(CreateDirectory());
            }
            catch (Exception ex)
            {
                //CreateScreenshot(CreateDirectory());

                Console.WriteLine("Что то пошло не так: " + ex.Message);
                //Console.ReadLine();
                //DriverQuit();
            }
            //finally
            //{
            //    driver.Quit();
            //}
        }

        private bool CheckElement(WebDriverWait wait, By by)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(by));
                //driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {

                return false;
            }

        }

        private string CreateDirectory()
        {
            string DirectoryDatetime = String.Format("{0:yyyy-MM-dd_hh_mm}", DateTime.Now);

            string folderPath = @"C:\!!Screen Web_Bot\" + DirectoryDatetime + @"\";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }

        private void CreateScreenshot(string folderPath)
        {
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            string friendlyDatetime = String.Format("{0:yyyy-MM-dd_hh-mm-ss}", DateTime.Now);
            screenshot.SaveAsFile(folderPath + friendlyDatetime + ".jpg", ScreenshotImageFormat.Jpeg);
        }

        public void DriverQuit()
        {
            driver.Quit();
        }

    }

}
