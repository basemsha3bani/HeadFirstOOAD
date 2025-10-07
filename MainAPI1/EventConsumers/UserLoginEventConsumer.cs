using MassTransit;
using Utils.Events;

namespace LoggingAPI.EventConsumers
{
    public class UserLoginEventConsumer : IConsumer<UserLoginEvent>
    {
        private ILogger<string> _logger;

        public UserLoginEventConsumer(ILogger<string> logger)
        {
            _logger = logger;
        }


        public async Task Consume(ConsumeContext<UserLoginEvent> context)
        {
            _logger.LogInformation(context.Message.UserName,null);



        }
    }
}
