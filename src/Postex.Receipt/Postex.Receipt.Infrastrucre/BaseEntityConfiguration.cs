using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postex.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postex.receipt.Infrastrucre
{
    public abstract class BaseEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase>
       where TBase : BaseEntity<int>
    {
        public virtual void Configure(EntityTypeBuilder<TBase> entity)
        {
            entity.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion();

            entity.Property(e => e.CreatedBy)
                .HasDefaultValue(null);

            entity.Property(e => e.ModifiedBy)
                .HasDefaultValue(null);
        }
    }
}
