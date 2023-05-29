using Arbolus.CallInvoice.BusinessLogic;
using Arbolus.CallInvoice.BusinessLogic.Services;
using Arbolus.CallInvoice.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace Arbolus.CallInvoice
{
    public class Startup
	{
		private IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration) => Configuration = configuration;

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddHttpClient();

			services.AddLogging();

			services.AddScoped<IClientsService, ClientService>();
			services.AddScoped<IExpertsService, ExpertsService>();
			services.AddScoped<IRatesService, RatesService>();
			services.AddScoped<IPriceCalculator, PriceCalculator>();
			services.AddScoped<DataRetriever>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/error");
				app.UseHsts();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}

}

