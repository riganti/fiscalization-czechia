using System.Text.RegularExpressions;

namespace Mews.Eet.Dto.Identifiers
{
    public class RegistryIdentifier : StringIdentifier
    {
        public static readonly Regex Pattern = new Regex("^[0-9a-zA-Z\\.,:;/#\\-_ ]{1,20}$");

        public RegistryIdentifier(string value)
            : base(value, Pattern)
        {
        }
    }
}
