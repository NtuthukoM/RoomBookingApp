using Microsoft.Extensions.Logging;
using Moq;
using RoomBookingApp.Api.Controllers;

namespace RoomBookingApp.Api.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldReturnForecastResults()
        {
            var logger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(logger.Object);
            var result = controller.Get();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}