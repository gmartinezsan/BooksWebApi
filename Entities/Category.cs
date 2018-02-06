using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebApi.Entities
{
    public class Category
    {
			public long Id { get; set; }
			public string Description { get; set; }
      public List<Book>Books { get; set; }
	}
}
