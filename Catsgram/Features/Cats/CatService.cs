using Catsgram.Data;
using Catsgram.Data.Models;
using Catsgram.Features.Cats;
using Catsgram.Features.Cats.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catsgram.Features.Cats
{
    public class CatService : ICatService
    {
        private readonly CatsgramDbContext context;

        public CatService(CatsgramDbContext context)
        {
            this.context = context;
        }
        public async Task<int> Create(string imageUrl, string description, string userId)
        {
            var cat = new Cat
            {
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };

            this.context.Add(cat);
            await this.context.SaveChangesAsync();
            return cat.Id;
        }

        public async Task<IEnumerable<CatListingServiceModel>> ByUser(string userId)
        {
            return await this.context.Cats.Where(c => c.UserId == userId).OrderByDescending(c => c.CreatedOn).Select(c => new CatListingServiceModel { Id = c.Id, ImageUrl = c.ImageUrl }).ToListAsync();
        }

        public async Task<CatDetailsServiceModel> Details(int id)
        {
            return await this.context.Cats
                .Where(x => x.Id == id)
                .Select(x => new CatDetailsServiceModel
                    { 
                        Id = x.Id,
                        Description = x.Description,
                        UserName = x.User.UserName,
                        ImageUrl = x.ImageUrl,
                        UserId = x.UserId
                    })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(int id, string description, string userId)
        {
            var cat = await this.GetByIdAndUserId(id, userId);

            if (cat == null)
            {
                return false;
            }

            cat.Description = description;

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id, string userId)
        {
            var cat = await this.GetByIdAndUserId(id, userId);

            if (cat == null)
            {
                return false;
            }

            this.context.Cats.Remove(cat);

            await this.context.SaveChangesAsync();

            return true;

        }

        private async Task<Cat> GetByIdAndUserId(int id, string userId)
        {
            return await this.context.Cats
                .Where(x => x.Id == id && x.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}
