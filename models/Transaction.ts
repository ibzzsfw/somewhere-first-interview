import Booking from './Booking'

/**
 * @class Transaction
 * @description Store all transactions
 */
class Transaction {
  private static instance : Transaction
  private activeBooking: Booking[]
  private canceledBooking: Booking[]

  private constructor() {
    this.activeBooking = []
    this.canceledBooking = []
  }

  static getInstance() {
    if (!Transaction.instance) {
      Transaction.instance = new Transaction()
    }
    return Transaction.instance
  }

  bookingAmount = () => this.activeBooking.length + this.canceledBooking.length

  isRoomAvailble = (roomId: number, checkIn: number, checkOut: number) => {
    for (const booking of this.activeBooking) {
      if (
        booking.getRoomId() === roomId &&
        !(checkOut <= booking.getCheckIn() ||
          checkIn >= booking.getCheckOut())
      ) {
        return false;
      }
    }
    return true;
  }

  book = (roomId: number, checkIn: number, checkOut: number) => {

    if (this.isRoomAvailble(roomId, checkIn, checkOut)) {
      this.activeBooking.push(new Booking(this.bookingAmount() + 1, roomId, checkIn, checkOut))
    }
  }

  cancel = (bookingId: number) => {
    const booking: Booking | undefined = this.activeBooking.filter(booking => booking.getId() == bookingId)[0]

    if (booking) {
      this.canceledBooking.push(booking)
      this.activeBooking = this.activeBooking.filter(booking => booking.getId() != bookingId)
    }
  }

  report = () => {

    let report: string = ''

    for (const booking of this.activeBooking) {
      report += `Booking ${booking.getId()} : ${booking.getRoomId()} ${booking.getCheckIn()} ${booking.getCheckOut()}`
    }

    console.log(report)
  }


}

export default Transaction