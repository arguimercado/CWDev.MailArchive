using MailProcessing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailProcessing.Commons.Results
{
    public class FetchResult
    {
        public static FetchResult CreateMime(MimeMessage message)
        {
            return new FetchResult
            {
                Message = message,
                Error = null
            };
        }

        public static FetchResult CreateError(ErrorMailLog error)
        {
            return new FetchResult
            {
                Message = null,
                Error = error
            };
        }

        MimeMessage? Message { get; init; }

        ErrorMailLog? Error { get; init; }
    }
    
}
