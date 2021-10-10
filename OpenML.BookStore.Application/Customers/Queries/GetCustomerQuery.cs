using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using OpenML.BookStore.Application.Customers.ViewModel;
using OpenML.BookStore.Application.Interfaces;
using OpenML.BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenML.BookStore.Application.Customers.Queries
{
    public class GetCustomerQuery : IRequest<IQueryable<CustomerViewModel>>
    {
        public int Id { get; set; }
        public class GetCustomerJobHandler : IRequestHandler<GetCustomerQuery, IQueryable<CustomerViewModel>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            private readonly IMapper _mapper;
            public GetCustomerJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
                _mapper = mapper;
            }

            public async Task<IQueryable<CustomerViewModel>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
            {
                IQueryable<CustomerViewModel> objCust = null;
                if (request.Id != 0)
                {
                    objCust = _unitOfWork.Query<Customer>().Where(f => f.CustomerId == request.Id).Select(a => new CustomerViewModel
                    {
                        Address = a.Address,
                        Phone = a.Phone,
                        Name = a.Name,
                        Email = a.Email,
                        CreatedBy = a.CreatedBy,
                        CreatedDate = a.Created,
                        ModifiedBy = a.LastModifiedBy,
                        ModifiedDate = a.LastModified
                    }).AsQueryable();
                }
                else
                    objCust = _unitOfWork.Query<Customer>().Select(a => new CustomerViewModel
                    {
                        Address = a.Address,
                        Phone = a.Phone,
                        Name = a.Name,
                        Email = a.Email,
                        CreatedBy = a.CreatedBy,
                        CreatedDate = a.Created,
                        ModifiedBy = a.LastModifiedBy,
                        ModifiedDate = a.LastModified
                    }).AsQueryable();
                return objCust;
            }
        }
    }
}
