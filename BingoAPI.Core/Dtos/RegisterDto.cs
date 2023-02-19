using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BingoAPI.Core.Dtos;
public class RegisterDto
{
    [Required(ErrorMessage = "{0} can not be empty")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "{0} can not be empty")]
    [EmailAddress(ErrorMessage = "Invalid format of email address")]
    public string? Email { get; set; }

    [Phone]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "{0} can not be empty")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "{0} can not be empty")]
    [Compare("Password")]
    [DisplayName("Confirm password")]
    public string? ConfirmPassord { get; set; }
}
