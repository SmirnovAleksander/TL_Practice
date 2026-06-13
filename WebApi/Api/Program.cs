using System.Text.Json.Serialization;
using Api.Middleware;
using Infrastructure;

namespace Api;

public class Program
{
    public static void Main( string[] args )
    {
        var builder = WebApplication.CreateBuilder( args );

        builder.Services.AddControllers()
            .AddJsonOptions( options =>
            {
                options.JsonSerializerOptions.Converters.Add( new JsonStringEnumConverter() );
            } );

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddInfrastructure( builder.Configuration );

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
