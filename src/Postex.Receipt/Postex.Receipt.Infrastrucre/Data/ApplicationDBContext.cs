using Microsoft.EntityFrameworkCore;
using Postex.SharedKernel.Domain;

namespace Postex.receipt.Infrastrucre.Data
{
    public class ApplicationDBContext : DbContext//:IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {

                if (entry.Entity is not BaseEntity<int>)
                    continue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["CreatedOn"] = DateTime.Now;
                        entry.CurrentValues["ModifiedOn"] = null;
                        break;

                    case EntityState.Modified:
                        entry.CurrentValues["CreatedOn"] = entry.OriginalValues["CreatedOn"];
                        entry.CurrentValues["ModifiedOn"] = DateTime.Now;
                        break;
                }
            }

            var entiries = base.SaveChangesAsync(cancellationToken);
            return entiries;
        }
    }
}
