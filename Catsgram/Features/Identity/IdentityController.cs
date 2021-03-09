using Catsgram.Features;
using Catsgram.Helpers;
using Catsgram.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Catsgram.Features.Identity
{
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;
        private readonly AppSettings options;

        public IdentityController(UserManager<User> userManager, IOptions<AppSettings> options, IIdentityService identityService)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.options = options.Value;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register(RegisterUserRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel loginRequestModel)
        {
            var user = await this.userManager.FindByNameAsync(loginRequestModel.Username);
            if (user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, loginRequestModel.Password);
            if (!passwordValid)
            {
                return Unauthorized();
            }


            var encryptedToken = this.identityService.GenerateJwtToken(user.Id, user.UserName, this.options.Secret);

            return  new LoginResponseModel
            {
                Token = encryptedToken
            };


        }
    }
}
