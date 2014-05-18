using System.ComponentModel.DataAnnotations;

namespace Carbontally.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="Please enter your user name.")]
        public string UserName { get; set; }

        [Required(ErrorMessage="Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}