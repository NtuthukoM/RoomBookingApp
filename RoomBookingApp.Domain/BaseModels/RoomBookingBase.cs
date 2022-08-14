using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Domain.BaseModels
{
    public abstract class RoomBookingBase
    {
        public string FullName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = String.Empty;
        public DateTime Date { get; set; }
    }
}
