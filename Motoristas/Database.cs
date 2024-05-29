using Microsoft.Data.SqlClient;
using System.Data;

namespace Motoristas
{
    internal class Database
    {
        private static string _conexao = "Data Source=127.0.0.1; Initial Catalog=DBMotorista; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=Yes";

        public static void SaveData(List<PenalidadesAplicadas> list)
        {
            int total = 0;
            bool erro = false;
            try
            {
                using var conexao = new SqlConnection(_conexao);
                conexao.Open();

                foreach (var item in list)
                {
                    using var proc = new SqlCommand("InserirPenalidade", conexao)
                    {
                        Parameters =
                        {
                            new SqlParameter("@cnpj", item.Cnpj),
                            new SqlParameter("@razao_social", item.RazaoSocial),
                            new SqlParameter("@nome_motorista", item.NomeMotorista),
                            new SqlParameter("@cpf", item.Cpf),
                            new SqlParameter("@vigencia_do_cadastro", DateOnly.FromDateTime(item.VigenciaCadastro).ToString())
                        },
                        CommandType = CommandType.StoredProcedure
                    };
                    proc.ExecuteNonQuery();
                    total++;
                    Console.WriteLine("Total de linhas inseridas no banco: " + total);
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

            if (erro == false)
                Console.WriteLine("Dados inseridos com sucesso!");
        }

    }
}
