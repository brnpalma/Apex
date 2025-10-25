namespace TransportApex.Application.Dtos.Entregas
{
    public class EntregaDto
    {
        public bool Sucesso { get; set; }
        public long Id { get; set; }
        public long FornecedorId { get; set; }
        public string FornecedorNome { get; set; }
        public long ProdutoId { get; set; }
        public string DescricaoProduto { get; set; }
        public string Mensagem { get; set; }
    }
}
