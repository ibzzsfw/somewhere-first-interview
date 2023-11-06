/*
 * Command line arguments:
 * 1. Create a new room: create room <name>
 * 2. Create a new booking: book <room id> <check in date> <check out date>
 * 3. Cancel a booking: cancel <booking id>
 * 4. Report: report
 */


using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Untitled.Controllers;
using Untitled.Core.Repository;
using Untitled.Core.Services;

namespace Untitled;

internal static class Program
{
    private static void Main()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IRoomRepository, RoomRepository>();
        services.AddSingleton<IBookingRepository, BookingRepository>();
        services.AddSingleton<IBookingService, BookingService>();
        services.AddSingleton<IRoomService, RoomService>();
        services.AddSingleton<IReportService, ReportService>();
        services.AddSingleton<BookingController>();
        services.AddSingleton<RoomController>();
        services.AddSingleton<ReportController>();

        var serviceProvider = services.BuildServiceProvider();

        while (true)
        {
            var command = Console.ReadLine();
            var roomController = serviceProvider.GetService<RoomController>() ?? throw new NullReferenceException();
            var bookingController =
                serviceProvider.GetService<BookingController>() ?? throw new NullReferenceException();
            var reportController = serviceProvider.GetService<ReportController>() ?? throw new NullReferenceException();

            if (command == null)
            {
                continue;
            }

            var commandParts = command.Split(' ');
            if (commandParts.Length < 1)
            {
                Console.WriteLine("Invalid command");
                continue;
            }

            switch (commandParts[0])
            {
                case "create":
                    if (commandParts.Length != 3)
                    {
                        Console.WriteLine("Invalid command");
                        continue;
                    }

                    if (commandParts[1] == "room")
                    {
                        roomController.Create(commandParts[2]);
                    }
                    else
                    {
                        Console.WriteLine("Invalid command");
                    }

                    break;
                case "book":
                    if (commandParts.Length != 4)
                    {
                        Console.WriteLine("Invalid command");
                        continue;
                    }

                    bookingController.Book
                    (
                        int.Parse(commandParts[1]),
                        DateTime.ParseExact(commandParts[2], "yyyyMMdd", CultureInfo.InvariantCulture),
                        DateTime.ParseExact(commandParts[3], "yyyyMMdd", CultureInfo.InvariantCulture)
                    );
                    break;
                case "cancel":
                    if (commandParts.Length != 2)
                    {
                        Console.WriteLine("Invalid command");
                        continue;
                    }

                    bookingController.Cancel(int.Parse(commandParts[1]));
                    break;
                case "report":
                    reportController.Report();
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }
        }
    }
}