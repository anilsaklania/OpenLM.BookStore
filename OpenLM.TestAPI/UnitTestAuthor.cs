using MediatR;
using System;
using Xunit;

namespace OpenLM.TestAPI
{
    public class UnitTestAuthor
    {
        private readonly IMediator _mediator;       
        public UnitTestAuthor(IMediator mediator)
        {
            this._mediator = mediator;            
        }
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            //var okResult = _controller.Get();
            //// Assert
            //Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
    }
}
