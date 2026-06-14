using Domain.Entities;

namespace Infrastructure.Foundation.Extensions;

public static class RoomTypeQueryExtensions
{
    public static IQueryable<RoomType> ApplyFilter( this IQueryable<RoomType> query, int? guests, decimal? maxPrice )
    {
        if ( guests.HasValue )
        {
            query = query.Where( r => r.MinPersonCount <= guests && r.MaxPersonCount >= guests );
        }

        if ( maxPrice.HasValue )
        {
            query = query.Where( r => r.DailyPrice <= maxPrice );
        }

        return query;
    }
}
