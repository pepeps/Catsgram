using Catsgram.Data;
using Catsgram.Data.Models;
using Catsgram.Features.Cats;
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
    }
}
