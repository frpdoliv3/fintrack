using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FinTrack.Server.Models
{
    [Index(nameof(ISIN), IsUnique = true)]
    public class Security
    {
        [Key]
        public int Id { get; set; }
        public string ISIN { get; set; }
        public string Name { get; set; }
        public string NativeCurrency { get; set; }
        public ICollection<SecurityTransaction> Transactions { get; } = new List<SecurityTransaction>();
    }
}
