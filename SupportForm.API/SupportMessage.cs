using System;
using System.Text.RegularExpressions;

namespace SupportForm.API
{
    public class SupportMessage
    {
        public SupportMessage(string email, string phone, MessageType messageType, string message)
        {
            Id = Guid.NewGuid();
            Email = email;
            Phone = phone;
            MessageType = messageType;
            Message = FilterTextFromScript(message);
        }

        public Guid Id { get; }

        public string Email { get; }

        public string Phone { get; }

        public MessageType MessageType { get; }

        public string CustomerId { get; protected set; }

        public string Message { get; }

        public void UpdateCustomerId(string customerId)
        {
            // In the real cases we may need to check if such customer ID is real before setting,
            // and set the customer object, not ID.
            CustomerId = customerId;
        }

        private string FilterTextFromScript(string text)
        {
            var regex = new Regex(
                "(\\<script(.+?)\\</script\\>)",
                RegexOptions.IgnoreCase
            );

            return regex.Replace(text, "");
        }
    }
}
