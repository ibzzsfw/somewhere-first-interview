import Room from "./Room";

/**
 * @class Catalog
 * @description Store hotel list
 */

class RoomManager {
  private static instance: RoomManager
  private room: Room[]

  private constructor() {
    this.room = []
  }

  static getInstance() {
    if (!RoomManager.instance) {
      RoomManager.instance = new RoomManager();
    }
    return RoomManager.instance;
  }

  getRoomAmount = () => this.room.length

  search = (roomId: number) => this.room.filter(room => room.getId() == roomId)[0]
  
  create = (name: string) => {

    let room: Room = this.room.filter(room => room.getName() == name)[0]

    if (!room) {
      this.room.push(new Room(this.getRoomAmount() + 1, name))
    }
  }

}

export default RoomManager