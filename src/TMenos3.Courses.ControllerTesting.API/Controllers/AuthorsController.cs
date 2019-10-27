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
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CopyRightActionFilter))]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorResponse>>> GetAll()
        {
            var authors = await _authorRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<AuthorResponse>>(authors);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<ActionResult<AuthorResponse>> Get(Guid id)
        {
            var author = await _authorRepository.GetAsync(id);

            if (author == null)
                return NotFound("Author does not exist.");

            var result = _mapper.Map<AuthorResponse>(author);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorResponse>> Create([FromBody] AuthorRequest request)
        {
            var author = _mapper.Map<Author>(request);
            author = await _authorRepository.CreateAsync(author);
            var result = _mapper.Map<AuthorResponse>(author);

            return CreatedAtRoute("GetAuthor", new { id = result.Id }, result);
        }
    }
}