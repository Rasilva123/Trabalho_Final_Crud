namespace Crud_Cadastro_Estoque.dao
{
    public class Item
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public int Quantidade { get; set; }
        public string Tipo { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataEntrada { get; set; }
        public string NomeFornecedor { get; set; }
        public double ValorCompra { get; set; }
        public double ValorVenda { get; set; }
        public Marca Marca { get; set; }

        public Item() { }
        public Item(int id, string modelo, int quantidade, string tipo, DateTime dataVencimento, DateTime dataEntrada, string nomeFornecedor, double valorCompra, double valorVenda, Marca marca)
        {
            Id = id;
            Modelo = modelo;
            Quantidade = quantidade;
            Tipo = tipo;
            DataVencimento = dataVencimento;
            DataEntrada = dataEntrada;
            NomeFornecedor = nomeFornecedor;
            ValorCompra = valorCompra;
            ValorVenda = valorVenda;
            Marca = marca;
        }
    }
}