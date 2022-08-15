using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomBookingsController : ControllerBase
    {
        private readonly IRoomBookingRequestProcessor requestProcessor;

        public RoomBookingsController(IRoomBookingRequestProcessor requestProcessor)
        {
            this.requestProcessor = requestProcessor;
        }

        [HttpPost]
        public async Task<IActionResult> BookRoom(RoomBookingRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = requestProcessor.BookRoom(request);
                if(result.Flag == BookingSuccessFlag.Success)
                {
                    return Ok(result);
                }
                ModelState.AddModelError(nameof(request.Date), "Invalid date request");
            }
            return BadRequest(ModelState);
            
        }
    }
}
