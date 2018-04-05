using BooksWebApi.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebApi.Data
{
	public static class DataInitializer
	{
    
    static Category _category = new Category { Description = "Science", Books = new List<Book>() };
    static List<Book> _sample = new List<Book>
    {
        new Book()
        {
          Name = "A science book",
          AuthorName = "A good author",
          Edition = 1,
          PublicationDate = new DateTime(2010, 5 , 1),
          ISDN = "1111111111111111",
          Category = _category
        }
    };

    internal static void Seed(UserManager<User> userMgr, RoleManager<IdentityRole> roleMgr, BooksCatalogDbContext ctx)
		{
				ctx.Database.EnsureCreated();

				if (userMgr.FindByNameAsync("myapiuser").Result == null)
				{
					if (!roleMgr.RoleExistsAsync("Administrator").Result)
					{

					IdentityRole role = new IdentityRole();
					role.Name = "Administrator";
					IdentityResult result = roleMgr.CreateAsync(role).Result;
				}

				var user = new User()
				{
					UserName = "myapiuser",
					FirstName = "myapiuser",
					LastName = "webapi",
					Email = "gmartinezsan@gmail.com"
				};

				var userResult =  userMgr.CreateAsync(user, "MyP4ssw0rd!").Result;

				if (userResult.Succeeded)
				{
						userMgr.AddToRoleAsync(user, "Administrator").Wait();
				}
				else
				{
					throw new InvalidOperationException("Failed to build user or role");
				}
			}


      if (!ctx.Books.Any() && !ctx.Categories.Any())
      {
        ctx.Add(_category);
        ctx.AddRange(_sample);        
      }
      ctx.SaveChanges();		
		}
	}
}
