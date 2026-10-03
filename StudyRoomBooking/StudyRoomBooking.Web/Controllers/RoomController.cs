using Microsoft.AspNetCore.Mvc;
using StudyRoomBooking.Web.Models;

namespace StudyRoomBooking.Web.Controllers;

public class RoomController : Controller
{
    public IActionResult Table()
    {
        var rooms = new List<Room>();
        var room1 = new Room
        {
            RoomId = 1,
            RoomName = "Pc Room",
            Capacity = 6,
            Building = "P35"
        };

        var room2 = new Room
        {
            RoomId = 2,
            RoomName = "Presentation Room",
            Capacity = 15,
            Building = "P34"
        };

        rooms.Add(room1);
        rooms.Add(room2);

        ViewBag.CurrentViewName = "List of Rooms";
        return View(rooms);
    }
}