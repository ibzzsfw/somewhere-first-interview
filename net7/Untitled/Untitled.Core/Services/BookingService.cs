using Untitled.Core.Models;
using Untitled.Core.Repository;

namespace Untitled.Core.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly List<UnprocessedBooking> _unprocessedBookings = new();

    public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
    }

    public void Book(int roomId, DateTime checkIn, DateTime checkOut, bool enableUnprocessed = false)
    {
        var unprocessedBooking = new UnprocessedBooking
        {
            RoomId = roomId,
            CheckIn = checkIn,
            CheckOut = checkOut
        };

        try
        {
            _bookingRepository.AddToActiveList(unprocessedBooking);
            Console.WriteLine($"Room with id {roomId} is booked from {checkIn} to {checkOut}");
        }
        catch (Exception e)
        {
            if (enableUnprocessed)
            {
                _unprocessedBookings.Add(unprocessedBooking);
            }

            Console.WriteLine
            (
                $"Unprocessed booking for room with id {roomId} from {checkIn} to {checkOut} with error: {e.Message}"
            );
        }
    }

    public void Book(string roomName, DateTime checkIn, DateTime checkOut, bool enableUnprocessed = false)
    {
        try
        {
            var room = _roomRepository.Get(roomName);
            Book(room.Id, checkIn, checkOut, enableUnprocessed);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void Cancel(int bookingId)
    {
        try
        {
            var booking = _bookingRepository.Get(bookingId);
            _bookingRepository.AddToCancelledList(booking);
            _bookingRepository.RemoveFromActiveList(bookingId);
            Console.WriteLine($"Booking with id {bookingId} is cancelled");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void ReprocessUnprocessedBookings()
    {
        foreach (var unprocessedBooking in _unprocessedBookings)
        {
            _bookingRepository.AddToActiveList(unprocessedBooking);
            _unprocessedBookings.Remove(unprocessedBooking);
        }
    }
}