using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FinTrack.Server.Models
{
    public class UserIdentity : IdentityUser
    {
        public UserDetail? UserDetails { get; set; }
    }
}

