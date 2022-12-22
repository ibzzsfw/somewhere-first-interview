class Booking {
  private id: number
  private roomId: number
  private checkIn: number
  private checkOut: number

  constructor(id: number, roomId: number, checkIn: number, checkOut: number) {
    this.id = id
    this.roomId = roomId
    this.checkIn = checkIn
    this.checkOut = checkOut
  }

  getId = () => this.id
  getRoomId = () => this.roomId
  getCheckIn = () => this.checkIn
  getCheckOut = () => this.checkOut
}

export default Booking