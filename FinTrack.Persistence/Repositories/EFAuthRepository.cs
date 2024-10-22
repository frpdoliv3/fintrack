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

    public Task<bool> ExistsAnyUser()
    {
        return _context.Users.AnyAsync();
    }

    public async Task<User?> FindUserByEmail(string email)
    {
        var persistenceUser = await _userManager.FindByEmailAsync(email);
        return persistenceUser?.ToUser();
    }

    public async Task RegisterUser(CreateUser newUser)
    {
        var user = new EFUser();
        await _userStore.SetUserNameAsync(user, newUser.UserName, CancellationToken.None);
        await _userStore.SetEmailAsync(user, newUser.Email, CancellationToken.None);
        var result = await _userManager.CreateAsync(user, newUser.Password);
    }
}
