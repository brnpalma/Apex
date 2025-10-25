namespace TransportApex.Application.Dtos.Fornecedores
{
    public class FornecedorDto
    {
        public bool Sucesso { get; set; }
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Mensagem { get; set; }
    }
}
