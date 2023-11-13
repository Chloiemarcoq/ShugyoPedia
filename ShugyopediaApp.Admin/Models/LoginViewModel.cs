﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShugyopediaApp.Admin.Models
{
    /// <summary>
    /// Login View Model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>ユーザーID</summary>
        [JsonPropertyName("userId")]
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; }
        /// <summary>パスワード</summary>
        [JsonPropertyName("password")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public string UserEmail { get; set; }
    }
}
