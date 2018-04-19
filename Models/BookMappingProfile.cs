using AutoMapper;
using BooksWebApi.Entities;

namespace BooksWebApi.Models
{
  public class BookMappingProfile : Profile
  {
    public BookMappingProfile()
    {
      CreateMap<Book, BookModel>()
      .ForMember(b => b.category, opt => opt.ResolveUsing(c => c.Category.Description))
      .ReverseMap();
        
      CreateMap<Book, UpdateBookModel>()
      .ReverseMap();
    }
  }
}
