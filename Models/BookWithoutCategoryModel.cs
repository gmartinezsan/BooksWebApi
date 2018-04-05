using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
