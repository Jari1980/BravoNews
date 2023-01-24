using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimerNewsApp.Data;
using TimerNewsApp.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(builder => builder.AddJsonFile("local.settings.json", false, true))
   .ConfigureServices(s =>
   {
        s.AddDbContext<FuncDbContext>();
        s.AddScoped<IDailyService, DailyService>();
        s.AddScoped<IWeeklyService, WeeklyService>();
        s.AddScoped<IStorageService, StorageService>();
        s.AddScoped<IEmailService, EmailService>();
        s.AddScoped<IArchiveNewsService, ArchiveNewsService>();
        s.AddHttpClient("sendemail");
   })
    .Build();

host.Run();