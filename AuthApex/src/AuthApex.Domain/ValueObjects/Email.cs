using System.Text.RegularExpressions;

namespace AuthApex.Domain.ValueObjects
{
    public class Email
    {
        public string Endereco { get; }

        public Email(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException("Email não pode ser vazio.");

            if (!Regex.IsMatch(endereco, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Email inválido.");

            Endereco = endereco;
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
