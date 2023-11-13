using Microsoft.Extensions.DependencyInjection;
using Untitled.Controllers;
using Untitled.Core.Repository;
using Untitled.Core.Services;

namespace Untitled;

public static class ServiceProviderWrapper
{
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IRoomRepository, RoomRepository>();
        services.AddSingleton<IBookingRepository, BookingRepository>();
        services.AddSingleton<IBookingService, BookingService>();
        services.AddSingleton<IRoomService, RoomService>();
        services.AddSingleton<IReportService, ReportService>();
        services.AddSingleton<BookingController>();
        services.AddSingleton<RoomController>();
        services.AddSingleton<ReportController>();
    }

    public static ServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        return services.BuildServiceProvider();
    }

    public static T GetService<T>(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<T>() ?? throw new NullReferenceException();
    }
}