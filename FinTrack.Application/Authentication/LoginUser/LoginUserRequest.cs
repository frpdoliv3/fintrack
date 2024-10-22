using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Authentication.LoginUser;
public record LoginUserRequest
{
    /* Can be both email or password */
    public string Identity { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; } = false;
}
