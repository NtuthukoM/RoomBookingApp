using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Core.Test
{
    public class RoomBookingRequestProcessorTest
    {
        [Fact]
        public void ShouldReturnRoomBookingResponseWithValues()
        {
            //Arrange
            var request = new RoomBookingRequest()
            {
                FullName = "Test name",
                EmailAddress = "test@example.com",
                Date = new DateTime(2022, 8, 15)
            };


            var processor = new RoomBookingRequestProcessor();

            //Act
            RoomBookingResult result = processor.BookRoom(request);

            //Assert
            Assert.NotNull(result);
            //result.ShouldNotBeNull();
            Assert.Equal(result.FullName, request.FullName);
            Assert.Equal(result.EmailAddress, request.EmailAddress);
            Assert.Equal(result.Date, request.Date);
        }
    }

}