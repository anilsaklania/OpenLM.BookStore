using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenML.BookStore.Application.Publishers.Command;
using OpenML.BookStore.Application.Publishers.Queries;
using OpenML.BookStore.Application.Publishers.ViewModel;

namespace OpenLM.BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PublisherController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetPublisherQuery() { }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetPublisherQuery() { Id = id }));
        }
        /// <summary>
        /// Api Post Request to create authors
        /// </summary>
        /// <param name="authorViewModel"></param>
        /// <returns></returns>
        [HttpPost("CreateBook")]
        public async Task<IActionResult> InsertBook(PublisherViewModel pubViewModel)
        {
            return Ok(await _mediator.Send(new CreatePublisherCommand() { pubViewModel = pubViewModel }));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody]PublisherViewModel pubViewModel)
        {
            return Ok(await _mediator.Send(new UpdatePublisherCommand() { pubViewModel = pubViewModel, Id = id }));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return Ok(await _mediator.Send(new DeletePublisherCommand() { Id = id }));
        }
    }
}
