using System;
using Crud_Cadastro_Estoque.dao;

namespace Crud_Cadastro_Estoque
{
    class Program
    {
        private ItemDAO itemDAO;
        private MarcaDAO marcaDAO;

        public Program()
        {
            itemDAO = new ItemDAO();
            marcaDAO = new MarcaDAO();
        }

        static void Main(string[] args)
        {
            new Program().Menu();
        }

        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("=========================================");
            Console.WriteLine("         Sistema de Cadastro            ");
            Console.WriteLine("           de Estoque                   ");
            Console.WriteLine("=========================================");
            int opcao;

            do
            {
                Console.WriteLine("\nEscolha uma opção:");
                Console.WriteLine("1 - Inserir Marca");
                Console.WriteLine("2 - Inserir Item");
                Console.WriteLine("3 - Deletar Item");
                Console.WriteLine("4 - Listar Itens");
                Console.WriteLine("5 - Atualizar Item");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");

                // Validação da entrada do usuário
                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Por favor, insira um número válido.");
                    continue;
                }

                switch (opcao)
                {
                    case 1: InserirMarca(); break;
                    case 2: InserirItem(); break;
                    case 3: DeletarItem(); break;
                    case 4: ListarItens(); break;
                    case 5: AtualizarItem(); break;
                    case 0: Console.WriteLine("Saindo..."); break;
                    default: Console.WriteLine("Opção inválida. Tente novamente."); break;
                }

            } while (opcao != 0);
        }

        private void InserirMarca()
        {
            try
            {
                Console.WriteLine("\n--- Inserir Nova Marca ---");
                var novaMarca = new Marca(Prompt("Nome da Marca: "));
                marcaDAO.Insert(novaMarca);
                Console.WriteLine("Marca inserida com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        private void InserirItem()
        {
            try
            {
                Console.WriteLine("\n--- Inserir Novo Item ---");
                var novoItem = new Item
                {
                    Modelo = Prompt("Modelo: "),
                    Quantidade = int.Parse(Prompt("Quantidade: ")),
                    Tipo = Prompt("Tipo: "),
                    DataVencimento = DateTime.Parse(Prompt("Data de Vencimento (yyyy-mm-dd): ")),
                    DataEntrada = DateTime.Now, // Data de entrada é a data atual
                    NomeFornecedor = Prompt("Nome do Fornecedor: "),
                    ValorCompra = double.Parse(Prompt("Valor de Compra: ")),
                    ValorVenda = double.Parse(Prompt("Valor de Venda: "))
                };

                // Listar marcas disponíveis para o usuário escolher
                ListarMarcas();
                int marcaId = int.Parse(Prompt("ID da Marca: "));

                // Verificar se a marca existe
                var marcas = marcaDAO.List();
                var marcaExistente = marcas.Find(m => m.Id == marcaId);

                if (marcaExistente != null)
                {
                    novoItem.Marca = new Marca(marcaId, marcaExistente.Nome);
                    itemDAO.Insert(novoItem);
                    Console.WriteLine("Item inserido com sucesso!");
                }
                else
                {
                    Console.WriteLine("Erro: A marca com o ID fornecido não existe.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao inserir item: " + ex.Message);
            }
        }

        private void DeletarItem()
        {
            try
            {
                Console.WriteLine("\n--- Deletar Item ---");
                int id = int.Parse(Prompt("ID do item para deletar: "));
                itemDAO.Delete(new Item { Id = id });
                Console.WriteLine("Item deletado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        private void ListarItens()
        {
            try
            {
                Console.WriteLine("\n--- Listar Itens ---");
                var itens = itemDAO.List();
                if (itens.Count == 0)
                {
                    Console.WriteLine("Nenhum item encontrado.");
                    return;
                }

                foreach (var item in itens)
                {
                    Console.WriteLine($"ID: {item.Id}");
                    Console.WriteLine($"Modelo: {item.Modelo}");
                    Console.WriteLine($"Quantidade: {item.Quantidade}");
                    Console.WriteLine($"Tipo: {item.Tipo}");
                    Console.WriteLine($"Data de Vencimento: {item.DataVencimento.ToShortDateString()}");
                    Console.WriteLine($"Data de Entrada: {item.DataEntrada.ToShortDateString()}");
                    Console.WriteLine($"Nome do Fornecedor: {item.NomeFornecedor}");
                    Console.WriteLine($"Valor de Compra: {item.ValorCompra:C}");
                    Console.WriteLine($"Valor de Venda: {item.ValorVenda:C}");
                    Console.WriteLine($"Marca: {item.Marca.Nome}");
                    Console.WriteLine("------------------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        private void AtualizarItem()
        {
            try
            {
                Console.WriteLine("\n--- Atualizar Item ---");
                int id = int.Parse(Prompt("ID do item para atualizar: "));
                var item = new Item { Id = id };

                item.Modelo = Prompt("Novo Modelo: ");
                item.Quantidade = int.Parse(Prompt("Nova Quantidade: "));
                item.Tipo = Prompt("Novo Tipo: ");
                item.DataVencimento = DateTime.Parse(Prompt("Nova Data de Vencimento (yyyy-mm-dd): "));
                item.DataEntrada = DateTime.Now; // Data de entrada é a data atual
                item.NomeFornecedor = Prompt("Novo Nome do Fornecedor: ");
                item.ValorCompra = double.Parse(Prompt("Novo Valor de Compra: "));
                item.ValorVenda = double.Parse(Prompt("Novo Valor de Venda: "));

                // Listar marcas disponíveis e permitir que o usuário escolha uma
                ListarMarcas();
                int marcaId = int.Parse(Prompt("ID da Marca: "));
                item.Marca = new Marca(marcaId, ""); // Aqui você pode buscar a marca pelo ID

                itemDAO.Update(item);
                Console.WriteLine("Item atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        private void ListarMarcas()
        {
            try
            {
                Console.WriteLine("\n--- Marcas Disponíveis ---");
                var marcas = marcaDAO.List();
                if (marcas.Count == 0)
                {
                    Console.WriteLine("Nenhuma marca encontrada.");
                    return;
                }

                foreach (var marca in marcas)
                {
                    Console.WriteLine($"ID: {marca.Id}, Nome: {marca.Nome}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        private string Prompt(string message, string defaultValue = "")
        {
            Console.Write(message);
            string input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? defaultValue : input;
        }
    }
}