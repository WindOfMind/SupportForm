using System.ComponentModel.DataAnnotations;
using SupportForm.Domain;

namespace SupportForm.API.Models
{
    public class SupportMessageRequest
    {
        /// <summary>
        /// Email address of the customer who applied for the support.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Phone number of the customer who applied for the support.
        /// </summary>
        [Required]
        [Phone]
        [MinLength(7)]
        [MaxLength(8)]
        public string Phone { get; set; }

        /// <summary>
        /// Type of the support message.
        /// </summary>
        [Required]
        public MessageType MessageType { get; set; }

        /// <summary>
        /// ID of the customer.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// The content of the support message.
        /// </summary>
        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Message { get; set; }
    }
}
