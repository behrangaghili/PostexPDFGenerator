using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postex.receipt.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
