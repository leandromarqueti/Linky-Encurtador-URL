# Linky - Encurtador de URL

## Sobre o projeto

O Linky é uma aplicação full-stack de encurtamento de URLs focada em usabilidade e design moderno. O projeto permite que o usuário insira uma URL longa e defina um tempo de expiração para o link gerado (Ex: 1 dia, 3 dias, 7 dias ou permanente). Utiliza um back-end robusto em C# que se comunica com um banco de dados local SQLite e um front-end dinâmico construído em React com Vite.

## Arquitetura

Este projeto se destaca por utilizar padrões profissionais de mercado:
- **Arquitetura em Camadas (Layered Architecture):** O back-end em C# foi desenhado separando claramente as responsabilidades nas camadas de 'Api', 'Application', 'Domain' e 'Infrastructure'. Isso garante um código mais manutenível, escalável e de fácil entendimento.
- **Separação Client-Server (Front-end independente):** Diferente do MVC tradicional onde o servidor gera as telas, o front-end em React atua como uma aplicação independente (SPA). Ele se comunica apenas via JSON com a API RESTful, garantindo total desacoplamento entre a interface e as regras de negócio.

## Tecnologias

- **Front-end:** React, Vite, TypeScript, TailwindCSS / CSS puro
- **Back-end:** C#, .NET 9, ASP.NET Core API
- **Banco de Dados:** SQLite (Entity Framework Core)

## Objetivos de aprendizagem

- Praticar a estruturação de um projeto Full-Stack (C# + React).
- Implementação de APIs RESTful usando o padrão de Injeção de Dependências e Arquitetura em Camadas.
- Configuração e uso do Entity Framework Core com provedor SQLite para testes em ambiente de desenvolvimento.
- Conexão e consumo de APIs C# a partir de uma aplicação React utilizando Axios.

## Entrada/Saída
- Entrada: "https://superlongurl.com/abc123"
- Saída: "linky.com/abc123"
  
## Como executar

1. **Back-end:**
   - Abra o terminal e navegue até a pasta da API: 'cd Linky/Linky.Api'
   - Execute o comando: 'dotnet run'
   - *Nota:* O banco de dados SQLite ('linky.db') será criado e as migrações aplicadas automaticamente durante a inicialização.

2. **Front-end:**
   - Em outro terminal, navegue até a pasta do cliente web: 'cd linky-web'
   - Instale as dependências (se for a primeira vez): 'npm install'
   - Inicie o servidor de desenvolvimento: 'npm run dev'
   - Abra o link local exibido no terminal (geralmente 'http://localhost:5173/') no seu navegador.

## Estrutura atual

.
├── Linky (Back-end em C# / .NET 9)
├── linky-web (Front-end em React + Vite)
├── .gitignore
└── README.md
