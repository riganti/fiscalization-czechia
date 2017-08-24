using System.Text.RegularExpressions;

namespace Mews.Eet.Dto.Identifiers
{
    public class TaxIdentifier : StringIdentifier
    {
        public static readonly Regex Pattern = new Regex("^CZ[0-9]{8,10}$");

        public TaxIdentifier(string value)
            : base(value, Pattern)
        {
        }
    }
}
