using FinTrack.Domain.Entities;

namespace FinTrack.Domain.Interfaces;
public interface IAuthRepository
{
    public Task RegisterUser(CreateUser newUser);
    public Task<User?> FindUserByEmail(string email);
    public Task<bool> ExistsAnyUser();
    public Task<bool> ExistsWithId(string userId);
}
