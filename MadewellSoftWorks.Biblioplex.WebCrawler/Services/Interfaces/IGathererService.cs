
namespace MadewellSoftWorks.Biblioplex.WebCrawler.Services
{
    public interface IGathererService
    {
        void Close();
        IList<string> GetNames();
        int GetPageNumber();
        void GoToFinalPage();
        void GoToFirstPage();
        void GoToGatherer();
        void GoToNextPageIfAfterTenthPage();
        void GoToNextPageIfFirstPage();
        void GoToNextPageIfNotFirstPage();
        void GoToPreviousPage();
        int Init();
    }
}