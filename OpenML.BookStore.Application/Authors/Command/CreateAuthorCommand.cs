using MediatR;
using Microsoft.Extensions.Configuration;
using OpenML.BookStore.Application.Authors.ViewModel;
using OpenML.BookStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenML.BookStore.Domain.Entities;
namespace OpenML.BookStore.Application.Authors.Command
{
    public class CreateAuthorCommand: IRequest<bool>
    {
        public AuthorViewModel authorViewModel { get; set; }
        public class CreateAuthorJobHandler : IRequestHandler<CreateAuthorCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public CreateAuthorJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
            {
                var objAuthor = new Author();
                objAuthor.Address = request.authorViewModel.Address;
                objAuthor.ContactNo = request.authorViewModel.ContactNo;
                objAuthor.Name = request.authorViewModel.Name;
                objAuthor.PortalURL = request.authorViewModel.PortalURL;
                objAuthor.Created = DateTime.Now;
                objAuthor.CreatedBy = "Test";
                 _unitOfWork.AddObj(objAuthor);
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return true;
            }
        }
    }
}
