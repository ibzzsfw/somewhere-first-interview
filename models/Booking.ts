class Booking {
  private _id: number
  private _roomId: number
  private _checkIn: number
  private _checkOut: number

  constructor(id: number, roomId: number, checkIn: number, checkOut: number) {
    this._id = id
    this._roomId = roomId
    this._checkIn = checkIn
    this._checkOut = checkOut
  }

  get id() { return this._id }
  get roomId() { return this._roomId }
  get checkIn() { return this._checkIn }
  get checkOut() { return this._checkOut }
}

export default Booking