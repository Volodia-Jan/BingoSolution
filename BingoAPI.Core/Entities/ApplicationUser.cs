using Microsoft.AspNetCore.Identity;

namespace BingoAPI.Core.Entities;
public class ApplicationUser : IdentityUser<Guid>
{
    public string? FullName { get; set; }
    public bool IsGameInfoPrivate { get; set; } = false;
    public DateTime? GameSchedule { get; set; }
}
