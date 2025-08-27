namespace ClimaApi.Modelos;

public class RegistroClima
{
    public int Id { get; set; }
    public string Cidade { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
    public double TemperaturaC { get; set; }
    public int Umidade { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime ObservadoEmUtc { get; set; }
    public DateTime CriadoEmUtc { get; set; } = DateTime.UtcNow;
}
