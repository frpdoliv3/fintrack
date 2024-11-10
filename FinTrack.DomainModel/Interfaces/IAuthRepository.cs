using FinTrack.Domain.Entities;

namespace FinTrack.Domain.Interfaces;
public interface IAuthRepository
{
    public Task<string> Register(CreateUser newUser);
    public Task<User?> FindByEmail(string email);
    public Task<bool> ExistsAny();
    public Task<bool> ExistsWithId(string userId);
    public Task<bool> HasRole(string userId, string role);
}
