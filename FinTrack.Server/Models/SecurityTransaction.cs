using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public float Price { get; set; }
        public float Commission { get; set; }
        public float ExchangeRate { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
