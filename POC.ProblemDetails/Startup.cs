using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;

namespace POC.ProblemDetails
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Register ProblemDetails and map exceptions
            services.AddProblemDetails(c =>
            {
                c.MapToStatusCode<InvalidOperationException>(StatusCodes.Status409Conflict);

                c.Map<CustomDomainException>(ex => new StatusCodeProblemDetails(StatusCodes.Status409Conflict)
                {
                    Detail = ex.CustomProperty
                });
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "POC.ProblemDetails", Version = "v1" });
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Use the middleware
            app.UseProblemDetails();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "POC.ProblemDetails v1"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
