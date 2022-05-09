namespace MadewellSoftWorks.Biblioplex.WebCrawler.Services
{
    public class IterationService : IIterationService
    {
        private readonly IGathererService _gathererService;

        public IterationService(IGathererService gathererService)
        {
            _gathererService = gathererService;
        }
        public IList<string> Iterate(int pageCount)
        {
            var names = new List<string>();
            for(int i = 0; i < pageCount; i++)
                {
                    if(i == 0)
                    {
                        var tempNames = _gathererService.GetNames();
                        foreach (var name in tempNames)
                        {
                            names.Add(name);
                        }
                        _gathererService.GoToNextPageIfFirstPage();
                    }

                    if(i > 0 && i < 9)
                    {
                        var tempNames = _gathererService.GetNames();
                        foreach (var name in tempNames)
                        {
                            names.Add(name);
                        }
                        _gathererService.GoToNextPageIfNotFirstPage();
                    }

                    if(i >= 9)
                    {
                        var tempNames = _gathererService.GetNames();
                        foreach (var name in tempNames)
                        {
                            names.Add(name);
                        }
                        _gathererService.GoToNextPageIfAfterTenthPage();
                    }
                }
            return names;
        }
    }
}
