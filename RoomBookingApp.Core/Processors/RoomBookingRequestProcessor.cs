using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingRequestProcessor
    {
        private IRoomBookingService roomBookingService;

        public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
        {
            this.roomBookingService = roomBookingService;
        }

        public RoomBookingResult BookRoom(RoomBookingRequest request)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var availableRooms = roomBookingService.GetAvailableRooms(request.Date);
            if(availableRooms.Any())
            {
                roomBookingService.Save(CreateRoomBookingObject<RoomBooking>(request));
            }


            var result = CreateRoomBookingObject<RoomBookingResult>(request);
            return result;
        }

        private T CreateRoomBookingObject<T>(RoomBookingRequest bookingRequest)
            where T : RoomBookingBase, new()
        {
            return new T()
            {
                FullName = bookingRequest.FullName,
                EmailAddress = bookingRequest.EmailAddress,
                Date = bookingRequest.Date
            };
        }
    }
}