using System;
using System.Collections.Generic;
using System.Text;

namespace OpenML.BookStore.Application.Common.Interfaces
{
   public interface ILdapService
    {
        bool ValidateCredentials(string userName, string password);
    }
}
