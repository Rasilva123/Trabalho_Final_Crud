using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadastro_Aluno.Modelos;
using Cadastro_Modelo.Modelos;
using MySql.Data.MySqlClient;


namespace Cadastro_Aluno.dao
{
    internal class ModeloDao
    {
        public void insert(Modelo modelo)
        {

           try
           {
                     string sql = "INSERT INTO Modelo(descricao, eixo, peso, passageiro, cavalo, cilindrada, fk_marca_id) " +
                     "VALUES(@descricao, @eixo, @peso, @passageiro, @cavalo, @cilindrada, @fk_marca_id)";

               using (MySqlConnection conexao = Conexao.Conectar())
               {
                   using (MySqlCommand command = new MySqlCommand(sql, conexao))
                   {
                       command.Parameters.AddWithValue("@descricao", modelo.descricao);
                       command.Parameters.AddWithValue("@eixo", modelo.eixo);
                       command.Parameters.AddWithValue("@peso", modelo.peso);
                       command.Parameters.AddWithValue("@passageiro", modelo.passageiro);
                       command.Parameters.AddWithValue("@cavalo", modelo.cavalo);
                       command.Parameters.AddWithValue("@cilindrada", modelo.cilindrada);

                      
                       if (modelo.fk_marca_id == null)
                       {
                           throw new ArgumentException("fk_marca_id não pode ser nulo.");
                       }

                       command.Parameters.Add("@fk_marca_id", MySqlDbType.Int32).Value = modelo.fk_marca_id.id_marca;

                      
                       command.ExecuteNonQuery();
                       Console.WriteLine("Modelo de veículo cadastrado com sucesso!");
                   }
               }
           }
           catch (Exception ex)
           {
                Console.WriteLine("Erro ao cadastrar Modelo!" + ex.Message);

           }
           finally
           {
                Conexao.FecharConexao();
           }


        }
        public void Delete(Modelo modelo)
        {
            try
            {
                string sql = "DELETE FROM Modelo WHERE id_modelo = @id_modelo";
                using (MySqlConnection conexao = Conexao.Conectar())
                {
                    using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@id_modelo", modelo.id_modelo);
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir Modelo!" + ex.Message);
            }

        }

        public List<Modelo> List()
        {
            List<Modelo> modelos = new List<Modelo>();

            string sql = "SELECT Modelo.id_modelo, Modelo.descricao, Modelo.eixo, Modelo.peso, Modelo.passageiro, Modelo.cavalo, Modelo.cilindrada, " +
             "Marca.id_marca, Marca.nome FROM Modelo " +
             "INNER JOIN Marca ON Modelo.fk_marca_id = Marca.id_marca";


            try
            {

                using (MySqlConnection conexao = Conexao.Conectar())
                {
                    using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                    {
                        using (MySqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Modelo modelo = new Modelo
                                {
                                    id_modelo = dr.GetInt32("id_modelo"),
                                    descricao = dr.GetString("descricao"),
                                    eixo = dr.GetString("eixo"),
                                    peso = dr.GetString("peso"),
                                    passageiro = dr.GetString("passageiro"),
                                    cavalo = dr.GetString("cavalo"),
                                    cilindrada = dr.GetString("cilindrada"),
                                    fk_marca_id = new Marca
                                    {
                                        id_marca = dr.GetInt32("id_marca"),
                                        nome = dr.GetString("nome"),
                                    }
                                };
                                modelos.Add(modelo);
                            }
                        }
                    }
                }
                //Conexao.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os Modelos! {ex.Message}");
            }
            finally
            {
                Conexao.FecharConexao();
            }
            return modelos;
        }


        
        public bool Update(Modelo modelo)
        {
            try
            {
                string sql = "UPDATE Modelo SET descricao = @descricao, eixo = @eixo, peso =  " +
                    "@peso, passageiro = @passageiro, cavalo = @cavalo, cilindrada = @cilindrada," +
                    " fk_marca_id = @fk_marca_id WHERE id_modelo = @id_modelo";

                MySqlConnection onectar = Conexao.Conectar();
                MySqlCommand comando = new MySqlCommand(sql, Conexao.Conectar());
                comando.Parameters.AddWithValue("@descricao", modelo.descricao);
                comando.Parameters.AddWithValue("@eixo", modelo.eixo);
                comando.Parameters.AddWithValue("@peso", modelo.peso);
                comando.Parameters.AddWithValue("@passageiro", modelo.passageiro);
                comando.Parameters.AddWithValue("@cavalo", modelo.cavalo);
                comando.Parameters.AddWithValue("@cilindradas", modelo.cilindrada);
                comando.Parameters.AddWithValue("@fk_marca_id", modelo.fk_marca_id.id_marca);
                
                int rowsAffected = comando.ExecuteNonQuery();
                return rowsAffected > 0;
                Console.WriteLine("Modelo de Veiculo atualizado com sucesso!");

               // Conexao.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar modelode veiculo {ex.Message}");
            }
            finally
            {
                Conexao.FecharConexao();
            }
        }





