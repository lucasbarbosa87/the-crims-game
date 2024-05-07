using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Core.Failures
{
    public class Failure : Exception
    {
        public HttpStatusCode StatusCode;

        public Failure(HttpStatusCode statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }
        public Failure(string message) : base(message)
        {
            this.StatusCode = HttpStatusCode.InternalServerError;
        }
    }
}
