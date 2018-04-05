using BooksWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace BooksWebApi.Models
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
          CreateMap<Category, CategoryModel>().ForMember(a => a.Books, opt => opt.ResolveUsing(c => c.Books))
          .ReverseMap();

          CreateMap<Book, BookWithoutCategoryModel>()
          .ReverseMap();
                
        }       
    }
}
