using System.ComponentModel.DataAnnotations;

namespace FinTrack.DomainModel
{
    public class Security
    {
        [Key]
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = String.Empty;

        [Required(AllowEmptyStrings = false)]
        [IsIsin]
        public string Isin { get; set; } = String.Empty;

        [Required(AllowEmptyStrings = false)]
        public string NativeCurrency { get; set; } = String.Empty;

        [Required(AllowEmptyStrings = false)]
        public string CounterpartyCountry { get; set; } = String.Empty;
    }
}
