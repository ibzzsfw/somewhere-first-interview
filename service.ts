import Hotel from "./models/Hotel"

const hotel: Hotel = Hotel.getInstance()

let commands = `7
create room Suite
book 1 5 10
create room Deluxe
book 2 1 10
book 1 12 18
book 2 20 25
cancel 4`

let commandQueue = commands.split('\n')

const mapCommand = (command: string) => {

  const [commandName, ...params] = command.split(' ')

  switch (commandName) {
    case 'create':
      hotel.createRoom(params[1])
      break
    case 'book':
      hotel.book(parseInt(params[0]), parseInt(params[1]), parseInt(params[2]))
      break
    case 'cancel':
      hotel.cancel(parseInt(params[0]))
      break
    default:
      break
  }
}

commandQueue.map(command => mapCommand(command))
hotel.report()