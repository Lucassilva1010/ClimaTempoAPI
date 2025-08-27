using ClimaApi.Dados;
using ClimaApi.Servicos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirBlazor",
        policy =>
        {
            policy.WithOrigins("https://localhost:7208") // endereço do Blazor
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


// Banco de dados SQL Server
builder.Services.AddDbContext<ClimaDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

// Serviço de clima (HttpClient injetado)
builder.Services.AddHttpClient<ClimaServico>();

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Usar Swagger em Development (VS já define ambiente)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ativar CORS antes de MapControllers
app.UseCors("PermitirBlazor");

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
