using Newtonsoft.Json;

namespace Motoristas
{
    internal class PenalidadesAplicadas
    {
        [JsonProperty("razao_social")]
        public string RazaoSocial { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("nome_motorista")]
        public string NomeMotorista { get; set; }
        
        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("vigencia_do_cadastro")]
        public DateTime VigenciaCadastro { get; set; }

        public override string ToString() => $"RazaoSocial: {RazaoSocial}, CNPJ: {Cnpj}, NomeMotorista: {NomeMotorista}, CPF: {Cpf}, Vigencia cadastro: {VigenciaCadastro}";
       
    }
}
