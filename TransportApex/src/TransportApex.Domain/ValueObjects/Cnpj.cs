using System.Text.RegularExpressions;

namespace TransportApex.Domain.ValueObjects
{
    public class Cnpj(string numero)
    {
        public string Numero { get; } = numero;

        public static bool TryCreate(string numero, out Cnpj? cnpjVo, out string? error)
        {
            cnpjVo = null;
            error = null;

            if (string.IsNullOrWhiteSpace(numero))
            {
                error = "CNPJ não pode ser vazio.";
                return false;
            }

            var digits = Regex.Replace(numero, @"[^\d]", "");

            if (digits.Length != 14)
            {
                error = "CNPJ deve conter 14 dígitos.";
                return false;
            }

            // Caso queira habilitar uma validação mais rigida do formato de cnpj:
            //if (!IsValidCnpj(digits))
            //{
            //    error = "CNPJ inválido.";
            //    return false;
            //}

            var formatted = Convert.ToUInt64(digits).ToString(@"00\.000\.000\/0000\-00");
            cnpjVo = new Cnpj(formatted);
            return true;
        }

        private static bool IsValidCnpj(string cnpj)
        {
            if (new string(cnpj[0], cnpj.Length) == cnpj)
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;
            string digito = resto.ToString();

            tempCnpj += digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public override bool Equals(object obj)
        {
            if (obj is not Cnpj other) return false;
            return Numero == other.Numero;
        }

        public override int GetHashCode() => Numero.GetHashCode();

        public override string ToString() => Numero;
    }

}
