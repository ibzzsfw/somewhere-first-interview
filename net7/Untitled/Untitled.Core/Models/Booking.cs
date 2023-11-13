namespace Untitled.Core.Models;

public class Booking
{
    public int Id { get; init; }
    public int RoomId { get; init; }
    public DateTime CheckIn { get; init; }
    public DateTime CheckOut { get; init; }
}

public class UnprocessedBooking
{
    public int RoomId { get; init; }
    public DateTime CheckIn { get; init; }
    public DateTime CheckOut { get; init; }
}