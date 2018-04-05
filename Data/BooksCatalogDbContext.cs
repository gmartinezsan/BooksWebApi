using BooksWebApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebApi.Data
{
  public class BooksCatalogDbContext : IdentityDbContext<User>
  {
    public BooksCatalogDbContext(DbContextOptions<BooksCatalogDbContext> options)
            : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Book>()
      .HasOne(b => b.Category)
      .WithMany(c => c.Books)
      .IsRequired();

      base.OnModelCreating(builder);
    }
  }
}
