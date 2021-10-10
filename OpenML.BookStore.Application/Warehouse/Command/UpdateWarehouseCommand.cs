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

namespace OpenML.BookStore.Application.Warehouses.Command
{
    public class UpdateWarehouseCommand : IRequest<bool>
    {
        public WarehouseViewModel wareHouseViewModel { get; set; }
        public int Id { get; set; }
        public class UpdateWarehouseJobHandler : IRequestHandler<UpdateWarehouseCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public UpdateWarehouseJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
            {
                bool response = false;
                var objwhouse = _unitOfWork.Query<Warehouse>().FirstOrDefault(x => x.WareHouseId == request.Id);
                if (objwhouse != null)
                {
                    objwhouse.Address = request.wareHouseViewModel.Address;
                    objwhouse.Phone = request.wareHouseViewModel.Phone;
                    objwhouse.LastModified = DateTime.Now;
                    objwhouse.LastModifiedBy = "Test1";
                    _unitOfWork.Update(objwhouse);
                    response = true;
                }
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return response;
            }
        }
    }
}
