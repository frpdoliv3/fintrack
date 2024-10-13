using System.ComponentModel.DataAnnotations;
using FinTrack.Validation;

namespace FinTrack.Models;

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
    [IsCurrency]
    public string NativeCurrency { get; set; } = String.Empty;
    
    [Required(AllowEmptyStrings = false)]
    [IsCountry]
    public string CounterpartyCountry { get; set; } = String.Empty;
}