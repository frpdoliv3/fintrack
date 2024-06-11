using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinTrack.Server.Models
{
    [Index(nameof(ISIN), IsUnique = true)]
    public class Security
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(12)]
        public string ISIN { get; set; }
        public string Name { get; set; }
        [MaxLength(3)]
        public string NativeCurrency { get; set; }
        public ICollection<SecurityTransaction> Transactions { get; } = new List<SecurityTransaction>();
    }
}
