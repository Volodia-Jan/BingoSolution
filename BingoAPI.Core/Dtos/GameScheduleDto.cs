using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BingoAPI.Core.Dtos;
public class GameScheduleDto
{
    [Required(ErrorMessage = "{0} can not be empty")]
    [DisplayName("Game Time")]
    public string? GameTime { get; set; }
}
