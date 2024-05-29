namespace Motoristas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lst = ReadFile.GetData(@"C:\teste_json\motoristas_habilitados.json");

            
            bool sair = false;

            while (!sair)
            {
                switch (Menu())
                {
                    case 1:
                        Console.WriteLine($"Quantidade de linhas: {TestFilters.GetCountRecords(lst)}");
                        break;
                    case 2:
                        Console.WriteLine($"=====Cpfs que comecam com 237=====\n\n");
                        TestFilters.PrintData(TestFilters.FilterByCpf(lst));
                        break;
                    case 3:
                        Console.WriteLine($"=====Registros que tenham o ano de vigencia igual a 2021=====\n\n");
                        TestFilters.PrintData(TestFilters.FilterByYear(lst));
                        break;
                    case 4:
                        Console.WriteLine($"Quantas empresas tem no nome da razao social a descricao LTDA: " + TestFilters.FilterByRazaoSocial(lst));
                        break;
                    case 5:
                        Console.WriteLine($"=====Ordenar a lista de registros pela razao social=====\n\n");
                        TestFilters.PrintData(TestFilters.OrderByRazaoSocial(lst));
                        break;
                    case 6:
                        Database.SaveData(lst);
                        break;
                    case 0:
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Resposta invalida!");
                        break;
                }

                Console.WriteLine("Digite qualquer tecla para continuar...");
                Console.ReadKey();
            }
            
        }

        private static int Menu()
        {
            Console.Clear();
            Console.WriteLine($"=====|Manipulacoes de json e linq|=====");
            Console.WriteLine("1- Quantidade de linhas:");
            Console.WriteLine("2- Registros que tenham o CPF iniciando com 237:");
            Console.WriteLine("3- Registros que tenham o ano de vigencia igual a 2021");
            Console.WriteLine("4- Quantas empresas tem no nome da razao social a descricao LTDA:");
            Console.WriteLine("5- Ordenar a lista de registros pela razao social");
            Console.WriteLine("6- Salvar dados no banco de dados");
            Console.WriteLine("0- Sair");
            Console.WriteLine($"=================================");

            return LerInt("Digite sua escolha:");
        }

        private static int LerInt(string titulo)
        {
            int resposta;

            Console.WriteLine(titulo);
            Console.Write("R: ");

            string? s = Console.ReadLine();


            while (!int.TryParse(s, out resposta))
            {
                Console.WriteLine("Resposta invalida! Tente novamente.");
                Console.WriteLine(titulo);
                Console.Write("R: ");
                s = Console.ReadLine();
            }

            return resposta;
        }
    }
}
