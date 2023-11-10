namespace Untitled.Core.Services;

public interface IRoomService
{
    public void Create(string name);
    public void CreateMany(List<string> names);
    public void Delete(string name);
    public void DeleteMany(List<string> names);
    public void List();
}