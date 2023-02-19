using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BingoAPI.Core.Dtos;
public class FriendDto
{
    [Required(ErrorMessage = "{0} can not be empty")]
    [DisplayName("Friend username")]
    public string? FriendUserName { get; set; }
}
