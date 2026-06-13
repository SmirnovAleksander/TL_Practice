using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure( EntityTypeBuilder<Reservation> builder )
    {
        builder.ToTable( nameof( Reservation ) );
        builder.HasKey( r => r.Id );

        builder.Property( r => r.GuestName )
            .HasMaxLength( 200 )
            .IsRequired();

        builder.Property( r => r.GuestPhoneNumber )
            .HasMaxLength( 20 );

        builder.Property( r => r.Total )
            .HasColumnType( "money" )
            .IsRequired();

        builder.Property( r => r.Currency )
            .HasMaxLength( 3 )
            .HasConversion<string>()
            .IsRequired();

        builder.Property( r => r.ArrivalDate )
            .IsRequired();

        builder.Property( r => r.DepartureDate )
            .IsRequired();

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey( r => r.PropertyId )
            .OnDelete( DeleteBehavior.Restrict );

        builder.HasOne<RoomType>()
            .WithMany()
            .HasForeignKey( r => r.RoomTypeId )
            .OnDelete( DeleteBehavior.Restrict );
    }
}
