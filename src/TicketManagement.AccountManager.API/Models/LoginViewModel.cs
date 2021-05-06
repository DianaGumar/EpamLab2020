using System.ComponentModel.DataAnnotations;

namespace TicketManagement.AccountManager.API.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        ////[TempData]
        ////public string ErrorMessage { get; set; }
    }
}
