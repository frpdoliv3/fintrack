using System.ComponentModel.DataAnnotations;

namespace FinTrack.Domain.Entities
{
    public class Country
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Alpha2Code { get; set; } = string.Empty;
        public string Alpha3Code { get; set; } = string.Empty;
        public int NumericCode { get; set; }
    }
}
