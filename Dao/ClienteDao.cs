using ProjetoLoja.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLoja.Dao
{
    public class ClienteDao : DbSql
    {
        public void InserirCliente(ClienteDto cliente)
        {
            using (SqlConnection conexao = new SqlConnection(conexaoSqlServer))
            {
                try
                {
                    string sql = "INSERT INTO ClienteLoja (Nome,Endereco,Email,Telefone,Sexo) VALUES(@Nome,@Endereco,@Email,@Telefone,@Sexo)";
                    SqlCommand cmd = new SqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                    cmd.Parameters.AddWithValue("@Email", cliente.Email);
                    cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
                    conexao.Open();
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Cadastrado com Sucesso");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro para Cadastrar" + e.Message);
                }
            }
        }

        public void AlteraCliente(ClienteDto cliente)
        {
            using(SqlConnection conexao = new SqlConnection(conexaoSqlServer))
            {
                try
                {
                    //verifica se o id Existe no banco
                    //string verificaIdSql = "SELECT COUNT(*) FROM ClienteLoja WHERE Id = @Id";
                    //SqlCommand verificaIdCmd = new SqlCommand(verificaIdSql, conexao);
                    //verificaIdCmd.Parameters.AddWithValue("@Id",cliente.id);
                    //conexao.Open();
                    //int count = Convert.ToInt32(verificaIdCmd.ExecuteScalar());

                    //if (count > 0)
                  //  {
                        string sql = "UPDATE ClienteLoja SET Nome =@Nome, Endereco =@Endereco, Email =@Email, Telefone =@Telefone, Sexo =@Sexo WHERE Id =@Id";
                        SqlCommand cmd = new SqlCommand(sql, conexao);
                        cmd.Parameters.AddWithValue("@Id", cliente.id);
                        cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                        cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email);
                        cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                        cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
                       conexao.Open();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Alterado com Sucesso");
                    //}
                    //else
                    //{
                    //    Console.WriteLine("ID não existe");
                    //}
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro para Alterar" + e.Message);
                }
            }
        }

        public void DeletarCliente(int id)
        {
            using (SqlConnection conexao = new SqlConnection(conexaoSqlServer))
            {
                try
                {
                    //verifica se o id Existe no banco
                    string verificaIdSql = "SELECT COUNT(*) FROM ClienteLoja WHERE Id = @Id";
                    SqlCommand verificaIdCmd = new SqlCommand(verificaIdSql, conexao);
                    verificaIdCmd.Parameters.AddWithValue("@Id", id);
                    conexao.Open();
                    int count = Convert.ToInt32(verificaIdCmd.ExecuteScalar());

                    //se existir ele deleta
                    if (count > 0)
                    {
                        string deleteSql = "DELETE FROM ClienteLoja WHERE Id = @Id";
                        SqlCommand deleteCmd = new SqlCommand(deleteSql, conexao);
                        deleteCmd.Parameters.AddWithValue("@Id", id);
                        deleteCmd.ExecuteNonQuery();
                        Console.WriteLine("Deletado com sucesso.");
                    }
                    //se nao ele diz que o id nao existe
                    else
                    {
                        Console.WriteLine("ID não existe");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao Apagar Cliente: " + e.Message);
                }
            }
        }

        public void MostrarClientes()
        {
            using(SqlConnection conexao = new SqlConnection(conexaoSqlServer))
            {
                try
                {
                    //seleciona a tabela ClienteLoja 
                    string sql = "SELECT * FROM ClienteLoja";
                    SqlCommand cmd = new SqlCommand(sql, conexao);
                    conexao.Open();
                    SqlDataReader tabela = cmd.ExecuteReader();

                    //se tiver algo na lista ele retorna true, se não é false e cai no 'else'
                    if (tabela.HasRows)
                    {
                        //pega o retorno dos dados do SQL e coloca em variaveis
                        while (tabela.Read())
                        {
                            int id = int.Parse(tabela["Id"].ToString());
                            string nome = tabela["Nome"].ToString();
                            string endereco = tabela["Endereco"].ToString();
                            string email = tabela["Email"].ToString();
                            string telefone = tabela["Telefone"].ToString();
                            string sexo = tabela["Sexo"].ToString();

                            Console.WriteLine("Id do cliente: " + id);
                            Console.WriteLine("Nome: " + nome);
                            Console.WriteLine("Endereço: " + endereco);
                            Console.WriteLine("E-mail: " + email);
                            Console.WriteLine("Telefone: " + telefone);
                            Console.WriteLine("Sexo: " + sexo);
                            Console.WriteLine("==================");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Lista de clientes vazia\n");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro para consultar " + e.Message);
                }
            }
        }

        // verifica se id para existe no banco de dados
        public bool VerificaId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(conexaoSqlServer))
            {
                try
                {
                    //verifica se o id Existe no banco
                    string verificaIdSql = "SELECT COUNT(*) FROM ClienteLoja WHERE Id = @Id";
                    SqlCommand verificaIdCmd = new SqlCommand(verificaIdSql, conexao);
                    verificaIdCmd.Parameters.AddWithValue("@Id",id);
                    conexao.Open();
                    int count = Convert.ToInt32(verificaIdCmd.ExecuteScalar());

                    if(count == 0)
                    {
                       // Console.WriteLine("ID não existe");
                        return true;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro para Alterar" + e.Message);
                }
            }
            return false;
        }

    }
}
