using System;

namespace SupportForm.API
{
    public interface ISupportMessageRepository
    {
        void Save(SupportMessage supportMessage);

        SupportMessage Get(Guid id);
    }
}