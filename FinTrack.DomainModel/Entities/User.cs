using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Domain.Entities;
public class User
{
    public required string Id { get; set; }
    public required string UserName { get; set; } = null!;
    public required string Email { get; set; } = null!;
}
