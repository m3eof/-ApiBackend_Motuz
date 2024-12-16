using System.ComponentModel.DataAnnotations;

namespace ApiBackend.Models.Accounts
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
