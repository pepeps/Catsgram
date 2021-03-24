using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catsgram.Infrastructure.Service
{
    public interface ICurrentUserService
    {
        string GetUsername();
        string GetUserId();
    }
}
