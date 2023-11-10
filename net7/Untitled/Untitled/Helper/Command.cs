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
        var roomId = int.Parse(CommandRegex.BookingByRoomId.Match(command).Groups["roomId"].Value);
        var checkIn = DateHelper.ToDateTime(CommandRegex.BookingByRoomId.Match(command).Groups["checkIn"].Value);
        var checkOut = DateHelper.ToDateTime(CommandRegex.BookingByRoomId.Match(command).Groups["checkOut"].Value);
        controller.Book(roomId, checkIn, checkOut);
    }

    public static void BookingByRoomName(string command, BookingController controller)
    {
        var roomName = CommandRegex.BookingByRoomName.Match(command).Groups["roomName"].Value;
        var checkIn = DateHelper.ToDateTime(CommandRegex.BookingByRoomId.Match(command).Groups["checkIn"].Value);
        var checkOut = DateHelper.ToDateTime(CommandRegex.BookingByRoomId.Match(command).Groups["checkOut"].Value);
        controller.Book(roomName, checkIn, checkOut);
    }

    public static void CancelBooking(string command, BookingController controller)
    {
        var bookingId = int.Parse(CommandRegex.CancelBooking.Match(command).Groups["bookingId"].Value);
        controller.Cancel(bookingId);
    }

    public static void Report(string command, ReportController controller)
    {
        controller.Report();
    }

    public static void ListRooms(string command, RoomController controller)
    {
        controller.List();
    }
}