using MadewellSoftWorks.Biblioplex.WebCrawler.Services;

namespace MadewellSoftWorks.Biblioplex.WebCrawler.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IGathererService _gathererService;
        private readonly IIterationService _iterationService;
        private readonly ILogger<Worker> _logger;

        public Worker(IGathererService gathererService, ILogger<Worker> logger, IIterationService iterationService)
        {
            _gathererService = gathererService;
            _iterationService = iterationService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow);
                int pageCount = _gathererService.Init();
                var names = _iterationService.Iterate(pageCount);
                foreach (var name in names)
                {
                    Console.WriteLine(name);
                }
                _gathererService.Close();
                await Task.Delay(604800000, cancellationToken);
                
            }
        }
    }
}
