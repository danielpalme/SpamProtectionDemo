using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpamProtectionDemo.Models
{
    public class NewsletterMember
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Id { get; set; } = DateTime.UtcNow.Ticks.ToString();

        [Required(ErrorMessage = "[email address is required]")]
        [DataType(DataType.EmailAddress, ErrorMessage = "[must be a valid email address]")]
        [DisplayName("Email Address")]
        public string Email { get; set; }
    }
}