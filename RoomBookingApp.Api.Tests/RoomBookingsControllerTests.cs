using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomBookingApp.Api.Controllers;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Api.Tests
{
    public class RoomBookingsControllerTests
    {
        private Mock<IRoomBookingRequestProcessor> roomBookingService;
        private RoomBookingsController controller;
        private RoomBookingRequest request;
        private RoomBookingResult result;

        public RoomBookingsControllerTests()
        {
            roomBookingService = new Mock<IRoomBookingRequestProcessor>();
            controller = new RoomBookingsController(roomBookingService.Object);
            request = new RoomBookingRequest() {
                FullName = "Ntuthuko",
                Date = new DateTime(2022, 11, 1),
                EmailAddress = "test@example.com",                
            };
            result = new RoomBookingResult() 
            { 
                Flag = Core.Enums.BookingSuccessFlag.Success 
            };

            roomBookingService.Setup(x => x.BookRoom(request))
                .Returns(result);
        }

        [Theory]
        [InlineData(1, true, typeof(OkObjectResult), Core.Enums.BookingSuccessFlag.Success)]
        [InlineData(0, false, typeof(BadRequestObjectResult), Core.Enums.BookingSuccessFlag.Failure)]
        public async Task ShouldCallBookingMethodWhenValid(int expectedMethodCalls, 
            bool isModelValid,
            Type expectedActionResultType,
            Core.Enums.BookingSuccessFlag? flag)
        {
            //Arrange:
            if (!isModelValid)
            {
                controller.ModelState.AddModelError("key", "error message");
                result.Flag = Core.Enums.BookingSuccessFlag.Failure;
            }

            //Act:
            var response = await controller.BookRoom(request);

            roomBookingService.Verify(x =>
                x.BookRoom(It.IsAny<RoomBookingRequest>()), Times.Exactly(expectedMethodCalls));
            Assert.IsType(expectedActionResultType, response);
            Assert.Equal(result.Flag, flag);
        }
    }
}
