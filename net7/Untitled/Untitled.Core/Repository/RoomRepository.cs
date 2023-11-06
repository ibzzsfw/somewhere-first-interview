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

        _rooms.Add(name, room);
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

    public Room Get(string name)
    {
        var room = _rooms[name];
        if (room == null)
        {
            throw new ArgumentException($"Room with name {name} does not exist");
        }

        return room;
    }

    public IEnumerable<Room> GetAll()
    {
        return _rooms.Values;
    }
}