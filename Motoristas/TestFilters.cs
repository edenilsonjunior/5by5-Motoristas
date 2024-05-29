using System.Xml.Linq;

namespace Motoristas
{
    internal class TestFilters
    {
        public static int GetCountRecords(List<PenalidadesAplicadas> lst) => lst.Count;

        public static List<PenalidadesAplicadas> FilterByCpf(List<PenalidadesAplicadas> lst) => lst.Where(p => p.Cpf.Substring(0, 3).Equals("237")).ToList();

        public static List<PenalidadesAplicadas> FilterByYear(List<PenalidadesAplicadas> lst) => lst.Where(p => p.VigenciaCadastro.Year == 2021).ToList();

        public static int FilterByRazaoSocial(List<PenalidadesAplicadas> lst) => lst.Where(p => p.RazaoSocial.Contains("LTDA")).ToList().Count;

        public static List<PenalidadesAplicadas> OrderByRazaoSocial(List<PenalidadesAplicadas> lst) => lst.OrderBy(p => p.RazaoSocial).ToList();

        public static void PrintData(List<PenalidadesAplicadas> lst)
        {
            foreach (var item in lst)
            {
                Console.WriteLine(item);
            }
        }

        public static string GenerateXML(List<PenalidadesAplicadas> lst)
        {
            if (lst.Count == 0)
                return "Nao existe registros na lista!";



            var penalidadeAplicada = new XElement("Root", from data in lst
                                                          select new XElement("motorista",
                                                              new XElement("razao_social", data.RazaoSocial),
                                                              new XElement("cnpj", data.Cnpj),
                                                              new XElement("nome_motorista", data.NomeMotorista),
                                                              new XElement("cpf", data.Cpf),
                                                              new XElement("vigencia_do_cadastro", data.VigenciaCadastro)
                                                          )
                                                        );
            return penalidadeAplicada.ToString();

        }
    }
}
