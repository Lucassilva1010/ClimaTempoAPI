using System.Net.Http.Json;
using System.Text.Json;
using ClimaApi.Dados;
using ClimaApi.Modelos;
using Microsoft.Win32;

namespace ClimaApi.Servicos;

public class ClimaServico
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;
    private readonly ClimaDbContext _contexto;

    public ClimaServico(HttpClient http, IConfiguration config , ClimaDbContext contexto)
    {
        _http = http;
        _config = config;
        _contexto = contexto;
    }

    public async Task<RegistroClima> ObterClimaAtualAsync(string cidade, bool salvar = false)
    {
        var chave = _config["OpenWeather:ApiKey"];
        var url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={chave}&units=metric&lang=pt_br";

        try
        {
            // Faz a requisição manualmente
            var respostaHttp = await _http.GetAsync(url);

            if (!respostaHttp.IsSuccessStatusCode)
            {
                if (respostaHttp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new Exception("A chave da API do OpenWeather é inválida ou ainda não foi ativada. Verifique no site do OpenWeather.");

                if (respostaHttp.StatusCode == System.Net.HttpStatusCode.NotFound)
                    throw new Exception($"Cidade '{cidade}' não encontrada.");

                throw new Exception($"Erro ao consultar OpenWeather: {respostaHttp.StatusCode}");
            }

            // Lê o JSON cru
            var json = await respostaHttp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            // Se não existir "name", significa que não retornou cidade válida
            if (!root.TryGetProperty("name", out var nomeCidade))
                throw new Exception($"Não foi possível obter dados de clima para '{cidade}'.");

            var registro = new RegistroClima
            {
                Cidade = nomeCidade.GetString() ?? "",
                Pais = root.GetProperty("sys").GetProperty("country").GetString() ?? "",
                TemperaturaC = root.GetProperty("main").GetProperty("temp").GetDouble(),
                Umidade = root.GetProperty("main").GetProperty("humidity").GetInt32(),
                Descricao = root.GetProperty("weather")[0].GetProperty("description").GetString() ?? "",
                ObservadoEmUtc = DateTime.UtcNow
            };

            if (salvar)
            {
                _contexto.RegistrosClima.Add(registro);
                await _contexto.SaveChangesAsync();
            }

            return registro;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao buscar clima: {ex.Message}");
        }
    }
}
