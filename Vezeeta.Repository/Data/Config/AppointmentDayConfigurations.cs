using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.Repository.Data.Config
{
    public class AppointmentDayConfigurations : IEntityTypeConfiguration<AppointmentDay>
    {
        public void Configure(EntityTypeBuilder<AppointmentDay> builder)
        {
            builder.Property(AD => AD.Day)
                .HasConversion(
                Day => Day.ToString(),
                Day => (Days)Enum.Parse(typeof(Days), Day)
                );
        }
    }
}
