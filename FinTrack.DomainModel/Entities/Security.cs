using System.ComponentModel.DataAnnotations;
using FinTrack.Domain.Entities.Validation;

namespace FinTrack.Domain.Entities
{
    public class Security
    {
        [Key]
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(12)]
        [IsIsin]
        public string Isin { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(3)]
        public string NativeCurrency { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(3)]
        public string CounterpartyCountry { get; set; } = string.Empty;
    }
}
