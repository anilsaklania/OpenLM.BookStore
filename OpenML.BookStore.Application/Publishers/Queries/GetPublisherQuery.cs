using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using OpenML.BookStore.Application.Publishers.ViewModel;
using OpenML.BookStore.Application.Interfaces;
using OpenML.BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenML.BookStore.Application.Publishers.Queries
{
    public class GetPublisherQuery : IRequest<IQueryable<PublisherViewModel>>
    {
        public int Id { get; set; }
        public class GetPublisherJobHandler : IRequestHandler<GetPublisherQuery, IQueryable<PublisherViewModel>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            private readonly IMapper _mapper;
            public GetPublisherJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
                _mapper = mapper;
            }

            public async Task<IQueryable<PublisherViewModel>> Handle(GetPublisherQuery request, CancellationToken cancellationToken)
            {
                IQueryable<PublisherViewModel> objPub = null;
                if (request.Id != 0)
                {
                    objPub = _unitOfWork.Query<Publisher>().Where(f => f.Id == request.Id).Select(a => new PublisherViewModel
                    {
                        Address = a.Address,
                        Phone = a.Phone,
                        Name = a.Name,
                        URL = a.URL,
                        CreatedBy = a.CreatedBy,
                        CreatedDate = a.Created,
                        ModifiedBy = a.LastModifiedBy,
                        ModifiedDate = a.LastModified
                    }).AsQueryable();
                }
                else
                    objPub = _unitOfWork.Query<Publisher>().Select(a => new PublisherViewModel
                    {
                        Address = a.Address,
                        Phone = a.Phone,
                        Name = a.Name,
                        URL = a.URL,
                        CreatedBy = a.CreatedBy,
                        CreatedDate = a.Created,
                        ModifiedBy = a.LastModifiedBy,
                        ModifiedDate = a.LastModified
                    }).AsQueryable();
                return objPub;
            }
        }
    }
}
