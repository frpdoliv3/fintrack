using System.Text.RegularExpressions;

namespace FinTrack.Application.Utils;

public static class IsValidNIF
{
    private static string _validNIFStartChars = "^[123]|45|5|6|7[01245789]|9[0189]";
    public static bool Validate(string nif)
    {
        if (!Regex.IsMatch(nif, @"^\d{9}$"))
        {
            return false;
        }
        if (!Regex.IsMatch(nif, _validNIFStartChars))
        {
            return false;
        }
        
        var multFactor = 9;
        var controlTotal = 0;
        foreach (var digit in nif.Substring(0, nif.Length - 1))
        {
            controlTotal += (digit - '0') * multFactor;
            multFactor -= 1;
        }

        controlTotal %= 11;
        var control = controlTotal is 1 or 0 ? 0 : controlTotal;
        return nif[8] - '0' == 11 - control;
    }
}