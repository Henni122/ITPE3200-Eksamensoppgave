using StudyRoomBooking.Web.Models;

namespace StudyRoomBooking.Web.Data;


public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<RoomRepository> _logger;

    public RoomRepository(ApplicationDbContext db, ILogger<RoomRepository> logger)
    {
        _db = db;
        _logger = logger; 
    }


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
