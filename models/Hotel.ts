import Room from './Room'
import Booking from './Booking'

class Hotel {
  private static instance: Hotel
  private room: Room[]
  private activeBooking: Booking[]
  private canceledBooking: Booking[]

  constructor() {
    this.room = []
    this.activeBooking = []
    this.canceledBooking = []
  }

  static getInstance() {
    if (!Hotel.instance) {
      Hotel.instance = new Hotel()
    }
    return Hotel.instance
  }

  getRoomAmount = () => this.room.length

  bookingAmount = () => this.activeBooking.length

  createRoom = (name: string) => {
    this.room.push(new Room(this.getRoomAmount() + 1, name))
  }

  isRoomAvailble = (roomId: number, checkIn: number, checkOut: number) => {

    const room: Room | undefined = this.room.find(room => room.getId() == roomId)

    if (!room) {
      return false
    }

    const booking: Booking | undefined = this.activeBooking.find((booking: Booking) => booking.getRoomId())

    if (!booking) {
      return true
    }

    return (checkIn > booking.getCheckOut() || checkOut < booking.getCheckIn())
  }

  book = (roomId: number, checkIn: number, checkOut: number) => {

    if (this.isRoomAvailble(roomId, checkIn, checkOut)) {
      this.activeBooking.push(
        new Booking(
          this.bookingAmount() + 1,
          roomId,
          checkIn,
          checkOut
        )
      )
    }
  }

  cancel = (bookingId: number) => {

    const booking: Booking | undefined = this.activeBooking.find(booking => booking.getId() == bookingId)

    if (booking) {
      this.canceledBooking.push(booking)
      this.activeBooking = this.activeBooking.filter(booking => booking.getId() != bookingId)
    }
  }

  report = () => {

    this.room.map(room => {
      console.log(`Room: ${room.getName()}`)

      this.activeBooking
        .filter(booking => booking.getRoomId() == room.getId())
        .map(booking => {
          console.log(`Booking Id ${booking.getId()}: ${booking.getCheckIn()} -> ${booking.getCheckOut()}`)
        })
    })
  }
}

export default Hotel