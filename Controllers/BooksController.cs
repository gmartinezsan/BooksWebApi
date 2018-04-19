using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BooksWebApi.Entities;
using BooksWebApi.Models;
using BooksWebApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {		
       private readonly ICrudRepository _repository;
       private readonly IMapper _mapper;
       
       public BooksController(ICrudRepository repository, IMapper mapper)
       {
         _repository = repository;
         _mapper = mapper;
       }


      [HttpGet, Authorize]
      public IActionResult Get()
      {
        if (User.Identity.IsAuthenticated)
        {
          var books = _repository.GetBooks();
          return Ok(Mapper.Map<IEnumerable<BookModel>>(books));

        }
        else
        {
           return new ForbidResult();
        }
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
        catch (Exception){ }
        return BadRequest("Could not found Book");
			}

      // POST: api/Books        
      [HttpPost]        
      public async Task<IActionResult> Post([FromBody]AddBookModel model)
      {
        try
        {
          if (!ModelState.IsValid) return BadRequest(ModelState);
          var book = new Book
          {
            Name = model.Name,
            AuthorName = model.AuthorName,
            Edition = model.Edition,
            PublicationDate = model.PublicationDate,
            ISDN = model.ISDN,
            Category = _repository.GetCategoryWithBooks(model.CategoryId)
          };
            
          _repository.Add(book);
          if (await _repository.SaveAllAsync())
          {
            var newUri = Url.Link("BookGet", new { id = book.Id });
            return Created(newUri, book);
          }
        }
        catch (Exception ex)
        { Console.Write(ex.Message); }
        return BadRequest("Could not post Book");
      }
        
      //PUT: api/Books/5
      [HttpPut("{id}")]
      public async Task<IActionResult> Put(int id, [FromBody]UpdateBookModel model)
      {
          string error = "";
          try
          {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var oldBook = _repository.GetBook(id);
            if (oldBook == null) return NotFound($"Couldn't find a book of {id}");                   
            model.category = oldBook.Category;
            _mapper.Map(model, oldBook);              
            if (await _repository.SaveAllAsync())
            {
              return Ok(_mapper.Map<UpdateBookModel>(oldBook));
            }
          }
          catch (Exception e)
          {
              error = e.Message;            
          }
          return BadRequest(string.Format("Could not update book: {0}", error));
      }
        
      //DELETE: api/ApiWithActions/5
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
