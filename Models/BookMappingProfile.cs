using AutoMapper;
using BooksWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebApi.Models
{
    public class BookMappingProfile : Profile
    {
				public BookMappingProfile()
				{
					CreateMap<Book, BookModel>()
          .ForMember(b => b.category, opt => opt.ResolveUsing(c => c.Category.Description))
          .ReverseMap();
				}
		}
}
