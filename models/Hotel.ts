import Room from './Room'
import RoomManager from './RoomManager'
import Transaction from './Transaction'

/**
 * @class Hotel
 * @description Facade class for Hotel management system
 * @returns {Hotel}
 */
class Hotel {
  private static instance: Hotel
  private roomManager: RoomManager
  private transaction: Transaction

  private constructor() {
    this.roomManager = RoomManager.getInstance()
    this.transaction = Transaction.getInstance()
  }

  static getInstance() {
    if (!Hotel.instance) {
      Hotel.instance = new Hotel()
    }
    return Hotel.instance
  }

  createRoom = (name: string) => this.roomManager.create(name)

  book = (roomId: number, checkIn: number, checkOut: number) => {

    const room: Room | undefined = this.roomManager.search(roomId)

    if (room) {
      this.transaction.book(roomId, checkIn, checkOut)
    }
  }

  cancel = (bookingId: number) => this.transaction.cancel(bookingId)

  report = () => {

    // show active booking by room name
    this.roomManager.getRoomList().map(room => {

      let { id, name } = room
      console.log(`Room: ${name}`)

      let booking = this.transaction.searchActiveBooking(id)

      if (booking.length > 0) {
        booking.map(booking => {
          console.log(`Booking Id ${booking.getId()}: ${booking.getCheckIn()} -> ${booking.getCheckOut()}`)
        })
      }
    })
  }

}

export default Hotel