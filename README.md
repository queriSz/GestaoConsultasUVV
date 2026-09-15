# Gestão de Consultas UVV

Sistema web desenvolvido para gerenciamento de usuários e consultas, utilizando ASP.NET Core MVC, Entity Framework Core e SQL Server.

O projeto permite que o usuário crie uma conta, faça login e gerencie suas próprias consultas de forma individual e segura.

## Funcionalidades

- Cadastro de usuário
- Login e logout
- Armazenamento de senhas utilizando hash
- Cadastro de consultas
- Listagem das consultas do usuário autenticado
- Edição de consultas
- Exclusão de consultas
- Validação dos campos dos formulários
- Bloqueio de e-mails já cadastrados
- Proteção das páginas de consultas com autenticação
- Cada usuário possui acesso somente às suas próprias consultas

## Tecnologias utilizadas

- C#
- ASP.NET Core MVC
- .NET 8
- Entity Framework Core
- SQL Server
- HTML
- CSS
- Bootstrap
- Git
- GitHub

## Estrutura do projeto

O projeto foi desenvolvido utilizando o padrão MVC (Model-View-Controller), separando as responsabilidades da aplicação.

- **Models:** representam as entidades `Usuario` e `Consulta`.
- **Views:** responsáveis pela interface do sistema.
- **Controllers:** responsáveis pelas ações e regras da aplicação.
- **Data:** contém o contexto do Entity Framework Core.
- **Migrations:** contém as migrations utilizadas para criação e atualização do banco de dados.
- **wwwroot:** contém os arquivos CSS, JavaScript e imagens utilizados na interface.

## Banco de dados

O sistema utiliza SQL Server com Entity Framework Core através da abordagem Code First.

A string de conexão pode ser configurada no arquivo:

`appsettings.json`

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=GestaoConsultasUVV;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### Criando o banco de dados

Após configurar a conexão com o SQL Server, execute no terminal:

```bash
dotnet ef database update
```

No Visual Studio, através do Console do Gerenciador de Pacotes, também pode ser utilizado o comando:

```powershell
Update-Database
```

No VS Code, o comando utilizado deve ser:

```bash
dotnet ef database update
```

Caso a ferramenta do Entity Framework ainda não esteja instalada:

```bash
dotnet tool install --global dotnet-ef --version 8.0.0
```

## Como executar o projeto

### 1. Clonar o repositório

```bash
git clone https://github.com/queriSz/GestaoConsultasUVV.git
```

### 2. Entrar na pasta do projeto

```bash
cd GestaoConsultasUVV
```

### 3. Restaurar as dependências

```bash
dotnet restore
```

### 4. Verificar a configuração do banco

Confira se a string de conexão presente no arquivo `appsettings.json` corresponde à instância do SQL Server instalada no computador.

Exemplo utilizado durante o desenvolvimento:

```text
Server=localhost\SQLEXPRESS;Database=GestaoConsultasUVV;Trusted_Connection=True;TrustServerCertificate=True
```

### 5. Atualizar o banco de dados

```bash
dotnet ef database update
```

### 6. Executar o projeto

```bash
dotnet run
```

### 7. Abrir o sistema

Após executar o projeto, abra no navegador o endereço informado pelo terminal.

Durante o desenvolvimento foi utilizado, por exemplo:

```text
http://localhost:5298
```

A porta pode ser diferente dependendo do computador.

## Autenticação e segurança

O sistema utiliza autenticação por cookies.

As funcionalidades relacionadas às consultas são protegidas utilizando:

```csharp
[Authorize]
```

Somente usuários autenticados conseguem acessar as páginas de consultas.

Cada consulta também possui um `UsuarioId`, utilizado para relacionar a consulta ao usuário responsável por ela.

Dessa forma, mesmo que outro usuário tente acessar diretamente a URL de edição ou exclusão de uma consulta que não pertence à sua conta, o sistema não permite o acesso.

As senhas não são armazenadas diretamente em texto no banco de dados. Antes de serem salvas, são processadas utilizando `PasswordHasher`.

## Validações

Foram utilizadas Data Annotations nos Models para realizar validações dos campos.

Entre elas:

```csharp
[Required]
[EmailAddress]
[StringLength]
```

O cadastro também possui uma verificação que impede a utilização de um endereço de e-mail que já esteja cadastrado no sistema.

## Entidades principais

### Usuario

A entidade `Usuario` possui os seguintes campos:

- `Id`
- `Nome`
- `Email`
- `Senha`
- `DataCadastro`

### Consulta

A entidade `Consulta` possui os seguintes campos:

- `Id`
- `Especialidade`
- `DataHora`
- `Descricao`
- `UsuarioId`

Cada consulta pertence a um usuário cadastrado.

## Funcionalidades de consulta

Após realizar o login, o usuário pode:

- Visualizar suas consultas
- Cadastrar uma nova consulta
- Informar especialidade
- Informar data e hora
- Adicionar uma descrição
- Editar uma consulta existente
- Excluir uma consulta
- Sair da conta

As consultas apresentadas na tela são filtradas de acordo com o usuário autenticado.

## Interface

A interface foi desenvolvida utilizando HTML, CSS e Bootstrap.

O sistema possui páginas para:

- Página inicial
- Cadastro de usuário
- Login
- Listagem de consultas
- Nova consulta
- Edição de consulta
- Confirmação de exclusão

A interface foi desenvolvida com uma paleta em tons claros de azul, mantendo um padrão visual simples e responsivo.

## Repositório

Código-fonte do projeto:

https://github.com/queriSz/GestaoConsultasUVV

## Vídeo de demonstração

Link para o vídeo apresentando e demonstrando o funcionamento do sistema:

**https://youtu.be/PT_VCbK980A**

## Integrantes

- **Quéren Hapuque dos Santos Costa**


## Projeto acadêmico

Trabalho desenvolvido para a Universidade Vila Velha (UVV), com o objetivo de aplicar conceitos de desenvolvimento web, arquitetura MVC, persistência de dados, Entity Framework Core, autenticação, validação e segurança.