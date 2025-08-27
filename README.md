# 🌤️ ClimaTempoAPI - API de Previsão do Tempo

[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-WebAssembly-blue.svg)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

Uma moderna aplicação web para consulta de previsão do tempo desenvolvida com .NET 8 e Blazor WebAssembly, oferecendo uma interface intuitiva e uma API RESTful robusta.

## ✨ Funcionalidades

- 🔍 **Busca por cidade** - Obtenha condições climáticas atuais de qualquer cidade
- 💾 **Armazenamento de histórico** - Salve consultas no banco de dados para referência futura
- 📊 **Dados detalhados** - Temperatura, umidade e condições atuais
- 📜 **Consulta de histórico** - Acesse todas as consultas climáticas salvas anteriormente
- 📱 **Design responsivo** - Interface adaptável para desktop e dispositivos móveis
- ⚡ **Performance otimizada** - Desempenho acelerado com Blazor WebAssembly
- 🔒 **Segurança** - Tratamento seguro de chaves de API e dados sensíveis

## 🛠️ Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **Blazor WebAssembly** - Interface web interativa
- **Entity Framework Core** - ORM para acesso a dados
- **SQL Server** - Banco de dados para armazenamento do histórico
- **OpenWeatherMap API** - Fonte de dados meteorológicos
- **Bootstrap** - Framework CSS para design responsivo

## 🚀 Como Executar

### Pré-requisitos
- .NET 8 SDK
- Docker (opcional)
- SQL Server (local ou container)
- Conta na [OpenWeatherMap](https://openweathermap.org/) para API key

### Execução Local
```bash
# Clone o repositório
git clone https://github.com/Lucassilva1010/ClimaTempoAPI.git

# Navegue até o diretório
cd ClimaTempoAPI

# Configure a API key e string de conexão
dotnet user-secrets set "OpenWeather:ApiKey" "SUA_API_KEY" --project ClimaTempo.API
dotnet user-secrets set "ConnectionStrings:ClimaDb" "Sua_String_Conexao_SQL" --project ClimaTempo.API

# Execute a aplicação
dotnet run --project ClimaTempo.API
```

### Execução com Docker
```bash
# Build da imagem
docker build -t climatempoapi .

# Executar container
docker run -p 8080:80 \
  -e OpenWeather__ApiKey=SUA_API_KEY \
  -e ConnectionStrings__ClimaDb="Sua_String_Conexao_SQL" \
  climatempoapi
```

## 📁 Estrutura do Projeto

```
ClimaTempoAPI/
├── ClimaTempo.API/           # API principal (Backend)
├── ClimaTempo.App/           # Aplicação Blazor WebAssembly (Frontend)
├── ClimaTempo.Core/          # Entidades e interfaces do domínio
├── ClimaTempo.Infra/         # Implementações de infraestrutura
├── ClimaTempo.Tests/         # Projeto de testes unitários
└── Dockerfile                # Configuração para containerização
```

## 🌐 API Endpoints

### `GET /api/clima/atual?cidade={cidade}`
Consulta as condições climáticas atuais de uma cidade específica sem salvar no banco de dados.

**Parâmetros:**
- `cidade` (obrigatório): Nome da cidade para consulta

**Resposta:**
```json
{
  "cidade": "São Paulo",
  "temperatura": 25.7,
  "descricao": "Nublado",
  "umidade": 65,
  "velocidadeVento": 3.5,
  "dataConsulta": "2024-01-15T10:30:00Z"
}
```

### `POST /api/clima/salvar?cidade={cidade}`
Consulta as condições climáticas atuais e salva o registro no banco de dados.

**Parâmetros:**
- `cidade` (obrigatório): Nome da cidade para consulta e armazenamento

**Resposta:**
- Status 201 Created com os dados do clima salvos
- Header Location com URL para consulta do histórico

### `GET /api/clima/historico`
Retorna todo o histórico de consultas climáticas salvas, ordenado por data de criação (mais recente primeiro).

**Resposta:**
```json
[
  {
    "id": 1,
    "cidade": "Rio de Janeiro",
    "temperatura": 28.3,
    "descricao": "Ensolarado",
    "umidade": 70,
    "velocidadeVento": 2.8,
    "criadoEmUtc": "2024-01-15T09:15:00Z"
  },
  {
    "id": 2,
    "cidade": "São Paulo",
    "temperatura": 25.7,
    "descricao": "Nublado",
    "umidade": 65,
    "velocidadeVento": 3.5,
    "criadoEmUtc": "2024-01-15T10:30:00Z"
  }
]
```

## 🧪 Testes

```bash
# Executar testes unitários
dotnet test

# Executar testes com cobertura
dotnet test --collect:"XPlat Code Coverage"
```
```

## 🤝 Contribuição

Contribuições são sempre bem-vindas! Por favor, leia o [guia de contribuição](CONTRIBUTING.md) antes de enviar um pull request.

## 📄 Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

Link do Projeto: [https://github.com/Lucassilva1010/ClimaTempoAPI](https://github.com/Lucassilva1010/ClimaTempoAPI)

---

⭐️ Dê uma estrela no repositório se este projeto te ajudou!
