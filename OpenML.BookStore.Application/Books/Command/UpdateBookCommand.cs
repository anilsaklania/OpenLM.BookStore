using MediatR;
using Microsoft.Extensions.Configuration;
using OpenML.BookStore.Application.Authors.ViewModel;
using OpenML.BookStore.Application.Books.ViewModel;
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
    public class UpdateBookCommand : IRequest<bool>
    {
        public BookViewModel bookViewModel { get; set; }
        public int Id { get; set; }
        public class UpdateBookJobHandler : IRequestHandler<UpdateBookCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public UpdateBookJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
            {
                bool response = false;
                var objBook = _unitOfWork.Query<Book>().FirstOrDefault(x => x.BookId == request.Id);
                if (objBook != null)
                {
                    objBook.ISBN = request.bookViewModel.ISBN;
                    objBook.Price = request.bookViewModel.Price;
                    objBook.Pub_Id = request.bookViewModel.Pub_Id;
                    objBook.Title = request.bookViewModel.Title;
                    objBook.PublisherName = request.bookViewModel.PublisherName;
                    objBook.Year = request.bookViewModel.Year;
                    objBook.LastModified = DateTime.Now;
                    objBook.LastModifiedBy = "Test1";
                    _unitOfWork.Update(objBook);
                    response = true;
                }                
               
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return response;
            }
        }
    }
}
