using Catsgram.Data;
using Catsgram.Infrastructure;
using Catsgram.Models;
using Catsgram.Models.Cats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Catsgram.Controllers
{
    public class CatsController : ApiController
    {
        private readonly CatsgramDbContext context;

        public CatsController(CatsgramDbContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCatRequest createCatRequest)
        {
            var userId = this.User.GetId();
            var cat = new Cat
            {
                Description = createCatRequest.Description,
                ImageUrl = createCatRequest.ImageUrl,
                UserId = userId
            };

            this.context.Add(cat);
            await this.context.SaveChangesAsync();

            return Created(nameof(this.Create), cat.Id);
        }
    }
}
