using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public AuthorController(IMediator mediator)
        {
            this._mediator = mediator;
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
            return Ok(await _mediator.Send(new CreateAuthorCommand() { authorViewModel = authorViewModel }));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody]AuthorViewModel authorViewModel)
        {
            return Ok(await _mediator.Send(new UpdateAuthorCommand() { authorViewModel = authorViewModel, Id = id }));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            return Ok(await _mediator.Send(new DeleteAuthorCommand() { Id = id }));
        }
    }
}
