# ProvaCandidatoDotNet

## Descrição
Projeto desenvolvido em ASP.NET Core 8 MVC com Entity Framework Core, seguindo o modelo solicitado na prova técnica.  
Contém CRUD de Tags e Notícias, com vínculo N:N entre elas.  
As operações de criação e edição de notícias são realizadas de forma assíncrona via Ajax.

---

## Requisitos
- .NET SDK 8.0 instalado  
- SQL Server LocalDB

---

## Como executar o projeto

1. Abra o Prompt de Comando (CMD) na pasta do projeto, onde está o arquivo `ProvaCandidatoDotNet.csproj`.

2. Crie a primeira migration:
   dotnet ef migrations add InitialCreate

3. Atualize o banco de dados:
   dotnet ef database update
