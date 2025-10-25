using TransportApex.Domain.ValueObjects;

namespace TransportApex.Application.Dtos.Fornecedores
{
    public class ListaFornecedorDto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
    }
}
