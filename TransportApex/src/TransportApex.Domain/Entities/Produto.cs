namespace TransportApex.Domain.Entities
{
    public class Produto
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public decimal Peso { get; set; }

        private Produto() { }

        public Produto(string descricao, decimal peso)
        {
            Descricao = descricao;
        }
    }

}
