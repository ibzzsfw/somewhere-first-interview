using System.Text.RegularExpressions;
using Untitled;
using Untitled.Constant;
using Untitled.Helper;

var serviceProvider = ServiceProviderWrapper.GetServiceProvider();
var client = new Command(serviceProvider);

var commands = new Dictionary<Regex, Action<string>>
{
    { CommandRegex.CreateRoom, s => client.CreateRoom(s) },
    { CommandRegex.BookingByRoomId, s => client.BookingByRoomId(s) },
    { CommandRegex.BookingByRoomName, s => client.BookingByRoomName(s) },
    { CommandRegex.CancelBooking, s => client.CancelBooking(s) },
    { CommandRegex.Report, s => client.Report() },
    { CommandRegex.ListRooms, s => client.ListRooms() },
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