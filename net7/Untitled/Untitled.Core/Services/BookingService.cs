using Untitled.Core.Repository;

namespace Untitled.Core.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public void Book(int roomId, DateTime checkIn, DateTime checkOut)
    {
        try
        {
            _bookingRepository.AddToActiveList(roomId, checkIn, checkOut);
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
            _bookingRepository.AddToCancelledList(booking.RoomId, booking.CheckIn, booking.CheckOut);
            _bookingRepository.RemoveFromActiveList(bookingId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}