using Catsgram.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catsgram.Infrastructure.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public string GetUserId()
        {
            return this.httpContextAccessor.HttpContext?.User?.GetId();
        }

        public string GetUsername()
        {
            return this.httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }
    }
}
