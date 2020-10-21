using System.ComponentModel.DataAnnotations;

namespace SupportForm.API
{
    public class SupportMessageRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public MessageType MessageType { get; set; }

        public string CustomerId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Message { get; set; }
    }
}
