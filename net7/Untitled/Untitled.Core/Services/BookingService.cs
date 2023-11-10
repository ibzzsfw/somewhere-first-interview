using Serilog;
using Untitled.Core.Repository;

namespace Untitled.Core.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;

    public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
    }

    public void Book(int roomId, DateTime checkIn, DateTime checkOut)
    {
        try
        {
            _bookingRepository.AddToActiveList(roomId, checkIn, checkOut);
            Console.WriteLine($"Room with id {roomId} is booked from {checkIn} to {checkOut}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void Book(string roomName, DateTime checkIn, DateTime checkOut)
    {
        try
        {
            var room = _roomRepository.Get(roomName);
            Book(room.Id, checkIn, checkOut);
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
}