# Time Manager - Backend API

API RESTful desenvolvida em **C# e .NET** para o sistema **Time Manager**. Este serviço é o núcleo central do sistema, responsável por processar regras de negócio, gerenciar o banco de horas, registrar o ponto eletrônico e garantir a segurança dos dados.

O projeto foi construído sob os princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**, garantindo que as regras da aplicação sejam testáveis e independentes de frameworks de infraestrutura.

## 🚀 Tecnologias Utilizadas

* **Framework:** .NET (ASP.NET Core Web API)
* **Linguagem:** C#
* **ORM:** Entity Framework Core
* **Banco de Dados:** PostgreSQL (ou SQL Server)
* **Segurança:** Autenticação via JWT (JSON Web Tokens)
* **Padrões:** SOLID, Repository Pattern, Dependency Injection

---

## 🏛️ Arquitetura e Princípios SOLID

A estrutura do código separa estritamente a intenção do negócio da tecnologia de entrega:

* **Domain (Domínio):** O coração do software. Contém as entidades (`TimeRecord`, `WorkJourneyRule`), regras de cálculo de horas e interfaces. Não possui nenhuma dependência externa (Princípio da Inversão de Dependência - DIP).
* **Application (Aplicação):** Contém os Casos de Uso (Use Cases) e Serviços. Orquestra as regras do domínio, validando os fluxos (ex: impedir cálculo de resumo se o usuário não tiver uma jornada configurada).
* **Infrastructure (Infraestrutura):** Onde o código interage com o mundo externo. Contém os Repositórios do Entity Framework, geração de tokens JWT e acesso ao banco de dados.
* **API (Presentation):** Os Controllers (`Auth`, `TimePunch`, `Summary`). Atuam apenas como portas de entrada HTTP. Eles recebem o JSON, chamam a camada de aplicação e devolvem a resposta (Princípio da Responsabilidade Única - SRP).

---

## ⚙️ Como Executar o Projeto Localmente

### Pré-requisitos
* **.NET SDK** (versão 8.0 ou superior)
* Um servidor de banco de dados rodando (ex: PostgreSQL local ou em um container isolado).

### Passo a Passo

1. **Clone o repositório e acesse a pasta do projeto.**

2. **Configure a String de Conexão:**
   Abra o arquivo `appsettings.Development.json` (ou `appsettings.json`) e configure os dados de acesso ao seu banco de dados na sessão `ConnectionStrings`.
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Database=TimeManagerDb;Username=seu_usuario;Password=sua_senha"
   }

```

3. **Crie o Banco de Dados (Migrations):**
No terminal, execute o comando do Entity Framework para criar as tabelas no banco baseado no seu código:
```bash
dotnet ef database update

```


4. **Inicie a Aplicação:**
```bash
dotnet run

```


5. **Acesso e Documentação:**
A API estará rodando em `http://localhost:5000` (ou na porta configurada no seu `launchSettings.json`).
Se o Swagger estiver ativado no ambiente de desenvolvimento, você pode testar os endpoints acessando `http://localhost:5000/swagger`.
