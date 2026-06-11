using Api.Middleware;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Foundation.Data;
using Infrastructure.Foundation.Repositories;
using Infrastructure.Foundation.Services;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class Program
{
    public static void Main( string[] args )
    {
        var builder = WebApplication.CreateBuilder( args );

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<HotelManagementDbContext>( options =>
        {
            options.UseSqlServer( builder.Configuration.GetConnectionString( "DefaultConnection" ) );
        } );

        builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
        builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
        builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

        builder.Services.AddScoped<IPropertyService, PropertyService>();
        builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
        builder.Services.AddScoped<IReservationService, ReservationService>();

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();

        if ( app.Environment.IsDevelopment() )
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
