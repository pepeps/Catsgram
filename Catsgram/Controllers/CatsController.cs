using Catsgram.Data;
using Catsgram.Models.Cats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catsgram.Controllers
{
    public class CatsController : ApiController
    {

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCatRequest createCatRequest)
        {

        }
    }
}
