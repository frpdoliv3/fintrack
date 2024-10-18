using System.ComponentModel.DataAnnotations;
using FinTrack.Domain.Entities.Validation;

namespace FinTrack.Domain.Entities
{
    public class Security
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Isin { get; set; } = string.Empty;
        public string NativeCurrency { get; set; } = string.Empty;
        public Country CounterpartyCountry { get; set; } = null!;
    }
}
