using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Models;
using TestTask.Domain.Ropositories.Interfaces;

namespace TestTask.Domain.Ropositories
{
    public class TimerWebHookRepository : ITimerWebHookRepository
    {
        private TimerDBContext _dbContext;

        public TimerWebHookRepository(TimerDBContext timerDBContext)
        {
            _dbContext = timerDBContext;
        }

        public async Task AddTimerAsync(TimerWebHook timer)
        {
            _dbContext.TimerWebHooks.Add(timer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CompleteTimerAsync(Guid id)
        {
            var timer = await _dbContext.TimerWebHooks.FindAsync(id);
            if (timer != null)
            {
                timer.IsCompleted = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<TimerWebHook>> GetPendingTimersAsync()
        {
            return await _dbContext.TimerWebHooks.Where(t => !t.IsCompleted).ToListAsync();
        }

        public async Task<TimerWebHook> GetTimerWebHookAsync(Guid id)
        {
            return await _dbContext.TimerWebHooks.FindAsync(id);
        }
    }
}
