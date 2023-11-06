using Untitled.Core.Models;

namespace Untitled.Core.Repository;

public interface IRoomRepository
{
    public void Add(string name);
    public Room Get(int id);
    public Room Get(string name);
    public IEnumerable<Room> GetAll();
}