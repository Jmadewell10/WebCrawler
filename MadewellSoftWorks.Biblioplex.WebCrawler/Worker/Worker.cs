using MadewellSoftWorks.Biblioplex.WebCrawler.Services;

namespace MadewellSoftWorks.Biblioplex.WebCrawler.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IGathererService _gathererService;
        private readonly ILogger<Worker> _logger;

        public Worker(IGathererService gathererService, ILogger<Worker> logger)
        {
            _gathererService = gathererService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow);
                _gathererService.GoToGatherer();
                _gathererService.GoToFinalPage();
                int pageCount = _gathererService.GetPageNumber();
                _gathererService.GoToFirstPage();
                for(int i = 0; i < pageCount; i++)
                {
                    var names = _gathererService.GetNames();
                    foreach(var name in names)
                    {
                        Console.WriteLine(name);
                    }
                    _gathererService.NextPageUrl(i+1);
                }
                _gathererService.Close();
                await Task.Delay(604800000, cancellationToken);
                
            }
        }
    }
}
