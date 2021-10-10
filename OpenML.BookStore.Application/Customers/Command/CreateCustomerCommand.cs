using MediatR;
using Microsoft.Extensions.Configuration;
using OpenML.BookStore.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using OpenML.BookStore.Domain.Entities;
using OpenML.BookStore.Application.Customers.ViewModel;

namespace OpenML.BookStore.Application.Customers.Command
{
    public class CreateCustomerCommand : IRequest<bool>
    {
        public CustomerViewModel custViewModel { get; set; }
        public class CreateCustomerJobHandler : IRequestHandler<CreateCustomerCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public CreateCustomerJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var objCust = new Customer();
                objCust.Address = request.custViewModel.Address;
                objCust.Email = request.custViewModel.Email;
                objCust.Name = request.custViewModel.Name;
                objCust.Phone = request.custViewModel.Phone;
                objCust.Created = DateTime.Now;
                objCust.CreatedBy = "Test";
                _unitOfWork.AddObj(objCust);
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return true;
            }
        }
    }
}
