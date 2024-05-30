using Microsoft.Data.SqlClient;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motoristas
{
    internal class DatabaseMongo
    {
        public static string Conexao
        {
            get => "mongodb://root:Mongo%402024%23@localhost:27017/";
        }

        public DatabaseMongo()
        {

        }

        public static void ProcessDataToMongoDB()
        {
            IMongoCollection<BsonDocument> collection;

            var client = new MongoClient(Conexao);
            var database = client.GetDatabase("DBMotorista");
            collection = database.GetCollection<BsonDocument>("Penalidade");

            var penalidades = DatabaseSql.LoadData();
            int total = 1;
            foreach (var p in penalidades)
            {
                var document = new BsonDocument
                {
                    {"razao_social", p.RazaoSocial },
                    {"cnpj", p.Cnpj },
                    {"nome_motorista", p.NomeMotorista },
                    {"cpf", p.Cpf },
                    {"vigencia_do_cadastro", p.VigenciaCadastro.ToString("dd/MM/yyyy") }
                };
                collection.InsertOne(document);
                total++;
                Console.WriteLine("Total inseridos: " + total);
            }
            DatabaseSql.InserirControle("Insercao de registros no mongoDB", DateTime.Now, penalidades.Count);
        }
    }
}
