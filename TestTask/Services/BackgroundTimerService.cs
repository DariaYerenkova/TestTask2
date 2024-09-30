
using RestSharp;
using TestTask.Domain.Ropositories.Interfaces;

namespace TestTask.Services
{
    public class BackgroundTimerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly RestClient _restClient;

        public BackgroundTimerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _restClient = new RestClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var _repository = scope.ServiceProvider.GetRequiredService<ITimerWebHookRepository>();

            while (!stoppingToken.IsCancellationRequested)
            {
                var pendingTimers = await _repository.GetPendingTimersAsync();

                foreach (var timer in pendingTimers)
                {
                    var triggeredTime = new TimeSpan(timer.Hours, timer.Minutes, timer.Seconds);
                    var nowTime = DateTime.UtcNow.TimeOfDay;

                    if (triggeredTime <= nowTime)
                    {
                        if (await SendWebHookAsync(timer.WebHookUrl))
                        {
                            await _repository.CompleteTimerAsync(timer.Id);
                        }                        
                    }
                }

            }
        }

        private async Task<bool> SendWebHookAsync(string url)
        {

            var request = new RestRequest(url, Method.Post);

            request.AddJsonBody(new { });
            var response = await _restClient.ExecuteAsync(request);

            return response.IsSuccessStatusCode;
        }
    }
}
