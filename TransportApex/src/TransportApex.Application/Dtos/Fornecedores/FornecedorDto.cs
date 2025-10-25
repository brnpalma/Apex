using TransportApex.Domain.ValueObjects;

namespace TransportApex.Application.Dtos.Fornecedores
{
    public class FornecedorDto
    {
        public bool Sucesso { get; set; }
        public long Id { get; init; }
        public string Nome { get; init; }
        public Cnpj Cnpj { get; init; }
    }
}
