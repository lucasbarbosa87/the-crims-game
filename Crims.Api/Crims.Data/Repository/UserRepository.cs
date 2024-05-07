using Crims.Data.Entities;
using Crims.Data.Persistence;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Data.Repository
{
    public class UserRepository(ApplicationDbContext dbContext, ILogger<AuditableRepository<UserEntity>> logger) : AuditableRepository<UserEntity>(dbContext, logger)
    {
    }
}
