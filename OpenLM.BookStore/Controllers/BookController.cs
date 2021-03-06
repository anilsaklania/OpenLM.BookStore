using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenML.BookStore.Application.Books.Command;
using OpenML.BookStore.Application.Books.Queries;
using OpenML.BookStore.Application.Books.ViewModel;

namespace OpenLM.BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public BookController(IMediator mediator, ILogger logger)
        {
            this._mediator = mediator;
            this._logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetBookQuery() { }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetBookQuery() { Id = id }));
        }
        /// <summary>
        /// Api Post Request to create authors
        /// </summary>
        /// <param name="authorViewModel"></param>
        /// <returns></returns>
        [HttpPost("CreateBook")]
        public async Task<IActionResult> InsertBook(BookViewModel bookViewModel)
        {
            if (bookViewModel == null)
            {
                _logger.LogError("Book object sent from client is null.");
                return BadRequest("Book object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid book object sent from client.");
                return BadRequest("Invalid model object");
            }
            return Ok(await _mediator.Send(new CreateBookCommand() { bookViewModel = bookViewModel }));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody]BookViewModel bookViewModel)
        {
            return Ok(await _mediator.Send(new UpdateBookCommand() { bookViewModel = bookViewModel, Id = id }));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return Ok(await _mediator.Send(new DeleteBookCommand() { Id = id }));
        }
    }
}
