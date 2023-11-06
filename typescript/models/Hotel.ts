import IRoom from './IRoom'
import RoomManager from './RoomManager'
import Transaction from './Transaction'

class Hotel {
  private static _instance: Hotel
  private _roomManager: RoomManager
  private _transaction: Transaction

  private constructor() {
    this._roomManager = RoomManager.instance
    this._transaction = Transaction.instance
  }

  static get instance(): Hotel {
    if (!Hotel._instance) {
      Hotel._instance = new Hotel()
    }
    return Hotel._instance
  }

  createRoom = (name: string) => this._roomManager.create(name)

  book = (roomId: number, checkIn: number, checkOut: number) => {

    const room: IRoom | undefined = this._roomManager.searchById(roomId)

    if (room) {
      this._transaction.book(roomId, checkIn, checkOut)
    }
  }

  cancel = (bookingId: number) => this._transaction.cancel(bookingId)

  report = () => {

    this._roomManager.room.map(room => {

      let { id, name } = room
      console.log(`Room: ${name}`)

      let booking = this._transaction.searchActiveBooking(id)

      if (booking.length > 0) {
        booking.map(booking => {
          console.log(`Booking Id ${booking.id}: ${booking.checkIn} -> ${booking.checkOut}`)
        })
      } else {
        console.log('No Booking')
      }
    })
  }
}

export default Hotel