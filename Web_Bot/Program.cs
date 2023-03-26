using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
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

        static void KillChrome()
        {
            if (Process.GetProcessesByName("chrome") != null)
            {
                Process[] chromeInstances = Process.GetProcessesByName("chrome");

                foreach (Process item in chromeInstances)
                {
                    item.Kill();
                }
            }
        }

        static void Main(string[] args)
        {
            KillChrome();

            string argProf1 = "--profile-directory=Default";
            string argProf2 = "--profile-directory=Profile 1";

            string url = "https://zombyland.wargm.ru/server/61439/votes";
            string url2 = "https://zombyland.wargm.ru/server/60694/votes";

            WorkBot workBot = new WorkBot(argProf1);

            workBot.Vote(url);
            workBot.Vote(url2);

            workBot.DriverQuit();

            System.Threading.Thread.Sleep(2000);

            WorkBot workBot2 = new WorkBot(argProf2);

            workBot2.Vote(url);
            workBot2.Vote(url2);

            workBot2.DriverQuit();

            System.Threading.Thread.Sleep(2000);

            KillChrome();           

        }
    }
}