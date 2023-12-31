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
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(A => A.Gender)
                .HasConversion(
                G => G.ToString(),
                G => (Gender)Enum.Parse(typeof(Gender), G)
                );
        }
    }
}
