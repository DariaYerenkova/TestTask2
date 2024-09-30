using TestTask.Domain.Models;

namespace TestTask.Domain.Ropositories.Interfaces
{
    public interface ITimerWebHookRepository
    {
        Task<List<TimerWebHook>> GetPendingTimersAsync();
        Task AddTimerAsync(TimerWebHook timer);
        Task CompleteTimerAsync(Guid id);
        Task<TimerWebHook> GetTimerWebHookAsync(Guid id);
    }
}
