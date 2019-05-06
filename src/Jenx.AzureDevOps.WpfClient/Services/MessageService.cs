using Jenx.AzureDevOps.WpfClient.EventAggregator;
using Prism.Events;

namespace Jenx.AzureDevOps.WpfClient.Services
{
    public class MessageService : IMessageService
    {
        private readonly IEventAggregator _eventAggregator;

        public MessageService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void SendMessage(string message)
        {
            _eventAggregator.GetEvent<SentGlobalMessageEvent>().Publish(message);
        }
    }
}