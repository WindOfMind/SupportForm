using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SupportForm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/message")]
    public class SupportController : ControllerBase
    {
        private readonly ILogger<SupportController> _logger;
        private readonly ISupportMessageRepository _messageRepository;

        public SupportController(ILogger<SupportController> logger, ISupportMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] SupportMessageRequest request)
        {
            var supportMessage = new SupportMessage(request.Email, request.Phone, request.MessageType, request.Message);

            if (string.IsNullOrEmpty(request.CustomerId))
            {
                supportMessage.UpdateCustomerId(request.CustomerId);
            }

            _messageRepository.Save(supportMessage);

            return Ok();
        }
    }
}
