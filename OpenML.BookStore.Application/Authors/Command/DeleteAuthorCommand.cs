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

namespace OpenML.BookStore.Application.Authors.Command
{
    public class DeleteAuthorCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public class DeleteAuthorJobHandler : IRequestHandler<DeleteAuthorCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public DeleteAuthorJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
            {
                bool response = false;
                var objAuthor = _unitOfWork.Query<Author>().FirstOrDefault(x => x.ID == request.Id);
                if (objAuthor != null)
                {                    
                    _unitOfWork.Remove(objAuthor);
                }
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return response;
            }
        }
    
    }
}
