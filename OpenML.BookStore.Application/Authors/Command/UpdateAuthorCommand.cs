using MediatR;
using Microsoft.Extensions.Configuration;
using OpenML.BookStore.Application.Authors.ViewModel;
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
    public class UpdateAuthorCommand : IRequest<bool>
    {
        public AuthorViewModel authorViewModel { get; set; }
        public int Id { get; set; }
        public class UpdateAuthorJobHandler : IRequestHandler<UpdateAuthorCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public UpdateAuthorJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
            {
                bool response = false;
                var objAuthor = _unitOfWork.Query<Author>().FirstOrDefault(x => x.ID == request.Id);
                if (objAuthor != null)
                {
                    objAuthor.Address = request.authorViewModel.Address;
                    objAuthor.ContactNo = request.authorViewModel.ContactNo;
                    objAuthor.Name = request.authorViewModel.Name;
                    objAuthor.PortalURL = request.authorViewModel.PortalURL;
                    objAuthor.LastModified = DateTime.Now;
                    objAuthor.LastModifiedBy = "Test1";
                    _unitOfWork.Update(objAuthor);
                    response = true;
                }                
               
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return response;
            }
        }
    }
}
