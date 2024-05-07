using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Core.Failures
{
    public class NotFoundFailure(string message) : Failure(HttpStatusCode.NotFound, message);
}
