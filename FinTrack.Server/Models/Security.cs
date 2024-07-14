using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FinTrack.Server.Models
{
    [Index(nameof(ISIN), IsUnique = true)]
    public class Security
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(12)] public string ISIN { get; set; } = null!;
        public string Name { get; set; } = null!;
        [MaxLength(3)] public string NativeCurrency { get; set; } = null!;
        public ICollection<SecurityTransaction> Transactions { get; } = new List<SecurityTransaction>();
    }
}
