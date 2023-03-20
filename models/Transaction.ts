import Booking from './Booking'

class Transaction {
  private static _instance: Transaction
  private _activeBooking: Array<Booking>
  private _canceledBooking: Array<Booking>

  private constructor() {
    this._activeBooking = new Array<Booking>()
    this._canceledBooking = new Array<Booking>()
  }

  static get instance(): Transaction {
    if (!Transaction._instance) {
      Transaction._instance = new Transaction()
    }
    return Transaction._instance
  }

  get length() {
    return this._activeBooking.length + this._canceledBooking.length
  }

  isRoomAvailble = (roomId: number, checkIn: number, checkOut: number) => {
    for (const booking of this._activeBooking) {
      if (
        booking.roomId === roomId &&
        !(checkOut <= booking.checkIn ||
          checkIn >= booking.checkOut)
      ) {
        return false;
      }
    }
    return true;
  }

  book = (roomId: number, checkIn: number, checkOut: number) => {

    if (this.isRoomAvailble(roomId, checkIn, checkOut)) {
      this._activeBooking.push(new Booking(this.length + 1, roomId, checkIn, checkOut))
    }
  }

  cancel = (bookingId: number) => {
    const booking: Booking | undefined = this.searchActiveBookingById(bookingId)

    if (booking) {
      this._canceledBooking.push(booking)
      this.removeFromActiveBooking(bookingId)
    }
  }

  searchActiveBookingById = (bookingId: number) => this._activeBooking.find(booking => booking.id == bookingId)

  searchActiveBooking = (roomId: number) => this._activeBooking.filter(booking => booking.roomId == roomId)

  removeFromActiveBooking = (bookingId: number) => {
    this._activeBooking = this._activeBooking.filter(booking => booking.id != bookingId)
  }
}

export default Transaction