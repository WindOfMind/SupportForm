using System;
using System.Collections.Concurrent;
using SupportForm.Domain;

namespace SupportForm.Infrastructure
{
    public class SupportMessageRepository : ISupportMessageRepository
    {
        private readonly ConcurrentDictionary<Guid, SupportMessage> _supportMessages = new ConcurrentDictionary<Guid, SupportMessage>();

        public void Save(SupportMessage supportMessage)
        {
            if (!_supportMessages.TryAdd(supportMessage.Id, supportMessage))
            {
                throw new InvalidOperationException($"Can't save the message. Id {supportMessage.Id} already exists.");
            }
        }
    }
}
