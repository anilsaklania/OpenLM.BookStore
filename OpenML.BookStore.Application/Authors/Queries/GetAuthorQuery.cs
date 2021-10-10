using AutoMapper;
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

namespace OpenML.BookStore.Application.Authors.Queries
{
    public class GetAuthorQuery : IRequest<IQueryable<AuthorViewModel>>
    {
        public int Id { get; set; }
        public class GetAuthorJobHandler : IRequestHandler<GetAuthorQuery, IQueryable<AuthorViewModel>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            private readonly IMapper _mapper;
            public GetAuthorJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
                _mapper = mapper;
            }

            public async Task<IQueryable<AuthorViewModel>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
            {
                IQueryable<AuthorViewModel> objAuthor = null;
                if (request.Id != 0)
                {
                    objAuthor = _unitOfWork.Query<Author>().Where(f => f.ID == request.Id).Select(a => new AuthorViewModel
                    {
                        Address = a.Address,
                        ContactNo = a.ContactNo,
                        Name = a.Name,
                        PortalURL = a.PortalURL,
                        CreatedBy = a.CreatedBy,
                        CreatedDate = a.Created,
                        ModifiedBy = a.LastModifiedBy,
                        ModifiedDate = a.LastModified
                    }).AsQueryable();
                }
                else
                    objAuthor = _unitOfWork.Query<Author>().Select(a => new AuthorViewModel
                    {
                        Address = a.Address,
                        ContactNo = a.ContactNo,
                        Name = a.Name,
                        PortalURL = a.PortalURL,
                        CreatedBy = a.CreatedBy,
                        CreatedDate = a.Created,
                        ModifiedBy = a.LastModifiedBy,
                        ModifiedDate = a.LastModified
                    }).AsQueryable();
                return objAuthor;
            }
        }
    }
}
