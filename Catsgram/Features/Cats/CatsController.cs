using Catsgram.Features.Cats.Models;
using Catsgram.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catsgram.Features.Cats
{
    using static Infrastructure.WebConstants;
    [Authorize]
    public class CatsController : ApiController
    {
        private readonly ICatService service;
        private readonly ICurrentUserService currentUser;

        public CatsController(ICatService service, ICurrentUserService currentUser )
        {
            this.service = service;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IEnumerable<CatListingServiceModel>> Mine()
        {
            var userId = this.currentUser.GetUserId();
            return await this.service.ByUser(userId);
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<CatDetailsServiceModel>> Details(int id)
        {
            return await this.service.Details(id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCatRequestModel createCatRequest)
        {
            var userId = this.currentUser.GetUserId();
            var id =  await this.service.Create(createCatRequest.ImageUrl, createCatRequest.Description, userId);
            return Created(nameof(this.Create), id);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateCatRequestModel model)
        {
            var userId = this.currentUser.GetUserId();

            var updated = await this.service.Update(
                model.Id,
                model.Description,
                userId);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUser.GetUserId();
            var deleted = await this.service.Delete(id, userId);

            if (!deleted)
            {
                return BadRequest();
            }

            return Ok();

        }
    }
}
