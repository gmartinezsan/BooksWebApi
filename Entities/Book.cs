﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebApi.Entities
{
	public class Book
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string AuthorName { get; set; }
		public int Edition { get; set; }
		public DateTime PublicationDate { get; set; }
		public string ISDN { get; set; }
		public Category Category { get; set; }
	}
}
