using Untitled.Core.Models;

namespace Untitled.Core.Repository;

public interface IBookingRepository
{
    public Booking Get(int bookingId);
    public void AddToActiveList(int roomId, DateTime checkIn, DateTime checkOut);
    public void RemoveFromActiveList(int bookingId);
    public void AddToCancelledList(int roomId, DateTime checkIn, DateTime checkOut);
    public void RemoveFromCancelledList(int bookingId);
    public List<Booking> GetActiveBookingByRoom(int roomId);
}