namespace TransportApex.Domain.Entities
{
    public sealed class Entrega
    {
        public long Id { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public Produto Produto { get; set; }

        private Entrega() { }

        public Entrega(Fornecedor fornecedorId, Produto produtoId)
        {
            Fornecedor = fornecedorId;
            Produto = produtoId;
        }
    }
}
