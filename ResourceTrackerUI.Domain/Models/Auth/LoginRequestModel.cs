using System.ComponentModel.DataAnnotations;

namespace ResourceTrackerUI.Domain.Models.Auth
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;
    }
}
