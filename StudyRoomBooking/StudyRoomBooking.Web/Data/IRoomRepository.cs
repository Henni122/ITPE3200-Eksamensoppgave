using StudyRoomBooking.Web.Models;

namespace StudyRoomBooking.Web.Data;

public interface IRoomRepository
{
    IEnumerable<Room>? GetAll();
    Room? GetRoomById(int id);
    bool Create(Room room);
    bool Update(Room room);
    bool Delete(int id);
}