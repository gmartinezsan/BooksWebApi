using System.Collections.Generic;

namespace BooksWebApi.Models
{
    public class CategoryModel
    {    
      public string Description { get; set; }
      public ICollection<BookWithoutCategoryModel> Books { get; set; }
  }
}
