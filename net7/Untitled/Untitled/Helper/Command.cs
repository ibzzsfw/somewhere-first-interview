using Untitled.Constant;
using Untitled.Controllers;

namespace Untitled.Helper;

public class Command
{
    private readonly RoomController _roomController;
    private readonly BookingController _bookingController;
    private readonly ReportController _reportController;

    public Command(IServiceProvider serviceProvider)
    {
        _roomController = ServiceProviderWrapper.GetService<RoomController>(serviceProvider);
        _bookingController = ServiceProviderWrapper.GetService<BookingController>(serviceProvider);
        _reportController = ServiceProviderWrapper.GetService<ReportController>(serviceProvider);
    }

    public void CreateRoom(string command)
    {
        var name = CommandRegex.CreateRoom.Match(command).Groups["name"].Value;

        _roomController.Create(name);
    }

    public void BookingByRoomId(string command)
    {
        var match = CommandRegex.BookingByRoomId.Match(command);

        var roomId = int.Parse(match.Groups["roomId"].Value);
        var checkIn = DateHelper.ToDateTime(match.Groups["checkIn"].Value);
        var checkOut = DateHelper.ToDateTime(match.Groups["checkOut"].Value);

        _bookingController.Book(roomId, checkIn, checkOut);
    }

    public void BookingByRoomName(string command)
    {
        var match = CommandRegex.BookingByRoomName.Match(command);

        var roomName = match.Groups["roomName"].Value;
        var checkIn = DateHelper.ToDateTime(match.Groups["checkIn"].Value);
        var checkOut = DateHelper.ToDateTime(match.Groups["checkOut"].Value);

        _bookingController.Book(roomName, checkIn, checkOut);
    }

    public void CancelBooking(string command)
    {
        var bookingId = int.Parse(CommandRegex.CancelBooking.Match(command).Groups["bookingId"].Value);

        _bookingController.Cancel(bookingId);
    }

    public void Report()
    {
        _reportController.Report();
    }

    public void ListRooms()
    {
        _roomController.List();
    }
}