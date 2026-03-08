using CardLand.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLand.Data.ModelsConfigurations
{
    public class OfficeEntityTypeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.ToTable("Offices");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedNever();

            builder.Property(o => o.Code);
            builder.Property(o => o.Uuid);
            builder.Property(o => o.Type).HasConversion<string>();
            builder.Property(o => o.CountryCode).IsRequired();
            builder.Property(o => o.AddressRegion);
            builder.Property(o => o.AddressCity);
            builder.Property(o => o.AddressStreet);
            builder.Property(o => o.AddressHouseNumber);
            builder.Property(o => o.WorkTime);

            builder.OwnsOne(o => o.Coordinates, cb =>
            {
                cb.Property(c => c.Latitude).HasColumnName("CoordinatesLatitude");
                cb.Property(c => c.Longitude).HasColumnName("CoordinatesLongitude");
            });

            builder.OwnsMany(o => o.Phones, pb =>
            {
                pb.WithOwner().HasForeignKey(p => p.OfficeId);
                pb.ToTable("OfficePhones");
                pb.Property<int>("Id");
                pb.HasKey("Id");

                pb.Property(p => p.PhoneNumber)
                  .IsRequired();

                pb.Property(p => p.Additional);
            });
        }
    }
}
