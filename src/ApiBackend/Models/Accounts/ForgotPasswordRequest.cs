﻿using System.ComponentModel.DataAnnotations;

namespace ApiBackend.Models.Accounts
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
