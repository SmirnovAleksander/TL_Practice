using Domain.Entities;
using Infrastructure.Foundation.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Data;

public class HotelManagementDbContext : DbContext
{
    public HotelManagementDbContext( DbContextOptions options )
        : base( options )
    {
    }

    public DbSet<Property> Properties { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating( ModelBuilder builder )
    {
        base.OnModelCreating( builder );

        builder.ApplyConfiguration( new PropertyConfiguration() );
        builder.ApplyConfiguration( new RoomTypeConfiguration() );
        builder.ApplyConfiguration( new ReservationConfiguration() );
    }
}