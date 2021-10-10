using MediatR;
using Microsoft.Extensions.Configuration;
using OpenML.BookStore.Application.Interfaces;
using OpenML.BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenML.BookStore.Application.Customers.Command
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public class DeleteCustomerJobHandler : IRequestHandler<DeleteCustomerCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public DeleteCustomerJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
            {
                bool response = false;
                var objCust = _unitOfWork.Query<Customer>().FirstOrDefault(x => x.CustomerId == request.Id);
                if (objCust != null)
                {
                    _unitOfWork.Remove(objCust);
                    response = true;
                }
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return response;
            }
        }
    }

}