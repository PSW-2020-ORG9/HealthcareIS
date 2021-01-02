using Hospital.API.Services.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Hospital.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        public IActionResult FindRooms(IEnumerable<int> ids)
            => Ok(_roomService.GetRoomsByIds(ids));
    }
}
