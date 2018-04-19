using AutoMapper;
using BooksWebApi.Data;
using BooksWebApi.Entities;
using BooksWebApi.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

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
        services.AddRouting(opt => opt.LowercaseUrls = true);
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(config => config.SlidingExpiration = true);
        services.ConfigureApplicationCookie(options =>
        {
          // Cookie settings
          options.Cookie.HttpOnly = true;
          options.ExpireTimeSpan = TimeSpan.FromMinutes(30);          
          options.SlidingExpiration = true;
          options.Events.OnRedirectToLogin = context =>
          {
              if (context.Request.Path.StartsWithSegments("/api")
              && context.Response.StatusCode == StatusCodes.Status200OK)
              {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.FromResult<object>(null);
              }
              context.Response.Redirect(context.RedirectUri);
              return Task.FromResult<object>(null);
          };
        });
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
