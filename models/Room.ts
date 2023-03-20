import IRoom from "./IRoom"
class Room implements IRoom {
  private _id: number
  private _name: string

  constructor(id: number, name: string) {
    this._id = id
    this._name = name
  }

  get id() { return this._id }
  get name() { return this._name }

}

export default Room