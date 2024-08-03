// Inicialize o cliente RestSharp com a URL base
using RestSharp;
using Newtonsoft.Json;
using System.Data;
using ConsoleFindObjetosNoTFS;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

List<string> ObjetosBusca = new List<string>();
// aqui coloque o texto que irá procurar nos repositorios
ObjetosBusca.Add("bd043tswp");

var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration configuration = builder.Build();

// Criar o DataTable
DataTable dataTable = new DataTable();
dataTable.Columns.Add("searchText");
dataTable.Columns.Add("FonteOrigem");
dataTable.Columns.Add("FileName");
dataTable.Columns.Add("Path");
dataTable.Columns.Add("Project");
dataTable.Columns.Add("Repository");

BuscaNoAzure(dataTable, ObjetosBusca);
BuscaNoTFS(dataTable, ObjetosBusca);

var guid = Guid.NewGuid();
ExcelGenerator csvGenerator = new ExcelGenerator();
string filePath = Path.Combine(configuration["AppSettings:DiretorioSaidaRelatorio"], $"{guid}.xlsx");

csvGenerator.GenerateExcelFile(dataTable, filePath);

Console.WriteLine($"Arquivo {guid} gerado com sucesso");

void BuscaNoTFS(DataTable dt, List<string> ObjetosBusca)
{
    foreach (var objeto in ObjetosBusca)
    {
        var client = new RestClient(configuration["AppSettings:UrlTFS"]);
 
        // Crie a requisição do tipo POST
        var request = new RestRequest();
        request.Method = Method.Post;

        // Adicione os cabeçalhos necessários
        // token TFS
        request.AddHeader("Authorization", $"Basic {configuration["AppSettings:TokenTFS"]}");

         request.AddHeader("Content-Type", "application/json");

        // Defina o objeto para o corpo da requisição
        var payload = new
        {
            searchText = $"{objeto}",
            scope = "Team Foundation Server",
            skipResults = 0,
            takeResults = 1000,
            sortOptions = ""
        };

        // Converta o objeto para JSON
        var body = JsonConvert.SerializeObject(payload);

        // Adicione o corpo da requisição
        request.AddParameter("application/json", body, ParameterType.RequestBody);

        try
        {
            // Execute a requisição
            var response = client.Execute(request);

            // Verifique o sucesso da requisição
            if (response.IsSuccessful)
            {
                Console.WriteLine("Resposta recebida com sucesso:");
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                Console.WriteLine($"Conteúdo do erro: {response.Content}");
            }

            var searchResponse = JsonConvert.DeserializeObject<SearchResponse>(response.Content);



            foreach (var value in searchResponse.Results.Values)
            {
                DataRow row = dataTable.NewRow();
                row["searchText"] = payload.searchText;
                row["FonteOrigem"] = "TFS";
                row["FileName"] = value.FileName;
                row["Path"] = value.Path;
                row["Project"] = value.Project;
                row["Repository"] = value.Repository;
                dataTable.Rows.Add(row);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro: {ex.Message}");
        }
    }

}

void BuscaNoAzure(DataTable dt, List<string> ObjetosBusca)
{
    foreach (var objeto in ObjetosBusca)
    {
        var client = new RestClient(configuration["AppSettings:UrlAzure"]);

        // Crie a requisição do tipo POST
        var request = new RestRequest();
        request.Method = Method.Post;

        // token do Azure
        request.AddHeader("Authorization", $"Basic {configuration["AppSettings:TokenAzure"]}");
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Cookie", "VstsSession=%7B%22PersistentSessionId%22%3A%220298cb6c-bd91-4a54-91e2-22bc07dba0fb%22%2C%22PendingAuthenticationSessionId%22%3A%2200000000-0000-0000-0000-000000000000%22%2C%22CurrentAuthenticationSessionId%22%3A%2200000000-0000-0000-0000-000000000000%22%2C%22SignInState%22%3A%7B%7D%7D");

        // Defina o objeto para o corpo da requisição
        var payload = new JObject
        {
            { "searchText" , $"{objeto}" },
            { "$skip" , 0 },
            { "$top", 1000 },
            { "includeFacets", true }
        };

        // Converta o objeto para JSON
        var body = JsonConvert.SerializeObject(payload);

        // Adicione o corpo da requisição
        request.AddParameter("application/json", body, ParameterType.RequestBody);

        try
        {
            // Execute a requisição
            var response = client.Execute(request);

            // Verifique o sucesso da requisição
            if (response.IsSuccessful)
            {
                Console.WriteLine("Resposta recebida com sucesso:");
                Console.WriteLine(response.Content);
            }
            else
            {
                Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                Console.WriteLine($"Conteúdo do erro: {response.Content}");
            }

            var searchResponse = JsonConvert.DeserializeObject<SearchResponseAzure>(response.Content);



            foreach (var value in searchResponse.results)
            {
                DataRow row = dataTable.NewRow();
                row["searchText"] = objeto;
                row["FonteOrigem"] = "AZURE";
                row["FileName"] = value.fileName;
                row["Path"] = value.path;
                row["Project"] = value.project.name;
                row["Repository"] = value.repository.name;
                dataTable.Rows.Add(row);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro: {ex.Message}");
        }
    }

}


