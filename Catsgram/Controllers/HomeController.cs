using Catsgram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Catsgram.Controllers
{ 
    public class HomeController : ApiController
    {
        [Authorize]
        public IActionResult Get()
        {
            return Ok("works");
        }
    }
}
