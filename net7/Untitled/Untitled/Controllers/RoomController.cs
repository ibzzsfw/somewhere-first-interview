using Untitled.Core.Services;

namespace Untitled.Controllers;

public class RoomController : IController
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    public void Create(string name)
    {
        _roomService.Create(name);
    }

    public void List()
    {
        _roomService.List();
    }
}