using Crims.Data.Entities;
using Crims.Data.Persistence;
using Crims.Data.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Data.Repository
{
    public class TokenRepository(ApplicationDbContext dbContext, ILogger<Repository<TokenEntity>> logger) : Repository<TokenEntity>(dbContext, logger)
    {
    }
}
