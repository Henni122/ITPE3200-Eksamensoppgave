using Microsoft.AspNetCore.Mvc;
using StudyRoomBooking.Web.Data;
using StudyRoomBooking.Web.Models;

namespace StudyRoomBooking.Web.Controllers;

public class RoomController : Controller
{
    //Controller gets access to the database 
    private readonly ApplicationDbContext _applicationDbContext;

    public RoomController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public IActionResult Table()
    {
        //Get all rooms from database
        List<Room> rooms = _applicationDbContext.Rooms.ToList();

        //View shows current page/view name
        ViewBag.CurrentViewName = "List of Rooms";

        return View(rooms);
    }

    [HttpGet]
    public IActionResult Create()
    {
        //Shows form for creating a new room
        return View();
    }

    [HttpPost]
    public IActionResult Create(Room room)
    {
        //Check for valid data 
        if (ModelState.IsValid)
        {
            //Add new room to database
            _applicationDbContext.Rooms.Add(room);
            //save changes to database
            _applicationDbContext.SaveChanges();

            return RedirectToAction(nameof(Table));
        }
        //If data is not valid, return to theform 
        return View(room);
    }
}