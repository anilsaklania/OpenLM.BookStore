using MediatR;
using Microsoft.Extensions.Configuration;
using OpenML.BookStore.Application.Customers.ViewModel;
using OpenML.BookStore.Application.Interfaces;
using OpenML.BookStore.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OpenML.BookStore.Application.Customers.Command
{
    public class UpdateCustomerCommand : IRequest<bool>
    {
        public CustomerViewModel custViewModel { get; set; }
        public int Id { get; set; }
        public class UpdateCustomerJobHandler : IRequestHandler<UpdateCustomerCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public UpdateCustomerJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                bool response = false;
                var objCustomer = _unitOfWork.Query<Customer>().FirstOrDefault(x => x.CustomerId == request.Id);
                if (objCustomer != null)
                {
                    objCustomer.Address = request.custViewModel.Address;
                    objCustomer.Email = request.custViewModel.Email;
                    objCustomer.Name = request.custViewModel.Name;
                    objCustomer.Phone = request.custViewModel.Phone;
                    objCustomer.LastModified = DateTime.Now;
                    objCustomer.LastModifiedBy = "Test1";
                    _unitOfWork.Update(objCustomer);
                    response = true;
                }                
               
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return response;
            }
        }
    }
}
