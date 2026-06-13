using Domain.Entities;

namespace Infrastructure.Foundation.Extensions;

public static class PropertyQueryExtensions
{
    public static IQueryable<Property> PropertyFilter( this IQueryable<Property> query, string? city )
    {
        if ( !string.IsNullOrWhiteSpace( city ) )
        {
            query = query.Where( p => p.City.Contains( city ) );
        }

        return query;
    }
}
