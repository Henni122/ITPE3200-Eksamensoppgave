using StudyRoomBooking.Web.Models;

namespace StudyRoomBooking.Web.Data;


/*
Repository for room data. Handles all database operations related to rooms
and wraps them in try/catch so that controllers don't have to deal with
database exceptions directly
*/
public class RoomRepository : IRoomRepository
{
    // EF Core database - gives us access to the Rooms table
    private readonly ApplicationDbContext _db;
    // Logger for recording errors during database operation
    private readonly ILogger<RoomRepository> _logger;

    public RoomRepository(ApplicationDbContext db, ILogger<RoomRepository> logger)
    {
        _db = db;
        _logger = logger; 
    }

    // Return all rooms from the database 
    // Returns null if the query fails
       public IEnumerable<Room>? GetAll()
    {
        try
        {
            return _db.Rooms.ToList();
        }
        catch (Exception e)
        {
            _logger.LogError("[RoomRepository] Rooms ToList() failed when GetAll(), error message: {e}", e.Message);
            return null;
        }
    }

    // Return a room with the given ID
    // Or null if it does not exist, or the query fails
    public Room? GetRoomById(int id)
    {
        try
        {
            return _db.Rooms.Find(id);
        }
        catch (Exception e)
        {
            _logger.LogError("[RoomRepository] Find(id) failed when GetRoomById from RoomId {RoomId:0000}, error message: {e}", id, e.Message);
            return null;
        }
    }

    // Adds a new room to the database
    public bool Create(Room room)
    {
        try
        {
            _db.Rooms.Add(room);
            _db.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("[RoomRepository] room creation failed for room {@room}, error message: {e}", room, e.Message);
            return false;
        }
    }

    // Updates an existing room in the database
    public bool Update(Room room)
    {
        try
        {
            _db.Rooms.Update(room);
            _db.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("[RoomRepository] Room upadte failed for room {@room}, error message: {e}", room, e.Message);
            return false; 
        }
    }


    // Deletes the room with the given ID 
    // Fails if the room does not exist or the deletion fails
    public bool Delete(int id)
    {
        try
        {
            var room = _db.Rooms.Find(id);
            if (room == null)
            {
                _logger.LogError("[RoomRepository] room not found for the RoomId {RoomId:0000}", id);
                return false;
            }

            _db.Rooms.Remove(room);
            _db.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("[RoomRepository] room deletion failed for the RoomId {RoomId:0000}, error message: {e}", id, e.Message);
            return false;
        }
    }
}
