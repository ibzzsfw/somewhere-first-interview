using Untitled.Core.Models;
using Untitled.Core.Repository;

namespace Untitled.Core.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public void Create(string name)
    {
        _roomRepository.Add(name);
    }

    public void CreateMany(List<string> names)
    {
        var existingRooms = _roomRepository.GetAll().Select(r => r.Name).ToList();

        foreach (var name in names)
        {
            if (existingRooms.Contains(name))
            {
                Console.WriteLine($"Room {name} already exists");
            }
            else
            {
                _roomRepository.Add(name);
            }
        }
    }

    public void Delete(string name)
    {
        _roomRepository.Remove(name);
    }

    public void DeleteMany(List<string> names)
    {
        var existingRooms = _roomRepository.GetAll().Select(r => r.Name).ToList();

        foreach (var name in names)
        {
            if (existingRooms.Contains(name))
            {
                _roomRepository.Remove(name);
            }
            else
            {
                Console.WriteLine($"Room {name} does not exist");
            }
        }
    }

    public void List()
    {
        var rooms = _roomRepository.GetAll();
        var enumerable = rooms as Room[] ?? rooms.ToArray();
        if (!enumerable.Any())
        {
            Console.WriteLine("No rooms found");
        }
        else
        {
            foreach (var room in enumerable)
            {
                Console.WriteLine($"Room {room.Id}: {room.Name}");
            }
        }
    }
}