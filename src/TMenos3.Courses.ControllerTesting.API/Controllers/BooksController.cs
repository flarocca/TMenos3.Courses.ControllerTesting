using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TMenos3.Courses.ControllerTesting.API.Filters;
using TMenos3.Courses.ControllerTesting.Contracts.Dtos.Requests;
using TMenos3.Courses.ControllerTesting.Contracts.Dtos.Responses;
using TMenos3.Courses.ControllerTesting.Models.Entities;
using TMenos3.Courses.ControllerTesting.Persistance.Repositories;

namespace TMenos3.Courses.ControllerTesting.API.Controllers
{
    [Route("api/authors/{authorId}/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CopyRightActionFilter))]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResponse>>> GetAll(Guid authorId)
        {
            var books = await _bookRepository.GetAllAsync(authorId);
            if (books == null)
                return NotFound();

            var result = _mapper.Map<IEnumerable<BookResponse>>(books);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<ActionResult<BookResponse>> Get(Guid authorId, Guid id)
        {
            var book = await _bookRepository.GetAsync(authorId, id);

            if (book == null)
                return NotFound("Either author or book does not exist.");

            var result = _mapper.Map<BookResponse>(book);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BookResponse>> Create([FromRoute] Guid authorId, [FromBody] BookRequest request)
        {
            try
            {
                var book = _mapper.Map<Book>(request);
                book = await _bookRepository.CreateAsync(authorId, book);
                var result = _mapper.Map<BookResponse>(book);

                return CreatedAtRoute("GetBook", new { authorId = book.AuthorId, id = result.Id }, result);
            }
            catch (ArgumentException)
            {
                return NotFound("Author does not exist.");
            }
        }
    }
}