using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Persistence.Repositories;

namespace RoomBookingApp.Persistence.Tests
{
    public class RoomBookingServiceTest
    {
        [Fact]
        public void ShouldReturnAvailableRooms()
        {
            //ARRANGE
            var date = new DateTime(2022,8, 16);
            var dbOptions = new DbContextOptionsBuilder<RoomBookingAppContext>()
                .UseInMemoryDatabase("AvailableRoomsTest").Options;

            using var context = new RoomBookingAppContext(dbOptions);
            context.Add(new Room() { Id = 1, Name = "Room 1" });
            context.Add(new Room() { Id = 2, Name = "Room 2" });
            context.Add(new Room() { Id = 3, Name = "Room 3" });

            context.Add(new RoomBooking() { RoomId = 1, Date = date });
            context.Add(new RoomBooking() { RoomId = 2, Date = date.AddDays(-5) });

            context.SaveChanges();

            var roomBookingService = new RoomBookingService(context);

            //ACT
            var availableRooms = roomBookingService.GetAvailableRooms(date);

            Assert.Equal(2, availableRooms.Count());
            Assert.Contains(availableRooms, x => x.Id == 2);
            Assert.Contains(availableRooms, x => x.Id == 3);
            Assert.DoesNotContain(availableRooms, x => x.Id == 1);

        }

        [Fact]
        public void ShouldSaveRoomBooking()
        {
            var dbOptions = new DbContextOptionsBuilder<RoomBookingAppContext>()
                .UseInMemoryDatabase("SaveRoomBookingTest").Options;
            var roomBooking = new RoomBooking()
            {
                RoomId = 1,
                Date = new DateTime(2022, 09, 11)                
            };
            using var context = new RoomBookingAppContext(dbOptions);
            var roomBookingService = new RoomBookingService(context);
            roomBookingService.Save(roomBooking);

            //Make sure only one item was saved:
            var bookings = context.RoomBookings.ToList();
            var booking = Assert.Single(bookings);
            //Converm values:
            Assert.Equal(booking.RoomId, roomBooking.RoomId);
            Assert.Equal(booking.Date, roomBooking.Date);
        }
    }
}