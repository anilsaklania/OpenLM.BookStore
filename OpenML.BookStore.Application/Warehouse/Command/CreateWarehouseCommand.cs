using MediatR;
using Microsoft.Extensions.Configuration;
using OpenML.BookStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenML.BookStore.Domain.Entities;
using OpenML.BookStore.Application.Warehouses.ViewModel;

namespace OpenML.BookStore.Application.Warehouses.Command
{
    public class CreateWarehouseCommand : IRequest<bool>
    {
        public WarehouseViewModel wareHouseViewModel { get; set; }
        public class CreateWarehouseJobHandler : IRequestHandler<CreateWarehouseCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public CreateWarehouseJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
            {
                var objwhouse = new Warehouse();
                objwhouse.Address = request.wareHouseViewModel.Address;
                objwhouse.Phone = request.wareHouseViewModel.Phone;
                objwhouse.Created = DateTime.Now;
                objwhouse.CreatedBy = "Test";
                _unitOfWork.AddObj(objwhouse);
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return true;
            }
        }
    }
}
