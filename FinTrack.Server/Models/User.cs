using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FinTrack.Server.Models
{
    public class User : IdentityUser
    {
        [MaxLength(450)]
        public string Name { get; set; } = null!;
        [MaxLength(150)]
        public string AddressFirstLine { get; set; } = null!;
        [MaxLength(64)]
        public string? AddressSecondLine { get; set; } = null;
        public string ZipCode { get; set; } = null!;
        
        public User(string name, string addressFirstLine, string? addressSecondLine, string zipCode)
        {
            Name = name;
            AddressFirstLine = addressFirstLine;
            AddressSecondLine = addressSecondLine;
            ZipCode = zipCode;
        }
    }
}
