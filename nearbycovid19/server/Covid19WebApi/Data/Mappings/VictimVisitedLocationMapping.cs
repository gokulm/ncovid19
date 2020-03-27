using Covid19WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Covid19WebApi.Data.Mappings
{
    public class VictimVisitedLocationMapping : IEntityTypeConfiguration<VictimVisitedLocation>
    {
        public void Configure(EntityTypeBuilder<VictimVisitedLocation> builder)
        {
            builder
                .ToTable("VictimVisitedLocation")
                .HasKey(k => k.Id);
            builder
                .Property(p => p.Id)
                .HasColumnName("VictimVisitedLocationId")
                .ValueGeneratedNever();
        }
    }
}
