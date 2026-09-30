namespace StudyRoomBooking.Web.Models; 


/*
This class represents the physical group room on campus
*/


public class Room
{
    public int RoomId { get; set; }
    public required string RoomName { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Building { get; set; } = string.Empty;

    //Navigation property
    public ICollection<RoomBooking> Bookings { get ; set; } = new List<RoomBooking>();
}

