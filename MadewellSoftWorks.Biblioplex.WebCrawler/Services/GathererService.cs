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
        private readonly HttpClient _httpClient;
        private readonly IWebDriver _driver;
        public GathererService(HttpClient httpClient, IWebDriver driver)
        {
            _httpClient = httpClient;
            _driver = driver;
        }
        string url = "https://gatherer.wizards.com/Pages/Search/Default.aspx?sort=color+&action=advanced&color=|[W]|[U]|[B]|[R]|[G]|[C]";

        public string GetHTML()
        {
            var html = _httpClient.GetStringAsync(url).Result;
            return html;
        }

        public void PrintHTML()
        {
            Console.WriteLine(GetHTML());
        }

        public HtmlDocument GetHTMLDoc(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc;
        }

        public IList<string> GetCardListHTML(HtmlDocument htmlDocument)
        {
            var listOfCardinfoHtml = new List<string>();
            var cardHtml = htmlDocument.DocumentNode.Descendants(Constants.TABLE)
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("cardItemTable")).ToList();
            var cardListItems = cardHtml[0].Descendants("tr")
                .Where(x => x.GetAttributeValue("class", "")
                .Equals("cardItem evenItem")).ToList();
            foreach (var card in cardListItems)
            {
                var cardHtmlString = card.InnerHtml.ToString();
                listOfCardinfoHtml.Add(cardHtmlString);
            }
            return listOfCardinfoHtml;
        }

        public IList<Card> GetCardList(IList<string> cardListHtml)
        {
            IList<Card> cardList = new List<Card>();
            foreach(var card in cardListHtml)
            {
                HtmlDocument cardHTMLDoc = GetHTMLDoc(card);
                Card tempCard = new();
                var name = cardHTMLDoc.DocumentNode.Descendants("span")
                    .Where(x => x.GetAttributeValue("class", "")
                    .Equals("cardTitle"))
                    .Where(x => !string.IsNullOrEmpty(x.InnerText))
                    .Select(x => x.InnerText).ToList();
                tempCard.Name = name[0].ToString();
                Console.WriteLine(tempCard.Name);
               
            }
            return cardList;
        }

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
            IList<string>nameList = new List<string>();
            var names = _driver.FindElements(By.ClassName("cardTitle"));
            foreach (var name in names)
            {
                string nameString = name.Text;
                nameList.Add(nameString);
            }
            return nameList;
        }

        public void NextPageUrl(int currentPage)
        {
            try
            {
                var pageElement = _driver.FindElement(By.XPath(String.Format("//*[@id=\"ctl00_ctl00_ctl00_MainContent_SubContent_bottomPagingControlsContainer\"]/div/a[{0}]", currentPage + 1)));
                _driver.Navigate().GoToUrl(pageElement.GetAttribute("href"));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public void GoToFinalPage()
        {
            var finalPageElementURL = _driver.FindElement(By.XPath("//*[@id=\"ctl00_ctl00_ctl00_MainContent_SubContent_bottomPagingControlsContainer\"]/div/a[17]"));
            _driver.Navigate ().GoToUrl(finalPageElementURL.GetAttribute("href"));
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
            int pageNumber = Int32.Parse(page)+1;
            return pageNumber;
        }
    }
}
