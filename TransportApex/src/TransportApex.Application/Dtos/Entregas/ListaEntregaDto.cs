namespace TransportApex.Application.Dtos.Entregas
{
    public class ListaEntregaDto
    {
        public long Id { get; set; }
        public long FornecedorId { get; set; }
        public string FornecedorNome { get; set; }
        public long ProdutoId { get; set; }
        public string DescricaoProduto { get; set; }
    }
}
