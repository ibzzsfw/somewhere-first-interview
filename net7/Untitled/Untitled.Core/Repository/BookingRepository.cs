using Untitled.Core.Models;

namespace Untitled.Core.Repository;

public class BookingRepository : IBookingRepository
{
    private readonly List<Booking> _activeBookings = new();
    private readonly List<Booking> _cancelledBookings = new();
    private int _totalBookings;

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

    public void Upsert(Booking booking)
    {
        var bookingIndex = _activeBookings.FindIndex(b => b.Id == booking.Id);
        if (bookingIndex == -1)
        {
            _activeBookings.Add(booking);
        }
        else
        {
            _activeBookings[bookingIndex] = booking;
        }
    }

    public void AddToActiveList(int roomId, DateTime checkIn, DateTime checkOut)
    {
        if (!IsAvailable(roomId, checkIn, checkOut))
        {
            throw new ArgumentException($"Room with id {roomId} is not available");
        }

        var booking = new Booking
        {
            Id = ++_totalBookings,
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

    public void AddToCancelledList(int bookingId, int roomId, DateTime checkIn, DateTime checkOut)
    {
        var booking = new Booking
        {
            Id = bookingId,
            RoomId = roomId,
            CheckIn = checkIn,
            CheckOut = checkOut
        };

        _cancelledBookings.Add(booking);
    }

    public void AddToCancelledList(Booking booking)
    {
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

    public void Update(Booking booking)
    {
        var bookingIndex = _activeBookings.FindIndex(b => b.Id == booking.Id);
        if (bookingIndex == -1)
        {
            throw new ArgumentException($"Booking with id {booking.Id} does not exist");
        }

        _activeBookings[bookingIndex] = booking;
    }

    private void MoveToCancelledList(int bookingId)
    {
        var booking = _activeBookings.FirstOrDefault(b => b.Id == bookingId);
        if (booking == null)
        {
            throw new ArgumentException($"Booking with id {bookingId} does not exist");
        }

        _activeBookings.Remove(booking);
        _cancelledBookings.Add(booking);
    }

    private void RollbackCancelledList(int bookingId)
    {
        var booking = _cancelledBookings.FirstOrDefault(b => b.Id == bookingId);
        if (booking == null)
        {
            throw new ArgumentException($"Booking with id {bookingId} does not exist");
        }

        _cancelledBookings.Remove(booking);
        _activeBookings.Add(booking);
    }

    private void ClearCancelledList()
    {
        _cancelledBookings.Clear();
    }

    private void ClearActiveList()
    {
        _activeBookings.Clear();
    }

    private void Reset(bool resetId = false)
    {
        if (resetId)
        {
            _totalBookings = 0;
        }

        ClearActiveList();
        ClearCancelledList();
    }
}