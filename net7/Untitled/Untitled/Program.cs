using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Untitled.Constant;
using Untitled.Controllers;
using Untitled.Core.Repository;
using Untitled.Core.Services;
using Untitled.Helper;

namespace Untitled;

internal static class Program
{
    private static void Main()
    {
        var serviceProvider = AddService.GetServiceProvider();

        var roomController = ServiceProviderHelper.GetService<RoomController>(serviceProvider);
        var bookingController = ServiceProviderHelper.GetService<BookingController>(serviceProvider);
        var reportController = ServiceProviderHelper.GetService<ReportController>(serviceProvider);

        while (true)
        {
            var command = Console.ReadLine();
            if (command is null)
            {
                continue;
            }

            var commands = new Dictionary<Regex, Action<string>>
            {
                { CommandRegex.CreateRoom, s => Command.CreateRoom(s, roomController) },
                { CommandRegex.BookingByRoomId, s => Command.BookingByRoomId(s, bookingController) },
                { CommandRegex.BookingByRoomName, s => Command.BookingByRoomName(s, bookingController) },
                { CommandRegex.CancelBooking, s => Command.CancelBooking(s, bookingController) },
                { CommandRegex.Report, s => Command.Report(s, reportController) },
                { CommandRegex.ListRooms, s => Command.ListRooms(s, roomController) },
                {
                    CommandRegex.Exit, _ =>
                    {
                        Console.WriteLine($"End {new DateTime():yyyy-MM-dd HH:mm:ss}");
                        Environment.Exit(0);
                    }
                }
            };

            foreach
            (
                var commandAction in commands
                    .Where
                    (
                        commandAction => commandAction.Key.IsMatch(command)
                    )
            )
            {
                commandAction.Value(command);
                break;
            }

            if (!commands.Keys.Any(regex => regex.IsMatch(command)))
            {
                Console.WriteLine("Command not found");
            }
        }
    }
}