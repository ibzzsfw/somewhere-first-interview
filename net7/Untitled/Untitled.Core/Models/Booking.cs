namespace Untitled.Core.Models;

public class Booking
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
}