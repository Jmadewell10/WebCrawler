using HtmlAgilityPack;
using MadewellSoftWorks.Biblioplex.WebCrawler.Common;
using MadewellSoftWorks.Biblioplex.WebCrawler.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;

namespace MadewellSoftWorks.Biblioplex.WebCrawler.Services
{
    public class GathererService : IGathererService
    {
        private readonly IWebDriver _driver;
        public GathererService(IWebDriver driver)
        {
            _driver = driver;
        }
        string url = "https://gatherer.wizards.com/Pages/Search/Default.aspx?sort=color+&action=advanced&color=|[W]|[U]|[B]|[R]|[G]|[C]";

        public void GoToGatherer()
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void Close()
        {
            _driver.Close();
        }

        public IList<string> GetNames()
        {
            IList<string> nameList = new List<string>();
            var names = _driver.FindElements(By.ClassName("cardTitle"));
            foreach (var name in names)
            {
                string nameString = name.Text;
                nameList.Add(nameString);
            }
            return nameList;
        }

        public void GoToNextPageIfFirstPage()
        {
            try
            {
                var pageElement = _driver.FindElement(By.XPath("//*[@id=\"ctl00_ctl00_ctl00_MainContent_SubContent_bottomPagingControlsContainer\"]/div/a[16]"));
                _driver.Navigate().GoToUrl(pageElement.GetAttribute("href"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }



        public void GoToNextPageIfNotFirstPage()
        {
            try
            {
                var pageElement = _driver.FindElement(By.XPath("//*[@id=\"ctl00_ctl00_ctl00_MainContent_SubContent_bottomPagingControlsContainer\"]/div/a[17]"));
                _driver.Navigate().GoToUrl(pageElement.GetAttribute("href"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void GoToNextPageIfAfterTenthPage()
        {
            try
            {
                var pageElement = _driver.FindElement(By.XPath("//*[@id=\"ctl00_ctl00_ctl00_MainContent_SubContent_bottomPagingControlsContainer\"]/div/a[18]"));
                _driver.Navigate().GoToUrl(pageElement.GetAttribute("href"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void GoToPreviousPage()
        {
            try
            {
                var pageElement = _driver.FindElement(By.XPath("//*[@id=\"ctl00_ctl00_ctl00_MainContent_SubContent_bottomPagingControlsContainer\"]/div/a[2]"));
                _driver.Navigate().GoToUrl(pageElement.GetAttribute("href"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GoToFinalPage()
        {
            var finalPageElementURL = _driver.FindElement(By.XPath("//*[@id=\"ctl00_ctl00_ctl00_MainContent_SubContent_bottomPagingControlsContainer\"]/div/a[17]"));
            _driver.Navigate().GoToUrl(finalPageElementURL.GetAttribute("href"));
        }

        public void GoToFirstPage()
        {
            var firstPageElement = _driver.FindElement(By.XPath("//*[@id=\"ctl00_ctl00_ctl00_MainContent_SubContent_bottomPagingControlsContainer\"]/div/a[1]"));
            _driver.Navigate().GoToUrl(firstPageElement.GetAttribute("href"));
        }

        public int GetPageNumber()
        {
            var pageURL = _driver.Url;
            Regex pattern = new Regex(@"page=(\w+)");
            Match match = pattern.Match(pageURL);
            string page = match.Groups[1].Value;
            int pageNumber = Int32.Parse(page) + 1;
            return pageNumber;
        }

        public int Init()
        {
            GoToGatherer();
            GoToFinalPage();
            int pageCount = GetPageNumber();
            GoToFirstPage();

            return pageCount;
        }

        public string GetImage(int count)
        {
            var imageElement = _driver.FindElement(By.XPath(String.Format("//*[@id=\"ctl00_ctl00_ctl00_MainContent_SubContent_SubContent_ctl00_listRepeater_ctl0{0}_cardImage\"]", count)));
            var imageSRC = imageElement.GetAttribute("src");
            return imageSRC;
        }
    }
}
