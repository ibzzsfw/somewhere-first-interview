using Untitled.Constant;
using Untitled.Controllers;

namespace Untitled.Helper;

public static class Command
{
    public static void CreateRoom(string command, RoomController controller)
    {
        var name = CommandRegex.CreateRoom.Match(command).Groups["name"].Value;

        controller.Create(name);
    }

    public static void BookingByRoomId(string command, BookingController controller)
    {
        var match = CommandRegex.BookingByRoomId.Match(command);

        var roomId = int.Parse(match.Groups["roomId"].Value);
        var checkIn = DateHelper.ToDateTime(match.Groups["checkIn"].Value);
        var checkOut = DateHelper.ToDateTime(match.Groups["checkOut"].Value);

        controller.Book(roomId, checkIn, checkOut);
    }

    public static void BookingByRoomName(string command, BookingController controller)
    {
        var match = CommandRegex.BookingByRoomName.Match(command);

        var roomName = match.Groups["roomName"].Value;
        var checkIn = DateHelper.ToDateTime(match.Groups["checkIn"].Value);
        var checkOut = DateHelper.ToDateTime(match.Groups["checkOut"].Value);

        controller.Book(roomName, checkIn, checkOut);
    }

    public static void CancelBooking(string command, BookingController controller)
    {
        var bookingId = int.Parse(CommandRegex.CancelBooking.Match(command).Groups["bookingId"].Value);

        controller.Cancel(bookingId);
    }

    public static void Report(ReportController controller)
    {
        controller.Report();
    }

    public static void ListRooms(RoomController controller)
    {
        controller.List();
    }
}