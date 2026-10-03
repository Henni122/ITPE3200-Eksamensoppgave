using Microsoft.AspNetCore.Mvc;
using StudyRoomBooking.Web.Data;
using StudyRoomBooking.Web.Models;

namespace StudyRoomBooking.Web.Controllers;

public class RoomController : Controller
{
    //Gives the controller access to the database
    private readonly ApplicationDbContext _applicationDbContext;

    public RoomController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    //READ - Shows all rooms
    public IActionResult Table()
    {
        //Gets all rooms from the database
        List<Room> rooms = _applicationDbContext.Rooms.ToList();

        //Shows the current page/view name
        ViewBag.CurrentViewName = "List of Rooms";

        return View(rooms);
    }

    //CREATE - Shows the form for creating a new room
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    //CREATE - Saves the new room to the database
    [HttpPost]
    public IActionResult Create(Room room)
    {
        //Checks if the submitted data is valid
        if (ModelState.IsValid)
        {
            // Adds the new room to the database
            _applicationDbContext.Rooms.Add(room);

            // Saves the changes to the database
            _applicationDbContext.SaveChanges();

            return RedirectToAction(nameof(Table));
        }

        //If the data is not valid, returns to the form
        return View(room);
    }

    //UPDATE - Finds the selected room and shows the Update form
    [HttpGet]
    public IActionResult Update(int id)
    {
        // Finds the room by id
        var room = _applicationDbContext.Rooms.Find(id);

        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    //UPDATE - Saves the updated room
    [HttpPost]
    public IActionResult Update(Room room)
    {
        //Checks if the submitted data is valid
        if (ModelState.IsValid)
        {
            // Updates the room in the database
            _applicationDbContext.Rooms.Update(room);

            // Saves the changes to the database
            _applicationDbContext.SaveChanges();

            return RedirectToAction(nameof(Table));
        }

        return View(room);
    }

    //DELETE - Shows the room before deletion
    [HttpGet]
    public IActionResult Delete(int id)
    {
        // Finds the room by id
        var room = _applicationDbContext.Rooms.Find(id);

        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    //DELETE - Removes the room from the database
    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        //Finds the room by id
        var room = _applicationDbContext.Rooms.Find(id);

        if (room == null)
        {
            return NotFound();
        }

        //Removes the room from the database
        _applicationDbContext.Rooms.Remove(room);

        //Saves the changes to the database
        _applicationDbContext.SaveChanges();

        return RedirectToAction(nameof(Table));
    }
}