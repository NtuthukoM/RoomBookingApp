using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Core.Models
{
    public class RoomBookingRequest
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Date { get; set; }
    }
}
