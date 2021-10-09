using System;
using System.Collections.Generic;
using System.Text;

namespace OpenML.BookStore.Application.Common.Utility
{
    public class AppSettings
    {       
        public static class EmailSettings
        {
            public const string FROMEMAIL = "EmailSettings:FromEmail";
            public const string PORT = "EmailSettings:Port";
            public const string HOST = "EmailSettings:Host";
            public const string ISDEVENVIROMENT = "EmailSettings:IsDevEnviroment";
            public const string DEVTESTINGEMAIL = "EmailSettings:DevTestingEmail";
            public const string ISADDITIONALRECIPIENT = "EmailSettings:IsAdditionalRecipient";
            public const string ADDITIONALRECIPIENT = "EmailSettings:AdditionalRecipient";
        }
    }
}
