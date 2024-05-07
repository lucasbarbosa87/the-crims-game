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
    public class EstablishmentRepository(ApplicationDbContext dbContext, ILogger<AuditableRepository<EstablishmentEntity>> logger) : AuditableRepository<EstablishmentEntity>(dbContext, logger)
    {
    }
}
