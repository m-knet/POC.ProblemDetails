using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;
using Microsoft.AspNetCore.Http;

namespace POC.ProblemDetails
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // 1. Register ProblemDetails and map exceptions
            services.AddProblemDetails(c =>
            {
                c.MapToStatusCode<InvalidOperationException>(StatusCodes.Status409Conflict);

                c.Map<CustomDomainException>(ex => new StatusCodeProblemDetails(StatusCodes.Status409Conflict)
                {
                    Detail = ex.CustomProperty
                });
            });

            // 2. Add problem details conventions
            services.AddControllers()
                .AddProblemDetailsConventions();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "POC.ProblemDetails", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 3. Use the middleware
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
