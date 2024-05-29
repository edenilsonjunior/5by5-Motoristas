using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Motoristas
{
    internal class ReadFile
    {
        public static List<PenalidadesAplicadas> GetData(string path)
        {
            using StreamReader r = new StreamReader(path);
            string jsonStr = r.ReadToEnd();

            var lst = JsonConvert.DeserializeObject<MotoristaHabilitado>(jsonStr, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

            if (lst == null)
                return null;

            return lst.PenalidadesAplicadas;
        }

    }
}
