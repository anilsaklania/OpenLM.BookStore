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

namespace OpenML.BookStore.Application.Books.Command
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public class DeleteBookJobHandler : IRequestHandler<DeleteBookCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public DeleteBookJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
            {
                bool response = false;
                var objBook = _unitOfWork.Query<Book>().FirstOrDefault(x => x.BookId == request.Id);
                if (objBook != null)
                {                    
                    _unitOfWork.Remove(objBook);
                    response = true;
                }
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return response;
            }
        }
    
    }
}
