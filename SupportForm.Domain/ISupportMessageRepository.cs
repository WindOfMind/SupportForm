using System;

namespace SupportForm.Domain
{
    public interface ISupportMessageRepository
    {
        /// <summary>
        /// Save the support message.
        /// </summary>
        /// <param name="supportMessage"></param>
        /// <exception cref="InvalidOperationException">Thrown if we already have a message with such ID.</exception>
        void Save(SupportMessage supportMessage);
    }
}