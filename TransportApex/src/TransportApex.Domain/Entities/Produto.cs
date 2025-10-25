namespace TransportApex.Domain.Entities
{
    public class Produto
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public decimal Peso { get; set; }

        private Produto() { }

        public Produto(string descricao, decimal peso)
        {
            Descricao = descricao;
            Peso = peso;
        }

        public static bool TryCreate(string descricao, decimal peso, out Produto? produto, out string? error)
        {
            produto = null;
            error = null;

            if (string.IsNullOrWhiteSpace(descricao))
            {
                error = "Descrição do produto não pode ser vazia.";
                return false;
            }

            if (descricao.Length < 3 || descricao.Length > 100)
            {
                error = "Descrição do produto deve ter entre 3 e 100 caracteres.";
                return false;
            }

            if (peso <= 0)
            {
                error = "Peso do produto deve ser maior que zero.";
                return false;
            }

            if (peso > 1000)
            {
                error = "Peso do produto é muito alto.";
                return false;
            }

            produto = new Produto(descricao.Trim(), peso);
            return true;
        }
    }

}
