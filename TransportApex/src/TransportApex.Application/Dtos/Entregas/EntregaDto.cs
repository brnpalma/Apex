namespace TransportApex.Application.Dtos.Entregas
{
    public class EntregaDto
    {
        public bool Sucesso { get; set; }
        public long Id { get; init; }
        public long FornecedorId { get; init; }
        public string FornecedorNome { get; init; } = default!;
        public long ProdutoId { get; init; }
        public string ProdutoNome { get; init; } = default!;
        public DateTime DataEntrega { get; init; }
    }
}
