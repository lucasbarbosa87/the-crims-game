using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Core.Failures
{
    public class BadRequestFailure(string message) : Failure(HttpStatusCode.BadRequest, message);
}
