using AutoMapper;
using BooksWebApi.Data;
using BooksWebApi.Entities;
using BooksWebApi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
			    services.AddScoped<ICrudRepository, CrudRepository>()
							.AddIdentity<User, IdentityRole>()
							.AddEntityFrameworkStores<BooksCatalogDbContext>()
							.AddDefaultTokenProviders();
              
			    services.AddAutoMapper();
          services.AddMvc()
          .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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

					app.UseAuthentication();					
					app.UseMvc();			  
      }
   }
}