        public Marca GetMarcaById(int idMarca)
        {
            Marca marca = null;

            string sql = "SELECT * FROM Marca WHERE id_marca = @id_marca";
            using (MySqlConnection conexao = Conexao.Conectar())
            {
                using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@id_marca", idMarca);
                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            marca = new Marca
                            {
                                id_marca = dr.GetInt32("id_marca"),
                                nome = dr.GetString("nome")
                            };
                        }
                    }
                }
            }

            return marca;
        }

        public void InsertMarca(Marca marca)
        {
            string sql = "INSERT INTO Marca (nome) VALUES (@nome)";
            using (MySqlConnection conexao = Conexao.Conectar())
            {
                using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@nome", marca.nome);
                    comando.ExecuteNonQuery();
                }
            }
        }



        public void Menu()
        {
            int opcao;

            ModeloDao adao = new ModeloDao();

            do
            {

                Console.WriteLine("Escolha uma operação:");
                Console.WriteLine("1 - Inserir Modelo \n2 - Deletar Modelo \n3 - Listar Modelos \n4 - Atualizar Modelo \n0 - Para sair do programa  ");
                opcao = int.Parse(Console.ReadLine());

                //List<Modelo> modelotodos = adao.List();

                switch (opcao)
                {
                    case 1:
                        try
                        {
                            Modelo novomod = new Modelo();

                            Console.WriteLine("Insira a descrição do Modelo: ");
                            novomod.descricao = Console.ReadLine();

                            Console.WriteLine("Insira a quantidade de eixo: ");
                            novomod.eixo = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Insira o peso do veiculo: ");
                            novomod.peso = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Insira a quantidade de passageiros: ");
                            novomod.passageiro = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Insira a quantidade de cavalos do veiculo:");
                            novomod.cavalo = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Insira a quantidade de cilindradas:");
                            novomod.cilindrada = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Insira o ID da marca que deseja associar ao modelo:");
                            int idMarca = int.Parse(Console.ReadLine());

                           
                            Marca marca = adao.GetMarcaById(idMarca);

                            if (marca == null)
                            {
                                
                                marca = new Marca { id_marca = idMarca, nome = "Nova Marca" };
                                adao.InsertMarca(marca);
                            }

                            novomod.fk_marca_id = marca;

                            adao.insert(novomod);

                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Erro ao inserir Modelo!" + ex.Message);
                        }

                        break;

                    case 2:
                        try
                        {
                            List<Modelo> modelotodos = adao.List();
                            Console.WriteLine("Digite o ID do Modelo que deseja deletar:");
                            int id = int.Parse(Console.ReadLine());

                            Modelo deletarModelo = modelotodos.FirstOrDefault(x => x.id_modelo == id);
                            if (deletarModelo != null)
                            {
                                adao.Delete(deletarModelo);
                                Console.WriteLine("Modelo excluído com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Modelo não encontrado. Verifique o ID informado.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao excluir o Modelo: " + ex.Message);
                        }
                        break;

                    case 3:
                        try
                        {
                            List<Modelo> modelotodos = adao.List();
                            foreach (Modelo modelo3 in modelotodos)
                            {
                                Console.ForegroundColor = ConsoleColor.Red; 
                                Console.WriteLine("--------------------------------------");
                                Console.WriteLine($"ID: {modelo3.id_modelo}");
                                Console.WriteLine($"Descrição: {modelo3.descricao}");
                                Console.WriteLine($"Eixo: {modelo3.eixo}");
                                Console.WriteLine($"Peso: {modelo3.peso}");
                                Console.WriteLine($"Passageiro: {modelo3.passageiro}");
                                Console.WriteLine($"Cavalo: {modelo3.cavalo}");
                                Console.WriteLine($"Cilindradas: {modelo3.cilindrada}");
                                Console.WriteLine("--------------------------------------");
                                Console.ResetColor(); 
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao listar os Modelos: " + ex.Message);
                        }

                        break;

                    case 4:
                        try
                        {
                            List<Modelo> modelotodos = adao.List();
                            Console.WriteLine("Digite o ID do Modelo que deseja atualizar:");
                            int id = int.Parse(Console.ReadLine());

                            Modelo atualizaModelo = modelotodos.FirstOrDefault(x => x.id_modelo == id);
                            if (atualizaModelo != null)
                            {
                                Console.WriteLine("Insira uma nova descrição, ou deixe em branco para manter o de antes:");
                                string descricao = Console.ReadLine();
                                if (!string.IsNullOrEmpty(descricao))
                                {
                                    atualizaModelo.descricao = descricao;
                                }

                                Console.WriteLine("Insira uma nova quantidade de eixos, ou deixe em branco para manter o que estava antes:");
                                string eixo = Console.ReadLine();
                                if (!string.IsNullOrEmpty(eixo))
                                {
                                    atualizaModelo.eixo = eixo;
                                }

                                Console.WriteLine("Insira um novo peso, ou deixe em branco para manter o que estava antes:");
                                string peso = Console.ReadLine();
                                if (!string.IsNullOrEmpty(peso))
                                {
                                    atualizaModelo.peso = peso;
                                }

                                Console.WriteLine("Insira uma nova quantidade de passageiros, ou deixe em branco para manter o que estava antes:");
                                string passageiro = Console.ReadLine();
                                if (!string.IsNullOrEmpty(passageiro))
                                {
                                    atualizaModelo.passageiro = passageiro;
                                }

                                Console.WriteLine("Insira uma nova quantidade de cavalos, ou deixe em branco para manter o que estava antes:");
                                string cavalo = Console.ReadLine();
                                if (!string.IsNullOrEmpty(cavalo))
                                {
                                    atualizaModelo.cavalo = cavalo;
                                }

                                Console.WriteLine("Insira uma nova quantidade de cilindradas, ou deixe em branco para manter o que estava antes:");
                                string cilindrada = Console.ReadLine();
                                if (!string.IsNullOrEmpty(cilindrada))
                                {
                                    atualizaModelo.cilindrada = cilindrada;
                                }

                                adao.Update(atualizaModelo);
                                Console.WriteLine("Modelo atualizado com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Modelo não encontrado. Verifique o ID informado.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao atualizar o Modelo: " + ex.Message);
                        }
                        break;
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Por favor, escolha uma operação válida.");
                        break;
                }

            } while (opcao != 0);
        }
    }
}
