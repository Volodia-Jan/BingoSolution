using BingoAPI.Core.Entities;
using BingoAPI.Core.RepositoryContracts;
using BingoAPI.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace BingoAPI.Infrastructure.Repositories;
public class FriendshipRepository : IFriendshipRepository
{
    private readonly ApplicationDbContext _db;

    public FriendshipRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> AcceptFriend(Guid userId, Guid friendId)
    {
        var friendship = await _db.Friendships
            .Include("User")
            .Include("Friend")
            .SingleOrDefaultAsync(f => f.UserId == friendId && f.FriendId == userId && !f.IsAccepted);

        if (friendship != null)
        {
            friendship.IsAccepted = true;
            return await _db.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> AddFriend(Guid userId, Guid friendId)
    {
        var friendship = new Friendship
        {
            UserId = userId,
            FriendId = friendId,
            IsAccepted = false
        };

        _db.Friendships.Add(friendship);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<List<Friendship>> FindAllFriendRequests(Guid userId) 
        => await _db.Friendships
            .Include("User")
            .Include("Friend")
            .Where(f => f.FriendId == userId && !f.IsAccepted)
            .ToListAsync();

    public async Task<List<Friendship>> FindAllUserFriends(Guid userId) 
        => await _db.Friendships
        .Include("User")
        .Include("Friend")
        .Where(f => (f.UserId == userId || f.FriendId == userId) && f.IsAccepted)
        .ToListAsync();

    public async Task<bool> IsUsersFriends(Guid firstUser, Guid secondUser) =>
        await _db.Friendships
        .AnyAsync(f => ((f.UserId == firstUser && f.FriendId == secondUser) || (f.UserId == secondUser && f.FriendId == firstUser)) && f.IsAccepted);
}
