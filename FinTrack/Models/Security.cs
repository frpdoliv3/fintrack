using System.ComponentModel.DataAnnotations;
using FinTrack.Validation;

namespace FinTrack.Models;

public class Security
{
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