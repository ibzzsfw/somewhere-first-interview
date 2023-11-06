using Untitled.Core.Models;

namespace Untitled.Core.Repository;

public class BookingRepository : IBookingRepository
{
    private readonly List<Booking> _activeBookings = new();
    private readonly List<Booking> _cancelledBookings = new();

    private bool IsAvailable(int roomId, DateTime checkIn, DateTime checkOut)
    {
        foreach (var activeBooking in _activeBookings.Where(activeBooking => activeBooking.RoomId == roomId))
        {
            if (checkIn >= activeBooking.CheckIn && checkIn <= activeBooking.CheckOut)
            {
                return false;
            }

            if (checkOut >= activeBooking.CheckIn && checkOut <= activeBooking.CheckOut)
            {
                return false;
            }
        }

        return true;
    }

    private int GetCurrentId()
    {
        return _activeBookings.Count + _cancelledBookings.Count + 1;
    }

    public void AddToActiveList(int roomId, DateTime checkIn, DateTime checkOut)
    {
        if (!IsAvailable(roomId, checkIn, checkOut))
        {
            throw new ArgumentException($"Room with id {roomId} is not available");
        }

        var booking = new Booking
        {
            Id = GetCurrentId(),
            RoomId = roomId,
            CheckIn = checkIn,
            CheckOut = checkOut
        };

        _activeBookings.Add(booking);
    }

    public void RemoveFromActiveList(int bookingId)
    {
        var booking = _activeBookings.FirstOrDefault(b => b.Id == bookingId);
        if (booking == null)
        {
            throw new ArgumentException($"Booking with id {bookingId} does not exist");
        }

        _activeBookings.Remove(booking);
    }

    public void AddToCancelledList(int roomId, DateTime checkIn, DateTime checkOut)
    {
        var booking = new Booking
        {
            Id = GetCurrentId(),
            RoomId = roomId,
            CheckIn = checkIn,
            CheckOut = checkOut
        };

        _cancelledBookings.Add(booking);
    }

    public void RemoveFromCancelledList(int bookingId)
    {
        var booking = _cancelledBookings.FirstOrDefault(b => b.Id == bookingId);
        if (booking == null)
        {
            throw new ArgumentException($"Booking with id {bookingId} does not exist");
        }

        _cancelledBookings.Remove(booking);
    }

    public List<Booking> GetActiveBookingByRoom(int roomId)
    {
        return _activeBookings.Where(b => b.RoomId == roomId).ToList();
    }

    public Booking Get(int bookingId)
    {
        var booking = _activeBookings.FirstOrDefault(b => b.Id == bookingId);
        if (booking == null)
        {
            throw new ArgumentException($"Booking with id {bookingId} does not exist");
        }

        return booking;
    }
}