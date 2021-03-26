using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using POC.ProblemDetails;

await Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    }).RunConsoleAsync();
