using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Carbontally.Models
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage="Please enter a valid email address.")]
        [Display(Name= "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage="The {0} must be at least {2} characters long.", MinimumLength=8)]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}