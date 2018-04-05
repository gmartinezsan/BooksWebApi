using BooksWebApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksWebApi.Repository
{
    public interface ICrudRepository
    {
		
		void Add<T>(T entity) where T : class;
		void Delete<T>(T entity) where T : class;
		Task<bool> SaveAllAsync();
		
		IEnumerable<Book> GetBooks();
		Book GetBook(int id);

		IEnumerable<Book> GetBooksByCategory(string category);				
		Category GetCategoryWithBooks(int id);


		User GetUser(string userName);
	}
}
