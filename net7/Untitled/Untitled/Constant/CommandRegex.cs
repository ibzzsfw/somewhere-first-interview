using System.Text.RegularExpressions;

namespace Untitled.Constant;

public static partial class CommandRegex
{
    public static readonly Regex CreateRoom = CreateRoomRegex();
    public static readonly Regex BookingByRoomId = BookingByRoomIdRegex();
    public static readonly Regex BookingByRoomName = BookingByRoomNameRegex();
    public static readonly Regex CancelBooking = CancelBookingRegex();
    public static readonly Regex Report = ReportRegex();
    public static readonly Regex ListRooms = ListRoomsRegex();
    public static readonly Regex Exit = ExitRegex();

    [GeneratedRegex("^create room (?<name>.+)$", RegexOptions.Compiled)]
    private static partial Regex CreateRoomRegex();

    [GeneratedRegex("^book (?<roomId>\\d+) (?<checkIn>.+) (?<checkOut>.+)$", RegexOptions.Compiled)]
    private static partial Regex BookingByRoomIdRegex();

    [GeneratedRegex(("^book (?<roomName>.+) (?<checkIn>.+) (?<checkOut>.+)$"), RegexOptions.Compiled)]
    private static partial Regex BookingByRoomNameRegex();

    [GeneratedRegex("^cancel booking (?<bookingId>\\d+)$", RegexOptions.Compiled)]
    private static partial Regex CancelBookingRegex();

    [GeneratedRegex("^report$", RegexOptions.Compiled)]
    private static partial Regex ReportRegex();

    [GeneratedRegex("^list rooms$", RegexOptions.Compiled)]
    private static partial Regex ListRoomsRegex();

    [GeneratedRegex("^exit$", RegexOptions.Compiled)]
    private static partial Regex ExitRegex();
}