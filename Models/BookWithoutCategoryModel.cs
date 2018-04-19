using System;

namespace BooksWebApi.Models
{
    public class BookWithoutCategoryModel
    {
      public string Name { get; set; }
      public string AuthorName { get; set; }
      public int Edition { get; set; }
      public DateTime PublicationDate { get; set; }
      public string ISDN { get; set; }    
    }
}
