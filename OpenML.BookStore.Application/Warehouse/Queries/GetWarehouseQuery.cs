using AutoMapper;
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
using OpenML.BookStore.Application.Warehouses.ViewModel;

namespace OpenML.BookStore.Application.Warehouses.Queries
{
    public class GetWarehouseQuery : IRequest<IQueryable<WarehouseViewModel>>
    {
        public int Id { get; set; }
        public class GetWarehouseJobHandler : IRequestHandler<GetWarehouseQuery, IQueryable<WarehouseViewModel>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            private readonly IMapper _mapper;
            public GetWarehouseJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
                _mapper = mapper;
            }

            public async Task<IQueryable<WarehouseViewModel>> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
            {
                IQueryable<WarehouseViewModel> objWH = null;
                if (request.Id != 0)
                {
                    objWH = _unitOfWork.Query<Warehouse>().Where(f => f.WareHouseId == request.Id).Select(a => new WarehouseViewModel
                    {
                        Address = a.Address,
                        Phone = a.Phone,
                        CreatedBy = a.CreatedBy,
                        CreatedDate = a.Created,
                        ModifiedBy = a.LastModifiedBy,
                        ModifiedDate = a.LastModified
                    }).AsQueryable();
                }
                else
                    objWH = _unitOfWork.Query<Warehouse>().Select(a => new WarehouseViewModel
                    {
                        Address = a.Address,
                        Phone = a.Phone,
                        CreatedBy = a.CreatedBy,
                        CreatedDate = a.Created,
                        ModifiedBy = a.LastModifiedBy,
                        ModifiedDate = a.LastModified
                    }).AsQueryable();
                return objWH;
            }
        }
    }
}
