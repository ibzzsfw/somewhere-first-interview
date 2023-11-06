namespace Untitled.Core.Services;

public interface IBookingService
{
    public void Book(int roomId, DateTime checkIn, DateTime checkOut);
    public void Cancel(int bookingId);
}