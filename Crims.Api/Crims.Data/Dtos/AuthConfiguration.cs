using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Data.Dtos
{
    public record AuthConfiguration(string Secret, int RefreshTokenExpires, int ExpiresIn, string ValidAudience);
}
