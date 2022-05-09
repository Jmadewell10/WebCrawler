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
        services.AddScoped<IGathererService, GathererService>((ServiceProvider) =>
        {
            return new GathererService(ServiceProvider.GetRequiredService<IWebDriver>());
        });
        services.AddScoped<IIterationService, IterationService>((ServiceProvider) =>
        {
            return new IterationService(ServiceProvider.GetRequiredService<IGathererService>());
        });
        services.AddHostedService<Worker>((ServiceProvider) =>
        {
            return new Worker(ServiceProvider.GetRequiredService<IGathererService>(), ServiceProvider.GetRequiredService<ILogger<Worker>>(), ServiceProvider.GetRequiredService<IIterationService>());
        });
        services.AddScoped<IWebDriver>((ServiceProvider) =>
        {
            return new ChromeDriver();
        });
    })
    .Build();
await host.RunAsync();
