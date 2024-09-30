using AutoMapper;
using Azure.Core;
using TestTask.Domain.Models;
using TestTask.Domain.Ropositories;
using TestTask.Domain.Ropositories.Interfaces;
using TestTask.DtoModels;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class TimerService : ITimerService
    {
        private readonly ITimerWebHookRepository _repository;
        private readonly IMapper _mapper;

        public TimerService(ITimerWebHookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Guid> AddTimerWebHookAsync(TimerWebHookDto timerDto)
        {
            var timer = _mapper.Map<TimerWebHookDto, TimerWebHook>(timerDto);
            timer.Id = Guid.NewGuid();
            var triggerTime = DateTime.UtcNow.AddHours(timerDto.Hours)
                                        .AddMinutes(timerDto.Minutes)
                                        .AddSeconds(timerDto.Seconds);
            timer.Hours = triggerTime.Hour;
            timer.Minutes = triggerTime.Minute;
            timer.Seconds = triggerTime.Second;
            timer.IsCompleted = false;
            await _repository.AddTimerAsync(timer);

            return timer.Id;
        }

        public async Task<int> GetSecondsLeftAsync(Guid id)
        {
            var timer = await _repository.GetTimerWebHookAsync(id);
            var triggeredTime = new TimeSpan(timer.Hours, timer.Minutes, timer.Seconds);
            var nowTime = DateTime.UtcNow.TimeOfDay;

            var timeDifference = triggeredTime - nowTime;

            if (timeDifference.TotalSeconds < 0)
            {
                return 0;
            }

            return (int)timeDifference.TotalSeconds;
        }
    }
}
