using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenML.BookStore.Application.Authors.Command;
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
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
