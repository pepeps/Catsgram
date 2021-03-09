using Catsgram.Data;
using Catsgram.Infrastructure;
using Catsgram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Catsgram.Features.Cats
{
    public class CatsController : ApiController
    {
        private readonly ICatService service;

        public CatsController(ICatService service)
        {
            this.service = service;
        }

        //[Authorize]
        //[HttpGet] 
        //public async Task<IActionResult> Mine()
        //{

        //}

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCatRequest createCatRequest)
        {
            var userId = this.User.GetId();
            var id =  await this.service.Create(createCatRequest.ImageUrl, createCatRequest.Description, userId);
            return Created(nameof(this.Create), id);
        }
    }
}
