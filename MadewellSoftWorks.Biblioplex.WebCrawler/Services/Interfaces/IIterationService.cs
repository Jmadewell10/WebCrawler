
namespace MadewellSoftWorks.Biblioplex.WebCrawler.Services
{
    public interface IIterationService
    {
        IList<string> Iterate(int pageCount);
    }
}