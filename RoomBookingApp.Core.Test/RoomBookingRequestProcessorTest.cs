using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using System.Diagnostics;
using System.Reflection;

namespace RoomBookingApp.Core.Test
{
    public class RoomBookingRequestProcessorTest
    {
        private RoomBookingRequestProcessor processor;
        private RoomBookingRequest request;
        private Mock<IRoomBookingService> roomBookingServiceMock;
        private List<Room> availableRooms;

        public RoomBookingRequestProcessorTest()
        {            
            request = new RoomBookingRequest()
            {
                FullName = "Test name",
                EmailAddress = "test@example.com",
                Date = new DateTime(2022, 8, 15)
            };
            availableRooms = new List<Room>()
            {
                new Room() { Id = 1}
            };
            roomBookingServiceMock = new Mock<IRoomBookingService>();
            roomBookingServiceMock.Setup(a => a.GetAvailableRooms(request.Date))
                .Returns(availableRooms);
            processor = new RoomBookingRequestProcessor(roomBookingServiceMock.Object);
        }

        [Fact]
        public void ShouldReturnRoomBookingResponseWithValues()
        {
            //Act
            RoomBookingResult result = processor.BookRoom(request);

            //Assert
            Assert.NotNull(result);
            //result.ShouldNotBeNull();
            Assert.Equal(result.FullName, request.FullName);
            Assert.Equal(result.EmailAddress, request.EmailAddress);
            Assert.Equal(result.Date, request.Date);
        }

        [Fact]
        public void ShouldThrowExceptionForNullResult()
        {
            
            //Assert
            var exception = Assert.Throws<ArgumentNullException>(() => processor.BookRoom(null));            
            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveRoomBookingRequest()
        {
            RoomBooking booking = null;
            roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBooking>()))
            .Callback<RoomBooking>(x => {
                booking = x;
            });
            
            processor.BookRoom(request);

            roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Once);
            Assert.NotNull(booking);
            Assert.Equal(booking.FullName, request.FullName);
            Assert.Equal(booking.EmailAddress, request.EmailAddress);
            Assert.Equal(booking.Date, request.Date);
            Assert.Equal(booking.Id, availableRooms.First().Id);
        }

        [Fact]
        public void ShouldNotSaveRoomBookingRequestIfNoneAvailable()
        {
            availableRooms.Clear();
            processor.BookRoom(request);

            roomBookingServiceMock.Verify(a => a.Save(It.IsAny<RoomBooking>()), Times.Never);            
        }

        [Theory]
        [InlineData(BookingSuccessFlag.Success, true)]
        [InlineData(BookingSuccessFlag.Failure, false)]
        public void ShouldReturnSuccessOrFailureFlagInResult(BookingSuccessFlag flag, bool isAvailable)
        {
            if (!isAvailable)
            {
                availableRooms.Clear();
            }
            var result = processor.BookRoom(request);
            Assert.Equal(result.Flag, flag);
        }

    }

}