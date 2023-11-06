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
}