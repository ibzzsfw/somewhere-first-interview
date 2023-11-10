using Untitled.Core.Models;

namespace Untitled.Core.Repository;

public class RoomRepository : IRoomRepository
{
    private readonly Dictionary<string, Room> _rooms = new();

    private int GetCurrentId()
    {
        return _rooms.Count + 1;
    }

    public void Add(string name)
    {
        var room = new Room
        {
            Id = GetCurrentId(),
            Name = name
        };

        try
        {
            _rooms.Add(name, room);
        }
        catch (Exception e)
        {
            Console.WriteLine($"RoomRepository.Add: {e.Message}");
        }
    }

    private void AddMany(IEnumerable<string> names)
    {
        var existingRooms = new List<string>();

        foreach (var name in names)
        {
            if (_rooms.ContainsKey(name))
            {
                existingRooms.Add(name);
            }
            else
            {
                Add(name);
            }
        }

        if (existingRooms.Count > 0)
        {
            Console.WriteLine($"RoomRepository.AddMany: {string.Join(", ", existingRooms)} already exist");
        }
    }

    public Room Get(int id)
    {
        var room = _rooms.Values.FirstOrDefault(r => r.Id == id);
        if (room == null)
        {
            throw new ArgumentException($"Room with id {id} does not exist");
        }

        return room;
    }

    private List<Room> GetMany(IEnumerable<int> ids) => ids.Select(Get).ToList();

    public Room Get(string name)
    {
        var room = _rooms[name];
        if (room == null)
        {
            throw new KeyNotFoundException($"Room with name {name} does not exist");
        }

        return room;
    }

    private List<Room> GetMany(IEnumerable<string> names) => names.Select(Get).ToList();

    public IEnumerable<Room> GetAll()
    {
        return _rooms.Values;
    }

    public void Remove(string name)
    {
        try
        {
            _rooms.Remove(name);
        }
        catch (Exception e)
        {
            Console.WriteLine($"RoomRepository.Remove: {e.Message}");
        }
    }
}