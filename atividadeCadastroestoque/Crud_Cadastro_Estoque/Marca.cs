public class Marca
{
    public int Id { get; set; }
    public string Nome { get; set; }

    // Construtor que aceita ID e Nome
    public Marca(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }

    // Construtor que aceita apenas Nome
    public Marca(string nome)
    {
        Nome = nome;
    }
}