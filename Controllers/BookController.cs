using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksWebApi.Data;
using BooksWebApi.Entities;
using BooksWebApi.Models;
using BooksWebApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BookController : Controller
    {		
				private readonly ICrudRepository _repository;
			  private readonly IMapper _mapper;
		
				public BookController(ICrudRepository repository, IMapper mapper)
				{	
					_repository = repository;
					_mapper = mapper;				
				}
				
				[HttpGet]
				public IActionResult Get()
				{
					var books = _repository.GetBooks();
					return Ok(Mapper.Map<IEnumerable<BookModel>>(books));
				}

        // GET: api/Book/5
        [HttpGet("{id}", Name = "BookGet")]
        public IActionResult Get(int id)
        {
					try
					{
						var book = _repository.GetBook(id);
						if (book == null) return NotFound($"Book of {id} was not found");
            return Ok(Mapper.Map<BookModel>(book));
					}
					catch (Exception)
					{ }
					return BadRequest("Could not found Book");
				}
        
        // POST: api/Book
        [HttpPost]
   //     [Authorize]
        public async Task<IActionResult> Post([FromBody]Book model)
        {
          try
          {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _repository.Add(model);
            if (await _repository.SaveAllAsync())
            {
              var newUri = Url.Link("BookGet", new { id = model.Id });
              return Created(newUri, model);
            }
          }
          catch (Exception)
          { }
          return BadRequest("Could not post Book");
        }
        
        // PUT: api/Book/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]BookModel model)
        {
            try
            {
              if (!ModelState.IsValid) return BadRequest(ModelState);
              var oldBook = _repository.GetBook(id);
              if (oldBook == null) return NotFound($"Couldn't find a book of {id}");
              _mapper.Map(model, oldBook);

              if (await _repository.SaveAllAsync())
              {
                return Ok(_mapper.Map<BookModel>(oldBook));
              }
            }
            catch (Exception)
            { }
            return BadRequest("Could not update book");
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
          try
          {
            var oldBook = _repository.GetBook(id);
            if (oldBook == null) return NotFound($"Couldn’t found Book of id {id}");
            _repository.Delete(oldBook);
            if (await _repository.SaveAllAsync())
            {
              return Ok();
            }
          }
          catch (Exception)
          { }
          return BadRequest("Couldn’t Delete Book");
        }
    }
}
