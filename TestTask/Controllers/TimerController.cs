using Microsoft.AspNetCore.Mvc;
using TestTask.DtoModels;
using TestTask.Services.Interfaces;

namespace TestTask.Controllers
{
    [ApiController]
    [Route("Timer")]
    public class TimerController : ControllerBase
    {
        private readonly ITimerService _timerService;

        public TimerController(ITimerService timerService)
        {
            _timerService = timerService;
        }

        [HttpPost("SetTimer")]
        public async Task<IActionResult> SetTimer([FromBody] TimerWebHookDto request)
        {
            if (request != null && !String.IsNullOrEmpty(request.WebHookUrl) && (request.Hours > 0 || request.Minutes > 0 || request.Seconds > 0))
            {
                var id = await _timerService.AddTimerWebHookAsync(request);

                return Ok(id);
            }

            return BadRequest();
        }

        [HttpGet("GetTimerStatus")]
        public async Task<IActionResult> GetTimerStatus([FromQuery]Guid id)
        {
            var secondsLeft = await _timerService.GetSecondsLeftAsync(id);
            return Ok(secondsLeft);
        }

    }

}
