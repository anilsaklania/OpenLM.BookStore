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

namespace OpenML.BookStore.Application.Publishers.Command
{
    public class UpdatePublisherCommand : IRequest<bool>
    {
        public PublisherViewModel pubViewModel { get; set; }
        public int Id { get; set; }
        public class UpdatePublisherJobHandler : IRequestHandler<UpdatePublisherCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IConfiguration _configuration;
            public UpdatePublisherJobHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
                _configuration = configuration;
            }

            public async Task<bool> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
            {
                bool response = false;
                var objPub = _unitOfWork.Query<Publisher>().FirstOrDefault(x => x.Id == request.Id);
                if (objPub != null)
                {
                    objPub.Address = request.pubViewModel.Address;
                    objPub.Phone = request.pubViewModel.Phone;
                    objPub.Name = request.pubViewModel.Name;
                    objPub.URL = request.pubViewModel.URL;
                    objPub.LastModified = DateTime.Now;
                    objPub.LastModifiedBy = "Test1";
                    _unitOfWork.Update(objPub);
                    response = true;
                }                
               
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(true);
                return response;
            }
        }
    }
}
