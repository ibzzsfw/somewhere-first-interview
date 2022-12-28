import Hotel from "./models/Hotel"

class Service {
  private hotel: Hotel;

  constructor() {
    this.hotel = Hotel.getInstance();
  }

  private commandProcessors = {
    create: (params: string[]) => this.hotel.createRoom(params[1]),
    book: (params: string[]) => this.hotel.book(parseInt(params[0]), parseInt(params[1]), parseInt(params[2])),
    cancel: (params: string[]) => this.hotel.cancel(parseInt(params[0])),
    report: () => this.hotel.report()
  };

  add = (name: string, processor: any) => this.commandProcessors[name] = processor
  remove = (name: string) => delete this.commandProcessors[name]
  update = (name: string, processor: any) => this.add(name, processor)
  
  processCommand = (command: string) => {
    const [commandName, ...params] = command.split(' ');
    const processor: any = this.commandProcessors[commandName];
    if (processor) {
      processor(params);
    } else {
      // Handle invalid command
    }
  };
}

export default Service;