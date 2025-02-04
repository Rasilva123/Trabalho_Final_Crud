using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Crud_Cadastro_Estoque.dao
{
    internal class ItemDAO
    {
        public void Insert(Item item)
        {
            try
            {
                string sql = "INSERT INTO Itens (modelo, quantidade, tipo, data_vencimento, data_entrada, nome_fornecedor, valor_compra, valor_venda, marca_id) VALUES (@modelo, @quantidade, @tipo, @data_vencimento, @data_entrada, @nome_fornecedor, @valor_compra, @valor_venda, @marca_id)";
                MySqlCommand comando = new MySqlCommand(sql, Conexao.Conectar());

                comando.Parameters.AddWithValue("@modelo", item.Modelo);
                comando.Parameters.AddWithValue("@quantidade", item.Quantidade);
                comando.Parameters.AddWithValue("@tipo", item.Tipo);
                comando.Parameters.AddWithValue("@data_vencimento", item.DataVencimento);
                comando.Parameters.AddWithValue("@data_entrada", item.DataEntrada);
                comando.Parameters.AddWithValue("@nome_fornecedor", item.NomeFornecedor);
                comando.Parameters.AddWithValue("@valor_compra", item.ValorCompra);
                comando.Parameters.AddWithValue("@valor_venda", item.ValorVenda);

                // Aqui você deve passar o ID da marca, se existir
                comando.Parameters.AddWithValue("@marca_id", item.Marca != null ? item.Marca.Id : (object)DBNull.Value);

                comando.ExecuteNonQuery();
                Console.WriteLine("Item inserido com sucesso!");

                Conexao.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir item: " + ex.Message);
            }
        }

        public void Delete(Item item)
        {
            try
            {
                string sql = "DELETE FROM Itens WHERE id = @id";
                MySqlCommand comando = new MySqlCommand(sql, Conexao.Conectar());
                comando.Parameters.AddWithValue("@id", item.Id);
                comando.ExecuteNonQuery();
                Console.WriteLine("Item excluído com sucesso!");
                Conexao.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir o item: {ex.Message}");
            }
        }

        public void Update(Item item)
        {
            try
            {
                string sql = "UPDATE Itens SET modelo = @modelo, quantidade = @quantidade, tipo = @tipo, data_vencimento = @data_vencimento, data_entrada = @data_entrada, nome_fornecedor = @nome_fornecedor, valor_compra = @valor_compra, valor_venda = @valor_venda, marca_id = @marca_id WHERE id = @id";
                MySqlCommand comando = new MySqlCommand(sql, Conexao.Conectar());
                comando.Parameters.AddWithValue("@modelo", item.Modelo);
                comando.Parameters.AddWithValue("@quantidade", item.Quantidade);
                comando.Parameters.AddWithValue("@tipo", item.Tipo);
                comando.Parameters.AddWithValue("@data_vencimento", item.DataVencimento);
                comando.Parameters.AddWithValue("@data_entrada", item.DataEntrada);
                comando.Parameters.AddWithValue("@nome_fornecedor", item.NomeFornecedor);
                comando.Parameters.AddWithValue("@valor_compra", item.ValorCompra);
                comando.Parameters.AddWithValue("@valor_venda", item.ValorVenda);
                comando.Parameters.AddWithValue("@marca_id", item.Marca != null ? item.Marca.Id : (object)DBNull.Value); // Corrigido para usar marca_id
                comando.Parameters.AddWithValue("@id", item.Id);
                comando.ExecuteNonQuery();
                Console.WriteLine("Item atualizado com sucesso!");
                Conexao.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar o item: {ex.Message}");
            }
        }

        public List<Item> List()
        {
            List<Item> itens = new List<Item>();
            try
            {
                // Atualizando a consulta para incluir informações da tabela Marca
                var sql = @"
            SELECT Itens.*, Marca.nome AS marca_nome 
            FROM Itens 
            LEFT JOIN Marca ON Itens.marca_id = Marca.id 
            ORDER BY Itens.modelo";

                MySqlCommand comando = new MySqlCommand(sql, Conexao.Conectar());
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Item item = new Item(
                            dr.GetInt32("id"),
                            dr.GetString("modelo"),
                            dr.GetInt32("quantidade"),
                            dr.GetString("tipo"),
                            dr.GetDateTime("data_vencimento"),
                            dr.GetDateTime("data_entrada"),
                            dr.GetString("nome_fornecedor"),
                            dr.GetDouble("valor_compra"),
                            dr.GetDouble("valor_venda"),
                            new Marca(dr.IsDBNull(dr.GetOrdinal("marca_id")) ? 0 : dr.GetInt32("marca_id"), dr.IsDBNull(dr.GetOrdinal("marca_nome")) ? "N/A" : dr.GetString("marca_nome")) // Aqui você pode usar o ID e o nome da marca
                        );
                        itens.Add(item);
                    }
                }
                Conexao.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar itens: {ex.Message}");
            }
            return itens;
        }

    }
}