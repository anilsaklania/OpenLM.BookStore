using MediatR;
using Microsoft.Extensions.Configuration;
using OpenML.BookStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenML.BookStore.Domain.Entities;
using OpenML.BookStore.Application.Publishers.ViewModel;

namespace OpenML.BookStore.Application.Publishers.Command
{
    public class CreatePublisherCommand : IRequest<bool>
    {
        public PublisherViewModel pubViewModel { get; set; }
        public class CreatePublisherJobHandler : IRequestHandler<CreatePublisherCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public CreatePublisherJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
            {
                var objPub = new Publisher();
                objPub.Address = request.pubViewModel.Address;
                objPub.Phone = request.pubViewModel.Phone;
                objPub.Name = request.pubViewModel.Name;
                objPub.URL = request.pubViewModel.URL;
                objPub.Created = DateTime.Now;
                objPub.CreatedBy = "Test";
                 _unitOfWork.AddObj(objPub);
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return true;
            }
        }
    }
}
