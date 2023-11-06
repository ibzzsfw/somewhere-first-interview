using Untitled.Core.Repository;

namespace Untitled.Core.Services;

public class ReportService : IReportService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IBookingRepository _bookingRepository;

    public ReportService(IRoomRepository roomRepository, IBookingRepository bookingRepository)
    {
        _roomRepository = roomRepository;
        _bookingRepository = bookingRepository;
    }

    public void Report()
    {
        /*
         * Format:
         *
         * <Room name>
         * case: has no booking
         * No booking
         * case: has booking
         * Booking Id <booking id>: <check in date> -> <check out date>
         * Booking Id <booking id>: <check in date> -> <check out date>
         */

        foreach (var room in _roomRepository.GetAll())
        {
            Console.WriteLine(room.Name);
            var bookings = _bookingRepository.GetActiveBookingByRoom(room.Id);
            if (bookings.Count == 0)
            {
                Console.WriteLine("No booking");
            }
            else
            {
                foreach (var booking in bookings)
                {
                    Console.WriteLine($"Booking Id {booking.Id}: {booking.CheckIn} -> {booking.CheckOut}");
                }
            }
        }
    }
}