using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace MaxMines
{
    public class MaxMines
    {
        private int _tongHash;
        private double _hashPSec;
        private double _hashesTarget;
        private string _url;
        private double _jsTongHash = 0;
        private double _jsHashPSec;
        private string _winPos;
        public double JsTongHash
        {
            get
            {
                return _jsTongHash;
            }
            set
            {
                _jsTongHash = value;
            }
        }
        public string WinPos
        {
            get
            {
                return _winPos;
            }
            set
            {
                if (value == "hidden")
                {
                    _winPos = "--window-position=-32000,-32000";
                }
                else
                {
                    _winPos = "--window-position=0,0";
                }
            }
        }
        public double JsHashPSec
        {
            get
            {
                return Math.Round(_hashPSec, 2);
            }
            set
            {
                _hashPSec = value;
            }
        }
        public double HashesTarget
        {
            get
            {
                return _hashesTarget;
            }
            set
            {
                _hashesTarget = value;
            }
        }
        public int TongHash
        {
            get
            {
                return _tongHash;
            }
            set
            {
                _tongHash = value;
            }
        }
        public double HashPSec
        {
            get
            {
                return _hashPSec;
            }
            set
            {
                _hashPSec = value;
            }
        }
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
            }
        }
        public MaxMines(string url, string winPos)
        {
            this.Url = url;
        }
        public MaxMines() : this("https://textvn.com/snippet/6yrhkfxt", "hidden") { }
        public void VerScript(double amt_hash, int interval, string WinPosition)
        {
            double hps;
            double th;
            if (WinPosition == "hidden")
            {
                WinPos = "hidden";
            }
            else
            {
                WinPos = "";
            }
            ChromeDriverService Cserv = ChromeDriverService.CreateDefaultService();
            var opts = new ChromeOptions();
            opts.AddArgument("headless");
            Cserv.HideCommandPromptWindow = true;
            ChromeDriver driver = new ChromeDriver(Cserv, opts);
            IWebElement el;
            driver.Navigate().GoToUrl(Url);
            while (JsTongHash < amt_hash)
            {
                el = driver.FindElement(By.Id("hps"));
                string str_hps = el.Text;
                if (double.TryParse(str_hps, out hps))
                {
                    if (hps > 1000)
                    {
                        double.TryParse(str_hps.Replace(".", ","), out hps);
                    }
                }
                el = driver.FindElement(By.Id("th"));
                string str_th = el.Text;
                double.TryParse(str_th, out th);
                this.JsHashPSec = hps;
                this.JsTongHash = th;
            }
            driver.Quit();
        }
    }
}