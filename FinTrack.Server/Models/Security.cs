using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinTrack.Server.Models
{
    [Index(nameof(ISIN), IsUnique = true)]
    public class Security
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [MaxLength(12)]
        [Column("isin")]
        public string ISIN { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [MaxLength(3)]
        [Column("native_curency")]
        public string NativeCurrency { get; set; }
        public ICollection<SecurityTransaction> Transactions { get; } = new List<SecurityTransaction>();
    }
}
