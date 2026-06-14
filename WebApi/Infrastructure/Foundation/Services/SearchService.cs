using Domain.Dtos.Reservation;
using Domain.Dtos.Search;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Infrastructure.Foundation.Services;

public class SearchService : ISearchService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IReservationRepository _reservationRepository;

    public SearchService(
        IPropertyRepository propertyRepository,
        IRoomTypeRepository roomTypeRepository,
        IReservationRepository reservationRepository )
    {
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
        _reservationRepository = reservationRepository;
    }

    public async Task<IReadOnlyList<SearchResultServiceDto>> SearchAsync( SearchFilterServiceDto filter, CancellationToken ct = default )
    {
        IReadOnlyList<Property> allProperties = await _propertyRepository.GetAllAsync( filter.City, ct );

        List<SearchResultServiceDto> results = [];

        foreach ( Property property in allProperties )
        {
            IReadOnlyList<RoomType> roomTypes = await _roomTypeRepository
                .GetByPropertyAsync( property.Id, filter.Guests, filter.MaxPrice, ct );

            foreach ( RoomType roomType in roomTypes )
            {
                if ( filter.ArrivalDate.HasValue && filter.DepartureDate.HasValue )
                {
                    bool hasOverlap = await _reservationRepository.HasOverlapAsync(
                        roomType.Id,
                        filter.ArrivalDate.Value,
                        filter.DepartureDate.Value,
                        ct );

                    if ( hasOverlap )
                    {
                        continue;
                    }
                }

                results.Add( new SearchResultServiceDto
                {
                    Property = property,
                    RoomType = roomType,
                    DailyPrice = roomType.DailyPrice,
                    Currency = roomType.Currency
                } );
            }
        }

        return results;
    }
}
