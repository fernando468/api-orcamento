# Backend - API de Clientes e Orçamentos

API desenvolvida em ASP.NET Core para gerenciamento de clientes e orçamentos, utilizando PostgreSQL, C# e .NET.

## Tecnologias e ferramentas utilizadas

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- Npgsql (driver do PostgreSQL para .NET)
- PostgreSQL 16
- Docker / Docker Compose
- OpenAPI
- C#

## Requisitos

Antes de rodar o projeto, certifique-se de ter instalado:

- .NET SDK 10
- Docker Desktop ou Docker Engine
- Git
- Um cliente HTTP opcional para testar a API (Postman ou Insomnia)

## Configuração do banco de dados

O projeto usa PostgreSQL com a seguinte connection string padrão em [Backend/appsettings.json](Backend/appsettings.json):

```json
"ConnectionStrings": {
  "LocalConnection": "Host=localhost;Port=5432;Database=orcamentos;Username=app;Password=app"
}
```

## Como rodar o projeto

OBS: Obrigátorio o Docker e o .NET SDK 10 instalados.

### 1) Suba o PostgreSQL com Docker

No diretório raiz do projeto, execute:

```bash
docker compose up -d
```

Isso sobe um container PostgreSQL na porta 5432 com os seguintes dados:

- Banco: `orcamentos`
- Usuário: `app`
- Senha: `app`

### 2) Restore dos pacotes do projeto

```bash
dotnet restore
```

### 3) Aplicar as migrações do banco

```bash
dotnet ef database update
```

> Caso o comando `dotnet ef` não esteja disponível, instale a ferramenta global com:
>
> ```bash
> dotnet tool restore
> ```

### 4) Executar a API

```bash
dotnet run
```

A API ficará disponível em:

- HTTP: `https://localhost:7072` ou `http://localhost:5058`

O ambiente de desenvolvimento usa HTTPS por padrão e a aplicação também registra endpoints OpenAPI.
