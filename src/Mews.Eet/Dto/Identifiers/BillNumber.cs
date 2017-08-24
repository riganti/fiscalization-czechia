using System.Text.RegularExpressions;

namespace Mews.Eet.Dto.Identifiers
{
    public class BillNumber : StringIdentifier
    {
        public static readonly Regex Pattern = new Regex("^[0-9a-zA-Z\\.,:;/#\\-_ ]{1,25}$");

        public BillNumber(string value)
            : base(value, Pattern)
        {
        }
    }
}
