using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.Repository.Data.Config
{
    public class DiscountConfigurations : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {

            builder.Property(D => D.DiscountType)
                .HasConversion(
                Type => Type.ToString(),
                Type => (DiscountType)Enum.Parse(typeof(DiscountType), Type)
                );


        }
    }
}
