using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="You must fill your ID or email")]
        [Display(Name = "Reader ID or Email")]
        public string Identifier { get; set; }

        [Required(ErrorMessage ="You must enter your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "You must fill in your full name")]
        [Display(Name ="Full Name")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required(ErrorMessage = "You must fill in your birthday")]
        [Display(Name = "BirthDay")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "You must fill in your email address")]
        [EmailAddress(ErrorMessage ="You must fill in correct email address")]
        [Display(Name = "Email Address")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        [StringLength(100, ErrorMessage = "You must fill in your phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "You must fill in your full name")]
        [StringLength(200)]
        public string Address { get; set; }

    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage ="You must enter your old password")]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage ="You must enter your new password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "You must enter your new password to confirm")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

}
