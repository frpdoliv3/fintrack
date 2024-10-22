namespace FinTrack.Domain.Entities;

public sealed class UserIdentity
{
    /* Can be both email or password */
    public string Identity { get; set; } = null!;
    public string Password { get; set; } = null!;
}
