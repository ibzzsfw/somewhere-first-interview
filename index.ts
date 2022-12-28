import Service from './service';

const service = new Service();

let commands = `7
create room Suite
book 1 5 10
create room Deluxe
book 2 1 10
book 1 12 18
book 2 20 25
cancel 4
report`

const commandQueue = commands.split('\n');

commandQueue.shift(); // Remove first line

commandQueue.forEach(service.processCommand)