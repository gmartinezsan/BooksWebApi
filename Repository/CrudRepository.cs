using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksWebApi.Data;
using BooksWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksWebApi.Repository
{
	public class CrudRepository : ICrudRepository
	{

		private readonly BooksCatalogDbContext _context;

		public CrudRepository(BooksCatalogDbContext context)
		{
			_context = context;
		}

		public void Add<T>(T entity) where T : class
		{
			_context.Add(entity);
		}

		public void Delete<T>(T entity) where T : class
		{
			_context.Remove(entity);
		}

		public Book GetBook(int id)
		{
			return _context.Books
                .Include(b => b.Category)
                .Where(b => b.Id == id)             
								.FirstOrDefault();
		}

		public IEnumerable<Book> GetBooks()
		{
			return _context.Books
                .Include(b => b.Category)
                .ToList();
		}

		public IEnumerable<Book> GetBooksByCategory(string category)
		{
			return _context.Books
									.Where(c => c.Category.Description.Equals(category, StringComparison.CurrentCultureIgnoreCase))
                  .Include(c => c.Category)
									.OrderBy(b => b.Name)
								 .ToList();
		}

		public Category GetCategoryWithBooks(int id)
		{
			return _context.Categories
                .Where(c => c.Id == id)
                .Include(c => c.Books)								
								.FirstOrDefault();
		}

		public User GetUser(string userName)
		{
			return _context.Users								 
								 .Where(u => u.UserName == userName)
								 .Cast<User>()
								 .FirstOrDefault();
		}

		public async Task<bool> SaveAllAsync()
		{
			return (await _context.SaveChangesAsync() > 0);
		}
	}
}
