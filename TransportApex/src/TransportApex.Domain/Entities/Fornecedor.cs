using TransportApex.Domain.ValueObjects;

namespace TransportApex.Domain.Entities
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Cnpj Cnpj { get; set; }

        private Fornecedor() { }

        public Fornecedor(string nome, Cnpj cnpj)
        {
            Nome = nome;
            Cnpj = cnpj;
        }
    }
}
