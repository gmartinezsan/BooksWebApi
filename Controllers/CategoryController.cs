using AutoMapper;
using BooksWebApi.Entities;
using BooksWebApi.Models;
using BooksWebApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebApi.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICrudRepository _repository;
        private readonly IMapper _mapper;

        public CategoryController(ICrudRepository repository, IMapper mapper)
        {
          _repository = repository;
          _mapper = mapper;          
    }

        [HttpGet("{id}", Name = "CategoryGet")]
        public IActionResult Get(int id)
        {
          var category = _repository.GetCategoryWithBooks(id);
          if (category == null) return NotFound($"Category of {id} was not found");          
          return Ok(Mapper.Map<CategoryModel>(category));
          //return Ok(category);
    }
    }
}
