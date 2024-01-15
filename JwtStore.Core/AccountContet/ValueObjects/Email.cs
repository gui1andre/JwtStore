using System.Text.RegularExpressions;
using JwtStore.Core.SharedContext.Extensions;
using JwtStore.Core.SharedContext.ValueObjects;

namespace JwtStore.Core.AccountContet.ValueObjects
{
    public partial class Email : ValueObject
    {
        private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public Email(string adress)
        {
            if(string.IsNullOrWhiteSpace(adress))
                throw new Exception("E-mail invalido");

            Adress = adress.Trim().ToLower();

            if(Adress.Length < 5)
                throw new Exception("E-mail invalido");

            if(!EmailRegex().IsMatch(Adress))
                throw new Exception("E-mail invalido");
        }

        public string Adress { get; }

        public string Hash => Adress.ToBase64();

        public static implicit operator string(Email email) => email.ToString();

        public static implicit operator Email(string adress) => new Email(adress);

        public override string ToString() => Adress.Trim().ToLower();

        [GeneratedRegex(Pattern)]
        private static partial Regex EmailRegex();
    }
}