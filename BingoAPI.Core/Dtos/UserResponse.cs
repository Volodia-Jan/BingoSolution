namespace BingoAPI.Core.Dtos;
public class UserResponse
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsGameInfoPrivate { get; set; }
    public DateTime? GameSchedule { get; set; }
}
