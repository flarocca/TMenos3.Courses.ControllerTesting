using AutoMapper;
using TMenos3.Courses.ControllerTesting.Contracts.Dtos.Requests;
using TMenos3.Courses.ControllerTesting.Contracts.Dtos.Responses;
using TMenos3.Courses.ControllerTesting.Models.Entities;

namespace TMenos3.Courses.ControllerTesting.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorResponse>();
            CreateMap<AuthorRequest, Author>();

            CreateMap<Book, BookResponse>();
            CreateMap<BookRequest, Book>();
        }
    }
}
