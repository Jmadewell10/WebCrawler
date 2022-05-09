using OpenQA.Selenium;

namespace MadewellSoftWorks.Biblioplex.WebCrawler.Services
{
    public interface INewDriverService
    {
        IWebDriver StartWebDriver();
    }
}