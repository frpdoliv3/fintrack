using System.ComponentModel.DataAnnotations;

namespace FinTrack.Server.Models;

public class UserDetail
{
    [Key] [MaxLength(450)] public string UserIdentityId { get; set; } = null!;
    public UserIdentity UserIdentity { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateOnly Birthdate { get; set; }
    public string AddressFirstLine { get; set; } = null!;
    public string? AddressSecondLine { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
}
