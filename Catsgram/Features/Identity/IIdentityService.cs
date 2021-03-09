using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catsgram.Features.Identity
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string username, string secret);
    }
}
