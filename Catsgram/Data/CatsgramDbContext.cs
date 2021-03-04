using Catsgram.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catsgram.Data
{
    public class CatsgramDbContext : IdentityDbContext<User>
    {
        public CatsgramDbContext(DbContextOptions<CatsgramDbContext> options) 
                :base(options)
        {

        }
        public DbSet<Cat> Cats { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Cat>()
                .HasOne(x => x.User)
                .WithMany(x => x.Cats)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
