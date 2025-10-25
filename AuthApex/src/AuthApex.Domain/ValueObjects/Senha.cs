using System.Text.RegularExpressions;

namespace AuthApex.Domain.ValueObjects
{
    public sealed class Senha(string valorHash) : IEquatable<Senha>
    {
        private const int MinLength = 8;
        private const int MaxLength = 64;

        public string ValorHash { get; } = valorHash;

        public static bool TryCreate(string senhaPura, out Senha? senha, out string? error)
        {
            senha = null;
            error = null;

            if (string.IsNullOrWhiteSpace(senhaPura))
            {
                error = "A senha não pode estar vazia.";
                return false;
            }

            if (senhaPura.Length < MinLength)
            {
                error = $"A senha deve conter no mínimo {MinLength} caracteres.";
                return false;
            }

            if (senhaPura.Length > MaxLength)
            {
                error = $"A senha deve conter no máximo {MaxLength} caracteres.";
                return false;
            }

            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).+$");
            if (!regex.IsMatch(senhaPura))
            {
                error = "A senha deve conter letra maiúscula, minúscula, número e caractere especial.";
                return false;
            }

            senha = new Senha(GerarHash(senhaPura));
            return true;
        }

        private static string GerarHash(string senha)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool Verificar(string senhaPura)
        {
            var hash = GerarHash(senhaPura);
            return hash == ValorHash;
        }

        #region Igualdade e operadores

        public override bool Equals(object? obj) => Equals(obj as Senha);

        public bool Equals(Senha? other)
        {
            if (other is null)
                return false;

            return ValorHash == other.ValorHash;
        }

        public override int GetHashCode() => ValorHash.GetHashCode();

        public static bool operator ==(Senha? left, Senha? right) => Equals(left, right);
        public static bool operator !=(Senha? left, Senha? right) => !Equals(left, right);

        #endregion

        public override string ToString() => "********";
    }
}
