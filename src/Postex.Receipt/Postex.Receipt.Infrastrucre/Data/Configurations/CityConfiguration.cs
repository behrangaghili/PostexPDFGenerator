using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postex.receipt.Domain.Models;

namespace Postex.receipt.Infrastrucre.Data.Configurations
{
    public class CityConfiguration : BaseEntityConfiguration<City>
    {
        public override void Configure(EntityTypeBuilder<City> entity)
        {
            base.Configure(entity);
            entity.ToTable("Cities");
            entity.Property(c => c.CityName)
                .HasMaxLength(512);
        }
    }
}
