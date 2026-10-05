using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using StudyRoomBooking.Web.Data;
using StudyRoomBooking.Web.Models;

namespace StudyRoomBooking.Web.Controllers;

public class RoomController : Controller
{
    // Gives the controller access to the database
    // private readonly ApplicationDbContext _applicationDbContext;

    private readonly IRoomRepository _roomRepository;
    // Adding a logger to the constructor
    private readonly ILogger<RoomController> _logger;

    public RoomController(IRoomRepository roomRepository, ILogger<RoomController> logger)
    {
        // _applicationDbContext = applicationDbContext;
        _roomRepository = roomRepository;
        _logger = logger;
    }

    // READ - Shows all rooms
    public IActionResult Table()
    {
        var rooms = _roomRepository.GetAll();

        if (rooms == null)
        {
            _logger.LogError("[RoomController] Room list not found while executing _roomRepository.GetAll()");
            return NotFound("Room list not found");
        }
        
        ViewBag.CurrentViewName = "List of Rooms";
        return View(rooms);

    }

    // CREATE - Shows the form for creating a new room
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // CREATE - Saves the new room to the database
    [HttpPost]
    public IActionResult Create(Room room)
    {
        if (ModelState.IsValid)
        {
            bool returnOk = _roomRepository.Create(room);

            if (returnOk)
            {
                _logger.LogInformation("Created a new room with ID {RoomId}", room.RoomId);
                return RedirectToAction(nameof(Table));
            }
        }

        _logger.LogWarning("[RoomController] Room creation failed {@room}", room);
        return View(room);
    }

    // UPDATE - Finds the selected room and shows the Update form
    [HttpGet]
    public IActionResult Update(int id)
    {
        var room = _roomRepository.GetRoomById(id);

        if (room == null)
        {
            _logger.LogError("[RoomController] Room not found when updating the RoomId {RoomId:0000}", id);
            return BadRequest("Room not found for the RoomId");
        }
        
        return View(room);
    }



    // UPDATE - Saves the updated room
    [HttpPost]
    public IActionResult Update(Room room)
    {
        if (ModelState.IsValid)
        {
            bool returnOk = _roomRepository.Update(room);

            if (returnOk)
            {
                return RedirectToAction(nameof(Table));
            }
        }

        _logger.LogWarning("[RoomController] Room update failed {@room}", room);

        return View(room);
    }

    // DELETE - Shows the room before deletion
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var room = _roomRepository.GetRoomById(id);

        if (room == null)
        {
            _logger.LogError("[RoomController] Room not found for RoomId {RoomId:0000}", id);
            return BadRequest("Room not found for the RoomId. Failed to delete room");
        }

        return View(room);

    }

    // DELETE - Removes the room from the database
    [HttpPost, ActionName("DeleteConfirmed")]
    public IActionResult DeleteConfirmed(int id)
    {
        bool returnOk = _roomRepository.Delete(id);

        if (!returnOk)
        {
            _logger.LogError("[RoomController] Room deletion failed for the RoomId {RoomId:0000}", id);
            return BadRequest("Room deletion failed");
        }

        TempData["SuccessMessage"] = "Room was deleted";
        return RedirectToAction(nameof(Table));
    }
}