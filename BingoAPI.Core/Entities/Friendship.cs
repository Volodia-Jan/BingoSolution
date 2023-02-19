using System.ComponentModel.DataAnnotations.Schema;

namespace BingoAPI.Core.Entities;
public class Friendship
{
    public Guid UserId { get; set; }
    public Guid FriendId { get; set; }
    public bool IsAccepted { get; set; }

    [ForeignKey("UserId")]
    public virtual ApplicationUser? User { get; set; }

    [ForeignKey("FriendId")]
    public virtual ApplicationUser? Friend { get; set; }
}
