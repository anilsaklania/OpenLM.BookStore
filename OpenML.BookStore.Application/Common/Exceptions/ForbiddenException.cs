using System;
using System.Collections.Generic;
using System.Text;

namespace OpenML.BookStore.Application.Common
{
   public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message)
        {
        }
    }
}
