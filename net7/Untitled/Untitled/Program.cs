using System.Text.RegularExpressions;
using Untitled;
using Untitled.Constant;
using Untitled.Controllers;
using Untitled.Helper;

var serviceProvider = ServiceProviderWrapper.GetServiceProvider();

var roomController = ServiceProviderWrapper.GetService<RoomController>(serviceProvider);
var bookingController = ServiceProviderWrapper.GetService<BookingController>(serviceProvider);
var reportController = ServiceProviderWrapper.GetService<ReportController>(serviceProvider);

var commands = new Dictionary<Regex, Action<string>>
{
    { CommandRegex.CreateRoom, s => Command.CreateRoom(s, roomController) },
    { CommandRegex.BookingByRoomId, s => Command.BookingByRoomId(s, bookingController) },
    { CommandRegex.BookingByRoomName, s => Command.BookingByRoomName(s, bookingController) },
    { CommandRegex.CancelBooking, s => Command.CancelBooking(s, bookingController) },
    { CommandRegex.Report, s => Command.Report(reportController) },
    { CommandRegex.ListRooms, s => Command.ListRooms(roomController) },
    {
        CommandRegex.Exit, _ =>
        {
            Console.WriteLine("End");
            Environment.Exit(0);
        }
    }
};

while (true)
{
    var command = Console.ReadLine();
    if (command is null)
    {
        continue;
    }

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