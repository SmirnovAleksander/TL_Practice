using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure( EntityTypeBuilder<Property> builder )
    {
        builder.ToTable( nameof( Property ) );
        builder.HasKey( p => p.Id );

        builder.Property( p => p.Name )
            .HasMaxLength( 200 )
            .IsRequired();

        builder.Property( p => p.Country )
            .HasMaxLength( 100 )
            .IsRequired();

        builder.Property( p => p.City )
            .HasMaxLength( 100 )
            .IsRequired();

        builder.Property( p => p.Address )
            .HasMaxLength( 300 )
            .IsRequired();

        builder.Property( p => p.Latitude )
            .HasColumnType( "decimal(9,6)" );

        builder.Property( p => p.Longitude )
            .HasColumnType( "decimal(9,6)" );
    }
}
