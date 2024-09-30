using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Models;

namespace TestTask.Domain
{
    public class TimerDBContext: DbContext
    {
        public TimerDBContext(DbContextOptions<TimerDBContext> options): base(options)
        {

        }

        public TimerDBContext()
             : base(new DbContextOptionsBuilder<TimerDBContext>()
                 .UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings__DatabaseConnectionString"),
                     b => b.MigrationsAssembly("TestTask")).Options)
        {
        }

        public DbSet<TimerWebHook> TimerWebHooks { get; set; }
    }
}
