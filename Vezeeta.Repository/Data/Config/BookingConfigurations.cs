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
    public class BookingConfigurations : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {

            builder.HasOne(B => B.Patient)
                .WithMany(P => P.PatientBookings)
                .HasForeignKey(B => B.PatientId);

            builder.HasOne(B => B.Doctor)
                .WithMany(D => D.DoctorBookings)
                .HasForeignKey(B => B.DoctorId);

            builder.Property(B => B.Day)
               .HasConversion(
               Day => Day.ToString(),
               Day => (Days)Enum.Parse(typeof(Days), Day)
               );

            builder.Property(B => B.Status)
               .HasConversion(
               Status => Status.ToString(),
               Status => (Status)Enum.Parse(typeof(Status), Status)
               );

        }
    }
}
