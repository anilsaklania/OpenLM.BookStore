using MediatR;
using Microsoft.Extensions.Configuration;
using OpenML.BookStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenML.BookStore.Domain.Entities;
using OpenML.BookStore.Application.Books.ViewModel;

namespace OpenML.BookStore.Application.Books.Command
{
    public class CreateBookCommand : IRequest<bool>
    {
        public BookViewModel bookViewModel { get; set; }
        public class CreateBookJobHandler : IRequestHandler<CreateBookCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public CreateBookJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(CreateBookCommand request, CancellationToken cancellationToken)
            {
                var objBook = new Book();
                objBook.ISBN = request.bookViewModel.ISBN;
                objBook.Price = request.bookViewModel.Price;
                objBook.Pub_Id = request.bookViewModel.Pub_Id;
                objBook.Title = request.bookViewModel.Title;
                objBook.PublisherName = request.bookViewModel.PublisherName;
                objBook.Year = request.bookViewModel.Year;
                objBook.Created = DateTime.Now;
                objBook.CreatedBy = "Test";
                 _unitOfWork.AddObj(objBook);
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return true;
            }
        }
    }
}
