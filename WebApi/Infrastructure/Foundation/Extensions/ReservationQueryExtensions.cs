using Domain.Dtos.Reservation;
using Domain.Entities;

namespace Infrastructure.Foundation.Extensions;

public static class ReservationQueryExtensions
{
    public static IQueryable<Reservation> ApplyFilter( this IQueryable<Reservation> query, ReservationFilterServiceDto filter )
    {
        if ( filter.PropertyId.HasValue )
        {
            query = query.Where( r => r.PropertyId == filter.PropertyId );
        }
        if ( filter.ArrivalDate.HasValue )
        {
            query = query.Where( r => r.ArrivalDate >= filter.ArrivalDate );
        }
        if ( filter.DepartureDate.HasValue )
        {
            query = query.Where( r => r.DepartureDate <= filter.DepartureDate );
        }
        if ( !string.IsNullOrWhiteSpace( filter.GuestName ) )
        {
            query = query.Where( r => r.GuestName.Contains( filter.GuestName ) );
        }

        return query;
    }
}
