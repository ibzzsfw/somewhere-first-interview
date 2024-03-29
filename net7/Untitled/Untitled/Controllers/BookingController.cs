using Untitled.Core.Services;

namespace Untitled.Controllers;

public class BookingController : IController
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public void Book(int roomId, DateTime checkIn, DateTime checkOut)
    {
        _bookingService.Book(roomId, checkIn, checkOut);
    }

    public void Book(string roomName, DateTime checkIn, DateTime checkOut)
    {
        _bookingService.Book(roomName, checkIn, checkOut);
    }

    public void Cancel(int bookingId)
    {
        _bookingService.Cancel(bookingId);
    }
}