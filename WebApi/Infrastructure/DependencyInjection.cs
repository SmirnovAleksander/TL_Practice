using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Foundation.Data;
using Infrastructure.Foundation.Repositories;
using Infrastructure.Foundation.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure( this IServiceCollection services, IConfiguration configuration )
    {
        services.AddDbContext<HotelManagementDbContext>( options =>
            options
                .UseSqlServer( configuration.GetConnectionString( "DefaultConnection" ) )
                .UseSnakeCaseNamingConvention()
        );

        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();

        services.AddScoped<IPropertyService, PropertyService>();
        services.AddScoped<IRoomTypeService, RoomTypeService>();
        services.AddScoped<IReservationService, ReservationService>();

        return services;
    }
}
