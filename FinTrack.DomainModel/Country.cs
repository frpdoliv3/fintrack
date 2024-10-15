using System.ComponentModel.DataAnnotations;

namespace FinTrack.DomainModel
{
    public class Country
    {
        [Key]
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [Length(2, 2)]
        public string Alpha2Code { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        [Length(3, 3)]
        public string Alpha3Code { get; set; } = string.Empty;

        [Required]
        public int NumericCode { get; set; }
    }
}
