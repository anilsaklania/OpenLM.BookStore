using OpenML.BookStore.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenML.BookStore.Application.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MessageDto message, string fileName,byte[] fileBytes = null);
    }
}
