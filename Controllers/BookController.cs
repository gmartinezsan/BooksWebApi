using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksWebApi.Data;
using BooksWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Book")]
    public class BookController : Controller
    {
				private readonly BooksCatalogDbContext _context;

				public BookController(BooksCatalogDbContext context)
				{

					_context = context;

					if (_context.Books.Count() == 0)
					{
						_context.Books.Add(new Book { Name = "Book1" });
						_context.SaveChanges();
					}
				}
			

        // GET: api/Book
        [HttpGet]
        public IEnumerable<Book> Get()
        {
					return _context.Books.ToList();
        }

        // GET: api/Book/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Book
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Book/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
