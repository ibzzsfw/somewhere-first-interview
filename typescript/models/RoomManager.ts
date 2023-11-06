import IRoom from "./IRoom";
import Room from "./Room";

class RoomManager {
  private static _instance: RoomManager
  private _room: Map<string, Room>

  private constructor() {
    this._room = new Map<string, Room>()
  }

  static get instance(): RoomManager {
    if (!RoomManager._instance) {
      RoomManager._instance = new RoomManager();
    }
    return RoomManager._instance;
  }

  get room() {
    return Array.from(this._room.values()).map(room => {
      let { id, name } = room
      return { id, name }
    }) satisfies Array<IRoom>
  }

  searchByName = (name: string) => this._room.get(name)

  searchById = (id: number) => Array.from(this._room.values()).find(room => room.id === id)

  create = (name: string) => {

    let room: Room | undefined = this.searchByName(name)

    if (!room) {
      this._room.set(name, new Room(this._room.size + 1, name))
    }
  }
}

export default RoomManager