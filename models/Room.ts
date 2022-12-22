class Room {
  private id: number
  private name: string

  constructor(id: number, name: string) {
    this.id = id
    this.name = name
  }

  getId = () => this.id
  getName = () => this.name
}

export default Room