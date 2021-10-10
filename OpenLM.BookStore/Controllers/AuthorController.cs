using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenML.BookStore.Application.Authors.Command;
using OpenML.BookStore.Application.Authors.Queries;
using OpenML.BookStore.Application.Authors.ViewModel;

namespace OpenLM.BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public AuthorController(IMediator mediator, ILogger logger)
        {
            this._mediator = mediator;
            this._logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAuthorQuery() { }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetAuthorQuery() { Id = id }));
        }
        /// <summary>
        /// Api Post Request to create authors
        /// </summary>
        /// <param name="authorViewModel"></param>
        /// <returns></returns>
        [HttpPost("CreateAuthor")]
        public async Task<IActionResult> InsertAuthor(AuthorViewModel authorViewModel)
        {
            if (authorViewModel == null)
            {
                _logger.LogError("Author object sent from client is null.");
                return BadRequest("Author object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid author object sent from client.");
                return BadRequest("Invalid model object");
            }
            return Ok(await _mediator.Send(new CreateAuthorCommand() { authorViewModel = authorViewModel }));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody]AuthorViewModel authorViewModel)
        {
            if (authorViewModel == null)
            {
                _logger.LogError("Author object sent from client is null.");
                return BadRequest("Author object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid author object sent from client.");
                return BadRequest("Invalid model object");
            }
            return Ok(await _mediator.Send(new UpdateAuthorCommand() { authorViewModel = authorViewModel, Id = id }));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            return Ok(await _mediator.Send(new DeleteAuthorCommand() { Id = id }));
        }
    }
}
