using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenML.BookStore.Application.Customers.Command;
using OpenML.BookStore.Application.Customers.Queries;
using OpenML.BookStore.Application.Customers.ViewModel;

namespace OpenLM.BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetCustomerQuery() { }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetCustomerQuery() { Id = id }));
        }
        /// <summary>
        /// Api Post Request to create authors
        /// </summary>
        /// <param name="authorViewModel"></param>
        /// <returns></returns>
        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> InsertCustomer(CustomerViewModel pubViewModel)
        {
            return Ok(await _mediator.Send(new CreateCustomerCommand() {  custViewModel = pubViewModel }));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody]CustomerViewModel pubViewModel)
        {
            return Ok(await _mediator.Send(new UpdateCustomerCommand() { custViewModel = pubViewModel, Id = id }));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            return Ok(await _mediator.Send(new DeleteCustomerCommand() { Id = id }));
        }
    }
}
