using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Persistence.Repositories
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly RoomBookingAppContext context;

        public RoomBookingService(RoomBookingAppContext context)
        {
            this.context = context;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {
            return context.Rooms.Where(x =>
                !x.RoomBookings.Any(r => r.Date == date)).ToList();
        }

        public void Save(RoomBooking roomBooking)
        {
            context.RoomBookings.Add(roomBooking);
            context.SaveChanges();
        }
    }
}
