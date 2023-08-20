using AutoMapper;
using Demo.Dtos.Book;
using Demo.Models;

namespace Demo.MappingProfiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, GetBookDto>()
                .ForMember(des => des.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<AddBookDto, Book>();
        }
    }
}
