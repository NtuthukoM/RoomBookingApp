using RoomBookingApp.Core.Models;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingRequestProcessor
    {
        public RoomBookingResult BookRoom(RoomBookingRequest request)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var result = new RoomBookingResult()
            {
                FullName = request.FullName,
                EmailAddress = request.EmailAddress,
                Date = request.Date
            };
            return result;
        }
    }
}