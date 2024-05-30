using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motoristas
{
    internal class DatabaseSql
    {
        private static string _conexao = "Data Source=127.0.0.1; Initial Catalog=DBMotorista; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=Yes";


        public static void SaveData(List<PenalidadesAplicadas> list)
        {
            bool erro = false;
            int batchSize = 400;
            int totalInsercoes = 0;

            Stopwatch stopwatch = new Stopwatch();
            try
            {
                using var conexao = new SqlConnection(_conexao);
                conexao.Open();

                stopwatch.Start();
                for (int i = 0; i < list.Count; i += batchSize)
                {
                    var batchList = list.Skip(i).Take(batchSize).ToList();

                    string sql = "INSERT INTO Penalidade VALUES ";

                    for (int j = 0; j < batchList.Count; j++)
                    {
                        sql += $"(@razao_social{j}, @cnpj{j}, @nome_motorista{j}, @cpf{j}, @vigencia_do_cadastro{j}), ";
                    }

                    sql = sql.Substring(0, sql.Length - 2);
                    using var cmd = new SqlCommand(sql, conexao);

                    int total = 0;

                    foreach (var item in batchList)
                    {
                        cmd.Parameters.Add($"@razao_social{total}", SqlDbType.VarChar, 50).Value = item.RazaoSocial;
                        cmd.Parameters.Add($"@cnpj{total}", SqlDbType.VarChar, 18).Value = item.Cnpj;
                        cmd.Parameters.Add($"@nome_motorista{total}", SqlDbType.VarChar, 40).Value = item.NomeMotorista;
                        cmd.Parameters.Add($"@cpf{total}", SqlDbType.VarChar, 14).Value = item.Cpf;
                        cmd.Parameters.Add($"@vigencia_do_cadastro{total}", SqlDbType.DateTime).Value = item.VigenciaCadastro;
                        total++;
                        Console.WriteLine("Total de insercoes: " + totalInsercoes++);
                    }

                    cmd.ExecuteNonQuery();

                }

                conexao.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro SQL: {ex.Message}");
                erro = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                erro = true;
            }
            stopwatch.Stop();


            if (!erro)
                Console.WriteLine($"Dados inseridos com sucesso! Tempo total: {stopwatch.Elapsed.TotalSeconds} segundos");
        }



        public static List<PenalidadesAplicadas> LoadData()
        {

            var lst = new List<PenalidadesAplicadas>();

            try
            {
                using var conexao = new SqlConnection(_conexao);

                var cmd = new SqlCommand("SELECT * FROM Penalidade", conexao);
                conexao.Open();
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lst.Add(new PenalidadesAplicadas()
                    {
                        RazaoSocial = reader.GetString(0),
                        Cnpj = reader.GetString(1),
                        NomeMotorista = reader.GetString(2),
                        Cpf = reader.GetString(3),
                        VigenciaCadastro = reader.GetDateTime(4)
                    });
                }
                conexao.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            return lst;
        }


        public static void InserirControle(string descricao, DateTime dataProcessamento, int quantidadeInsercoes)
        {

            try
            {
                using var conexao = new SqlConnection(_conexao);

                using var proc = new SqlCommand("InserirControle", conexao)
                {
                    CommandType = CommandType.StoredProcedure,
                    Parameters =
                    {
                        new SqlParameter("@descricao", descricao),
                        new SqlParameter("@data_processamento", dataProcessamento),
                        new SqlParameter("@quantidade_insercoes", quantidadeInsercoes)
                    }
                };

                conexao.Open();
                proc.ExecuteNonQuery();
                conexao.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}
