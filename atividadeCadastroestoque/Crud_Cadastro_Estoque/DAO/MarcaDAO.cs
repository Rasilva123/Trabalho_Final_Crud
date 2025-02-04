using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Crud_Cadastro_Estoque.dao
{
    internal class MarcaDAO
    {
        public void Insert(Marca marca)
        {
            try
            {
                string sql = "INSERT INTO Marca (nome) VALUES (@nome)";
                MySqlCommand comando = new MySqlCommand(sql, Conexao.Conectar());
                comando.Parameters.AddWithValue("@nome", marca.Nome);
                comando.ExecuteNonQuery();
                Console.WriteLine("Marca inserida com sucesso!");
                Conexao.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir marca: " + ex.Message);
            }
        }

        public List<Marca> List()
        {
            List<Marca> marcas = new List<Marca>();
            try
            {
                var sql = "SELECT * FROM Marca ORDER BY nome";
                MySqlCommand comando = new MySqlCommand(sql, Conexao.Conectar());
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Marca marca = new Marca(dr.GetString("nome"))
                        {
                            Id = dr.GetInt32("id")
                        };
                        marcas.Add(marca);
                    }
                }
                Conexao.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar marcas: {ex.Message}");
            }
            return marcas;
        }
    }
}