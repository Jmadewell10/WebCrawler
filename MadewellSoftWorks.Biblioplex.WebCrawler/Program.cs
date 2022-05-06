using MadewellSoftWorks.Biblioplex.WebCrawler.Services;
using MadewellSoftWorks.Biblioplex.WebCrawler.Worker;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using IHost host = (IHost)Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .ConfigureServices((hostContext, services) =>
    {        
        services.AddHostedService<Worker>((ServiceProvider) =>
        {
            return new Worker(ServiceProvider.GetRequiredService<IGathererService>(), ServiceProvider.GetRequiredService<ILogger<Worker>>());
        });
        services.AddScoped<IWebDriver>((ServiceProvider) =>
        {
            return new ChromeDriver();
        });
        services.AddScoped<IGathererService>((ServiceProvider) =>
        {
            return new GathererService(httpClient: new HttpClient(), driver: ServiceProvider.GetRequiredService<IWebDriver>());
        });
    })
    .Build();
await host.RunAsync();
