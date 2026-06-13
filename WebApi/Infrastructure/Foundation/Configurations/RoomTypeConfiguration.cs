using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure( EntityTypeBuilder<RoomType> builder )
    {
        builder.ToTable( nameof( RoomType ) );
        builder.HasKey( r => r.Id );

        builder.Property( r => r.Name )
            .HasMaxLength( 200 )
            .IsRequired();

        builder.Property( r => r.DailyPrice )
            .HasColumnType( "money" )
            .IsRequired();

        builder.Property( r => r.Currency )
            .HasMaxLength( 3 )
            .HasConversion<string>()
            .IsRequired();

        builder.Property( r => r.MinPersonCount )
            .IsRequired();

        builder.Property( r => r.MaxPersonCount )
            .IsRequired();

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey( r => r.PropertyId )
            .OnDelete( DeleteBehavior.Cascade );
    }
}
