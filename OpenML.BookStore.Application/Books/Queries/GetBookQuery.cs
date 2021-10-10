using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using OpenML.BookStore.Application.Books.ViewModel;
using OpenML.BookStore.Application.Interfaces;
using OpenML.BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenML.BookStore.Application.Books.Queries
{
    public class GetBookQuery : IRequest<List<BookViewModel>>
    {
        public int Id { get; set; }
        public class GetBookJobHandler : IRequestHandler<GetBookQuery, List<BookViewModel>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            private readonly IMapper _mapper;
            public GetBookJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
                _mapper = mapper;
            }

            public async Task<List<BookViewModel>> Handle(GetBookQuery request, CancellationToken cancellationToken)
            {
                List<BookViewModel> objAuthor = null;
                if (request.Id != 0)
                {
                    objAuthor = _unitOfWork.Query<Book>().Where(f => f.BookId == request.Id).Select(a => new BookViewModel
                    {
                        ISBN = a.ISBN,
                        Price = a.Price,
                        PublisherName = a.PublisherName,
                        Pub_Id = a.Pub_Id,
                        Title = a.Title,
                        Year = a.Year,
                        CreatedBy = a.CreatedBy,
                        CreatedDate = a.Created,
                        ModifiedBy = a.LastModifiedBy,
                        ModifiedDate = a.LastModified
                    }).ToList();
                }
                else
                    objAuthor = _unitOfWork.Query<Book>().Select(a => new BookViewModel
                    {
                        ISBN = a.ISBN,
                        Price = a.Price,
                        PublisherName = a.PublisherName,
                        Pub_Id = a.Pub_Id,
                        Title = a.Title,
                        Year = a.Year,
                        CreatedBy = a.CreatedBy,
                        CreatedDate = a.Created,
                        ModifiedBy = a.LastModifiedBy,
                        ModifiedDate = a.LastModified
                    }).ToList();
                return objAuthor;
            }
        }
    }
}
