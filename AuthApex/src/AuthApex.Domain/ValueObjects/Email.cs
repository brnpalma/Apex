using System.Text.RegularExpressions;

namespace AuthApex.Domain.ValueObjects
{
    public class Email(string endereco)
    {
        public string Endereco { get; } = endereco;

        public static bool TryCreate(string endereco, out Email? emailVo, out string? error)
        {
            emailVo = null;
            error = null;

            if (string.IsNullOrWhiteSpace(endereco))
            {
                error = "Email não pode ser vazio.";
                return false;
            }

            if (!Regex.IsMatch(endereco, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                error = "Email inválido.";
                return false;
            }

            emailVo = new Email(endereco);
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Email other) return false;
            return Endereco == other.Endereco;
        }

        public override int GetHashCode() => Endereco.GetHashCode();

        public override string ToString() => Endereco;
    }

}
