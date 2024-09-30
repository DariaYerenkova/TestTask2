using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.Models
{
    public class TimerWebHook
    {
        [Key]
        public Guid Id { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public string WebHookUrl { get; set; }
        public bool IsCompleted { get; set; }
    }
}
