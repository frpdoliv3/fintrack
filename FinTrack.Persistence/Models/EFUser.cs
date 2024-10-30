using FinTrack.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FinTrack.Persistence.Models;

public class EFUser: IdentityUser
{
    public User ToUser()
    {
        return new User
        {
            Id = Id,
            UserName = UserName!,
            Email = Email!
        };
    }
}
