using System.ComponentModel.DataAnnotations;

namespace BingoAPI.Core.Dtos;
public class LoginDto
{
    [Required(ErrorMessage = "{0} can not be empty")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "{0} can not be empty")]
    public string? Password { get; set; }
}
