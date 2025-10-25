namespace TransportApex.Application.Dtos.Produtos
{
    public class ProdutoDto
    {
        public bool Sucesso { get; set; }
        public long Id { get; set; }
        public string Descricao { get; set; }
        public decimal Peso { get; set; }
        public string Mensagem { get; set; }
    }
}
