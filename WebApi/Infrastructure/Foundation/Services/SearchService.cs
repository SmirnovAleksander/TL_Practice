using Infrastructure.Dto.Reservation;
using Infrastructure.Dto.Search;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;

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

    public async Task<IReadOnlyList<SearchResultDto>> SearchAsync( SearchFilterDto filter, CancellationToken ct )
    {
        IReadOnlyList<Property> allProperties = await _propertyRepository.GetAllAsync( ct, filter.City );

        List<SearchResultDto> results = [];

        foreach ( Property property in allProperties )
        {
            IReadOnlyList<RoomType> roomTypes = await _roomTypeRepository
                .GetByPropertyAsync( property.Id, ct, filter.Guests, filter.MaxPrice );

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

                results.Add( new SearchResultDto
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
