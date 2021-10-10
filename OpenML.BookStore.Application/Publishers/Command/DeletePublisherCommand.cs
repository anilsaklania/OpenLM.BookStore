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

namespace OpenML.BookStore.Application.Publishers.Command
{
    public class DeletePublisherCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public class DeletePublisherJobHandler : IRequestHandler<DeletePublisherCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public DeletePublisherJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
            {
                bool response = false;
                var objPub = _unitOfWork.Query<Publisher>().FirstOrDefault(x => x.Id == request.Id);
                if (objPub != null)
                {                    
                    _unitOfWork.Remove(objPub);
                }
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return response;
            }
        }
    
    }
}
