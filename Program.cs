using ClimaApi.Dados;
using ClimaApi.Servicos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar pol�tica de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirBlazor",
        policy =>
        {
            policy.WithOrigins("https://localhost:7208") // endere�o do Blazor
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


// Banco de dados SQL Server
builder.Services.AddDbContext<ClimaDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

// Servi�o de clima (HttpClient injetado)
builder.Services.AddHttpClient<ClimaServico>();

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Usar Swagger em Development (VS j� define ambiente)
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
