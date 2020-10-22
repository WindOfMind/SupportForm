using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SupportForm.API.Models;
using SupportForm.Domain;

namespace SupportForm.API.Controllers
{
    [ApiController]
    [Route("api/support/message")]
    public class SupportController : ControllerBase
    {
        private readonly ILogger<SupportController> _logger;
        private readonly ISupportMessageRepository _messageRepository;

        public SupportController(ILogger<SupportController> logger, ISupportMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            _logger = logger;
        }

        /// <summary>
        /// Post a support message.
        /// </summary>
        /// <param name="request"><see cref="SupportMessageRequest"/></param>
        /// <response code="200">Support message was successfully posted.</response>
        /// <response code="200">Validation failed.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
