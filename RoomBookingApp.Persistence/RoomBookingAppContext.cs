using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Persistence
{
    public class RoomBookingAppContext:DbContext
    {
        public RoomBookingAppContext(DbContextOptions<RoomBookingAppContext> options)
            :base(options)
        {

        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, Name = "Conference Room A" },
                new Room() { Id = 2, Name = "Conference Room B" },
                new Room() { Id = 3, Name = "Conference Room C" },
                new Room() { Id = 4, Name = "Conference Room D" },
                new Room() { Id = 5, Name = "Conference Room E" }
                );
        }

    }
}
