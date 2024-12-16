using System.ComponentModel.DataAnnotations;

namespace ApiBackend.Models.Accounts
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
