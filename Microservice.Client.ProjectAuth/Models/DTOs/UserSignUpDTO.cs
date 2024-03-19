using System.ComponentModel.DataAnnotations;

namespace Microservice.Client.ProjectAuth.Models.DTOs
{
    public class UserSignUpDTO
    {
        //    [Required(ErrorMessage = "Enter your first name")]
        //    [Display(Name = "Name")]
        //    [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} must contain from {2} to {1} characters.")]
        //    public string? Name { get; set; }

        //    [Required(ErrorMessage = "Enter your email")]
        //    [Display(Name = "Email")]
        //    [EmailAddress(ErrorMessage = "Invalid email")]
        //    public string? Email { get; set; }

        //    [Required(ErrorMessage = "Enter your password")]
        //   // [Compare("ConfirmPassword", ErrorMessage = "Password does not match")]
        //    //[Display(Name = "Password")]
        //   // [DataType(DataType.Password)]
        //    public string? Password { get; set; }

        //    [Required(ErrorMessage = "Enter your phone number")]
        //    [Display(Name = "PhoneNumber")]
        //    [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be {1} digits long.")]
        //    public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter your email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your first name")]
        [Display(Name = "Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} must contain from {2} to {1} characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your phone number")]
        [Display(Name = "PhoneNumber")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be {1} digits long.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Add the ConfirmPassword property
        [Required(ErrorMessage = "Confirm your password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
