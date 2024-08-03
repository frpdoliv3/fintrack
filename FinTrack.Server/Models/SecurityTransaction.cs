using System.ComponentModel.DataAnnotations;

namespace FinTrack.Server.Models
{
    public enum OrderType 
    {
        Buy,
        Sell
    }

    public class SecurityTransaction
    {
        [Key]
        public int Id { get; set; }
        public OrderType OrderType { get; set; }
        public uint Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Commission { get; set; }
        public decimal ExchangeRate { get; set; }
        [MaxLength(3)] public string Currency { get; set; } = null!;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public virtual Security Security { get; set; } = null!;
    }
}
