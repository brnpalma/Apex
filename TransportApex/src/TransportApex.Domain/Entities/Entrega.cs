namespace TransportApex.Domain.Entities
{
    public sealed class Entrega
    {
        public long Id { get; set; }
        public long FornecedorId { get; set; }
        public long ProdutoId { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataEntrega { get; set; }

        private Entrega() { }

        public Entrega(long fornecedorId, long produtoId)
        {
            FornecedorId = fornecedorId;
            ProdutoId = produtoId;
        }
    }
}
