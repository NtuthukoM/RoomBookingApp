using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using System.Diagnostics;
using System.Reflection;

namespace RoomBookingApp.Core.Test
{
    public class RoomBookingRequestProcessorTest
    {
        private RoomBookingRequestProcessor processor;

        public RoomBookingRequestProcessorTest()
        {
            processor = new RoomBookingRequestProcessor();
        }

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
    }

}