using FinTrack.Domain.Entities;
using FinTrack.Domain.Interfaces;
using FinTrack.Persistence.Contexts;
using FinTrack.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Persistence.Repositories;
internal class EFAuthRepository : IAuthRepository
{
    private readonly FinTrackDbContext _context;
    private readonly UserManager<EFUser> _userManager;
    private readonly IUserEmailStore<EFUser> _userStore;

    public EFAuthRepository(
        UserManager<EFUser> userManager,
        IUserStore<EFUser> userStore,
        FinTrackDbContext context
    ) {
        if (!userManager.SupportsUserEmail)
        {
            throw new NotSupportedException($"{nameof(EFAuthRepository)} requires a user store with email support.");
        }

        _userManager = userManager;
        _userStore = (IUserEmailStore<EFUser>) userStore;
        _context = context;
    }

    private async Task<EFUser?> FindUserById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
    
    public async Task<bool> ExistsWithId(string userId)
    {
        return await _userManager.Users.AnyAsync(u => u.Id == userId);
    }

    public Task<bool> ExistsAny()
    {
        return _context.Users.AnyAsync();
    }

    public async Task<User?> FindByEmail(string email)
    {
        var persistenceUser = await _userManager.FindByEmailAsync(email);
        return persistenceUser?.ToUser();
    }
    
    public async Task<bool> HasRole(string userId, string role)
    {
        var user = await FindUserById(userId);
        if (user == null) { return false; }
        return await _userManager.IsInRoleAsync(user, role);
    }
    
    public async Task<string> Register(CreateUser newUser)
    {
        var user = new EFUser();
        await _userStore.SetUserNameAsync(user, newUser.UserName, CancellationToken.None);
        await _userStore.SetEmailAsync(user, newUser.Email, CancellationToken.None);
        //TODO: Handle result
        await _userManager.CreateAsync(user, newUser.Password);
        return user.Id;
    }
}
