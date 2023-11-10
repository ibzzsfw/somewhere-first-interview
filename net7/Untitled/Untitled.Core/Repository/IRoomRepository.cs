using Untitled.Core.Models;

namespace Untitled.Core.Repository;

/// <summary>
/// Not allow inbound calls for multiple access
/// </summary>
public interface IRoomRepository
{
    public void Add(string name);
    public Room Get(int id);
    public Room Get(string name);
    public IEnumerable<Room> GetAll();
    public void Remove(string name);
}