using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinTrack.Server.Models
{
    public enum OrderType 
    {
        Buy,
        Sell
    }

    [Table("security_transactions")]
    public class SecurityTransaction
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("order_type")]
        public OrderType OrderType { get; set; }
        [Column("quantity")]
        public uint Quantity { get; set; }
        [Column("price")]
        public float Price { get; set; }
        [Column("comission")]
        public float Commission { get; set; }
        [Column("exchange_rate")]
        public float ExchangeRate { get; set; }
        [MaxLength(3)]
        [Column("currency")]
        public string Currency { get; set; }
        [Column("date")]
        public DateTime Date { get; set; } = DateTime.UtcNow;
        [Column("security_id")]
        private int SecurityId { get; set; }
        public virtual Security Security { get; set; }
    }
}
