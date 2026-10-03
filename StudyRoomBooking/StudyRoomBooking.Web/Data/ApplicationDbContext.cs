using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyRoomBooking.Web.Models;

namespace StudyRoomBooking.Web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<RoomBooking> RoomBookings => Set<RoomBooking>();
    public DbSet<BookingParticipant> BookingParticipants => Set<BookingParticipant>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        // Room 1 --- N RoomBooking
        b.Entity<RoomBooking>()
            .HasOne(x => x.Room).WithMany(r => r.Bookings)
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        // User 1 --- N RoomBooking (organizer)
        b.Entity<RoomBooking>()
            .HasOne(x => x.Organizer).WithMany(u => u.OrganizedBookings)
            .HasForeignKey(x => x.OrganizerId)
            .OnDelete(DeleteBehavior.Restrict);

        // RoomBooking 1 --- N BookingParticipant
        b.Entity<BookingParticipant>()
            .HasOne(x => x.RoomBooking).WithMany(r => r.Participants)
            .HasForeignKey(x => x.RoomBookingId)
            .OnDelete(DeleteBehavior.Cascade);

        // User 1 --- N BookingParticipant
        b.Entity<BookingParticipant>()
            .HasOne(x => x.User).WithMany(u => u.JoinedBookings)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // A user can only join a given booking once
        b.Entity<BookingParticipant>()
            .HasIndex(x => new { x.RoomBookingId, x.UserId })
            .IsUnique();

        // Speeds up conflict checks and course filtering
        b.Entity<RoomBooking>()
            .HasIndex(x => new { x.RoomId, x.StartTime, x.EndTime });
        b.Entity<RoomBooking>()
            .HasIndex(x => x.Course);

        // Seed data
        b.Entity<Room>().HasData(
            new Room { RoomId = 1, RoomName = "P35-101", Building = "Pilestredet 35", Capacity = 6 },
            new Room { RoomId = 2, RoomName = "P35-102", Building = "Pilestredet 35", Capacity = 10 },
            new Room { RoomId = 3, RoomName = "P52-201", Building = "Pilestredet 52", Capacity = 4 }
        );
    }
}