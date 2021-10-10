using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenML.BookStore.Application.Warehouses.Command;
using OpenML.BookStore.Application.Warehouses.Queries;
using OpenML.BookStore.Application.Warehouses.ViewModel;

namespace OpenLM.BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WarehouseController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetWarehouseQuery() { }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetWarehouseQuery() { Id = id }));
        }
        /// <summary>
        /// Api Post Request to create authors
        /// </summary>
        /// <param name="authorViewModel"></param>
        /// <returns></returns>
        [HttpPost("CreateBook")]
        public async Task<IActionResult> InsertBook(WarehouseViewModel pubViewModel)
        {
            return Ok(await _mediator.Send(new CreateWarehouseCommand() { wareHouseViewModel = pubViewModel }));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody]WarehouseViewModel pubViewModel)
        {
            return Ok(await _mediator.Send(new UpdateWarehouseCommand() { wareHouseViewModel = pubViewModel, Id = id }));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return Ok(await _mediator.Send(new DeleteWarehouseCommand() { Id = id }));
        }
    }
}
