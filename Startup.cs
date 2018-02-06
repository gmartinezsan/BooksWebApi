using BooksWebApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BooksWebApi
{
   public class Startup
    {
			public IConfiguration Configuration { get; set; }

			public Startup(IHostingEnvironment env)
      {
					var builder = new ConfigurationBuilder()
					 .SetBasePath(env.ContentRootPath)
					 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
					 .AddEnvironmentVariables();
			
					 Configuration = builder.Build();
      }
       
      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
					services.AddDbContext<BooksCatalogDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
          services.AddMvc();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
					if (env.IsDevelopment())
					{
						app.UseDeveloperExceptionPage();
						app.UseBrowserLink();
					}
					else
					{
						app.UseExceptionHandler("/Error");
					}

				app.UseMvc();
      }
   }
}
