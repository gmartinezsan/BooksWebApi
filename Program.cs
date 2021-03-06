﻿using System;
using BooksWebApi.Data;
using BooksWebApi.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BooksWebApi
{
    public class Program
    {
      public static void Main(string[] args)
      {
        var host = BuildWebHost(args);
      
        using (var scope = host.Services.CreateScope())
        {
           var services = scope.ServiceProvider;
           try
           {
           var context = services.GetRequiredService<BooksCatalogDbContext>();
           var userManager = services.GetRequiredService<UserManager<User>>();
           var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
           DataInitializer.Seed(userManager, roleManager, context);
           }
           catch (Exception ex)
           {
           var logger = services.GetRequiredService<ILogger<Program>>();
           logger.LogError(ex, "An error occurred while seeding the database.");
           }
        }
        host.Run();
    }
    
    public static IWebHost BuildWebHost(string[] args) =>  WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .Build();
   }
}
