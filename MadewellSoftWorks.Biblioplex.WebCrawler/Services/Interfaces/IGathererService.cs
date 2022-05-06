using HtmlAgilityPack;
using MadewellSoftWorks.Biblioplex.WebCrawler.Models;

namespace MadewellSoftWorks.Biblioplex.WebCrawler.Services
{
    public interface IGathererService
    {
        IList<string> GetCardListHTML(HtmlDocument htmlDocument);
        string GetHTML();
        HtmlDocument GetHTMLDoc(string html);
        void PrintHTML();
        IList<Card> GetCardList(IList<string> cardListHtml);
        public void GoToGatherer();
        public void Close();
        public IList<string> GetNames();
        public void NextPageUrl(int currentPage);
        public void GoToFinalPage();
        public void GoToFirstPage();
        public int GetPageNumber();
    }
}