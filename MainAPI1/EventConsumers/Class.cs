using MassTransit;
using Utils.Events;

namespace LoggingAPI.EventConsumers
{
    public class UserLoginEventConsumer : IConsumer<UserLoginEvent>
    {





        public async Task Consume(ConsumeContext<UserLoginEvent> context)
        {
            string message = (context.RoutingKey());



        }
    }
}
