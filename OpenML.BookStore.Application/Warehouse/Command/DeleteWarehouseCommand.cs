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

namespace OpenML.BookStore.Application.Warehouses.Command
{
    public class DeleteWarehouseCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public class DeleteWarehouseJobHandler : IRequestHandler<DeleteWarehouseCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public DeleteWarehouseJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
            {
                bool response = false;
                var objwhouse = _unitOfWork.Query<Warehouse>().FirstOrDefault(x => x.WareHouseId == request.Id);
                if (objwhouse != null)
                {                    
                    _unitOfWork.Remove(objwhouse);
                    response = true;
                }
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return response;
            }
        }
    
    }
}
