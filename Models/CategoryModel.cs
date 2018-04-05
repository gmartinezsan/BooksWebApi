using BooksWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebApi.Models
{
    public class CategoryModel
    {    
      public string Description { get; set; }
      public ICollection<BookWithoutCategoryModel> Books { get; set; }
  }
}
