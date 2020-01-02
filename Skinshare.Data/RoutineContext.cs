using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skinshare.Core.Entities;

namespace Skinshare.Data
{
    public class RoutineContext : DbContext
    {
        public RoutineContext(DbContextOptions<RoutineContext> options) : base(options)
        {
        }

        public DbSet<Routine> Routines { get; set; }
        public DbSet<Step> Steps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>("Created");
                modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
            }

            modelBuilder.Entity<Routine>().HasIndex(r => r.Identifier);
            
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property("Created").CurrentValue = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Property("LastModified").CurrentValue = DateTime.UtcNow;
                        break;
                }

                // TODO: figure out a way to abstract the implementation of the identifier
                if (entry.Entity is Routine routine && entry.State == EntityState.Added)
                {
                    routine.Identifier = Guid.NewGuid().ToString();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}