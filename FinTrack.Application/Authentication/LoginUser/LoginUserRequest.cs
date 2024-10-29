using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Authentication.LoginUser;

public record LoginUserRequest(
    string Identity,
    string Password,
    bool RememberMe = false
);
