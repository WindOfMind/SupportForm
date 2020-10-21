using Swashbuckle.AspNetCore.Filters;

namespace SupportForm.API.Models.SwaggerExamples
{
    public class SupportMessageRequestExample : IExamplesProvider<SupportMessageRequest>
    {
        public SupportMessageRequest GetExamples()
        {
            return new SupportMessageRequest
            {
                Email = "example@email.com",
                Phone = "55555555",
                CustomerId = "123",
                Message = "Test message.",
                MessageType = MessageType.Question
            };
        }
    }
}
