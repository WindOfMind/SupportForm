﻿using System;
using System.Collections.Concurrent;

namespace SupportForm.API
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

        public SupportMessage Get(Guid id)
        {
            if (!_supportMessages.TryGetValue(id, out SupportMessage supportMessage))
            {
                throw new ArgumentException($"Support message with ID {id} not found.");
            }

            return supportMessage;
        }
    }
}
