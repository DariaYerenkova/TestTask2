using TestTask.Domain.Models;
using TestTask.DtoModels;

namespace TestTask.Services.Interfaces
{
    public interface ITimerService
    {
        Task<Guid> AddTimerWebHookAsync(TimerWebHookDto Dto);
        Task<int> GetSecondsLeftAsync(Guid id);
    }
}
