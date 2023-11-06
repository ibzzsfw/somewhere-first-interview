import Hotel from "./models/Hotel";

class Service {
  private static _instance: Service;
  private _hotel: Hotel;
  private command: Map<string, (params: Array<string>) => void>

  private constructor() {
    this._hotel = Hotel.instance
    this.command = new Map()
    this.add('create room', (params: Array<string>) => this._hotel.createRoom(params[0]))
    this.add('book', (params: Array<string>) => this._hotel.book(parseInt(params[0]), parseInt(params[1]), parseInt(params[2])))
    this.add('cancel', (params: Array<string>) => this._hotel.cancel(parseInt(params[0])))
    this.add('report', () => this._hotel.report())
  }

  static get instance(): Service {
    if (!Service._instance) {
      Service._instance = new Service();
    }
    return Service._instance;
  }

  add = (name: string, processor: any) => this.command.set(name, processor)
  remove = (name: string) => delete this.command[name]
  update = (name: string, processor: any) => this.add(name, processor)

  process = (command: string) => {
    const commandList: Array<string> = Array.from(this.command.keys())
    const commandName: string | undefined = commandList.find(name => command.startsWith(name))
    if (commandName) {
      const params: Array<string> = command.replace(commandName, '').trim().split(' ')
      this.command.get(commandName)?.(params)
    }
  }
}

export default Service;