using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebApi.Entities
{
    public class Category
    {
			public int Id { get; set; }
			public string Description { get; set; }
      public ICollection<Book>Books { get; set; }
	}
}
