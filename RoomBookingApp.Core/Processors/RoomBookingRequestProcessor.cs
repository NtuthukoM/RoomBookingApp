using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Domain.BaseModels;

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
            var result = CreateRoomBookingObject<RoomBookingResult>(request);
            if (availableRooms.Any())
            {
                var room = availableRooms.First();
                var roomBooking = CreateRoomBookingObject<RoomBooking>(request);
                
                roomBookingService.Save(roomBooking);
                roomBooking.Id = room.Id;
                result.RoomBookingId = roomBooking.Id;
                result.Flag = Enums.BookingSuccessFlag.Success;
            }            


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