using Catsgram.Data.Models;
using Catsgram.Data.Models.Base;
using Catsgram.Infrastructure.Service;
using Catsgram.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catsgram.Data
{
    public class CatsgramDbContext : IdentityDbContext<User>
    {
        private readonly ICurrentUserService currentUser;

        public CatsgramDbContext(DbContextOptions<CatsgramDbContext> options, ICurrentUserService currentUser)
                : base(options)
        {
            this.currentUser = currentUser;
        }
        public DbSet<Cat> Cats { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInformation();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInformation();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Cat>()
                .HasQueryFilter(c => !c.IsDeleted)
                .HasOne(x => x.User)
                .WithMany(x => x.Cats)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        private void ApplyAuditInformation()
        {
            this.ChangeTracker.Entries()
                 .ToList()
                 .ForEach(entry =>
                 {
                     var username = this.currentUser.GetUsername();
                     if (entry.Entity is IDeletableEntity deletableEntity)
                     {
                         if (entry.State == EntityState.Deleted)
                         {
                             deletableEntity.DeletedOn = DateTime.UtcNow;
                             deletableEntity.DeletedBy = username;
                             deletableEntity.IsDeleted = true;
                             entry.State = EntityState.Modified;

                             return;
                         }
                          
                     }
                     if (entry.Entity is IEntity entity)
                     {
                         if (entry.State == EntityState.Added)
                         {
                             entity.CreatedOn = DateTime.UtcNow;
                             entity.CreatedBy = username;
                         }
                         else if (entry.State == EntityState.Modified)
                         {
                             entity.ModifiedOn = DateTime.UtcNow;
                             entity.ModifiedBy = username;
                         }
                     }
                 });
        }
    }
}
