using Microsoft.Extensions.DependencyInjection;
using Untitled.Constant;
using Untitled.Controllers;
using Untitled.Core.Repository;
using Untitled.Core.Services;
using Untitled.Helper;

namespace Untitled;

internal static class Program
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

    private static void Main()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();
        while (true)
        {
            var command = Console.ReadLine();

            var roomController = ServiceProviderHelper.GetService<RoomController>(serviceProvider);
            var bookingController = ServiceProviderHelper.GetService<BookingController>(serviceProvider);
            var reportController = ServiceProviderHelper.GetService<ReportController>(serviceProvider);

            if (command == null)
            {
                continue;
            }

            switch (command)
            {
                case var _ when CommandRegex.CreateRoom.IsMatch(command):
                    Command.CreateRoom(command, roomController);
                    break;
                case var _ when CommandRegex.BookingByRoomId.IsMatch(command):
                    Command.BookingByRoomId(command, bookingController);
                    break;
                case var _ when CommandRegex.BookingByRoomName.IsMatch(command):
                    Command.BookingByRoomName(command, bookingController);
                    break;
                case var _ when CommandRegex.CancelBooking.IsMatch(command):
                    Command.CancelBooking(command, bookingController);
                    break;
                case var _ when CommandRegex.Report.IsMatch(command):
                    Command.Report(command, reportController);
                    break;
                case var _ when CommandRegex.ListRooms.IsMatch(command):
                    Command.ListRooms(command, roomController);
                    break;
                case var _ when CommandRegex.Exit.IsMatch(command):
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Command not found");
                    break;
            }
        }
    }
}