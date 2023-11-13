namespace Untitled.Core.Services;

public interface IBookingService
{
    public void Book(int roomId, DateTime checkIn, DateTime checkOut, bool enableUnprocessed = false);
    public void Book(string roomName, DateTime checkIn, DateTime checkOut, bool enableUnprocessed = false);
    public void Cancel(int bookingId);
}