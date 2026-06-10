# Serilog API — Logging e Observabilidade no Azure

Projeto demonstrativo desenvolvido em **ASP.NET Core** com foco na implementação de **logging estruturado utilizando Serilog**, integração com **Azure App Service** e monitoramento via **Application Insights**, com diferentes níveis de severidade (Information, Warning, Error).

<img width="1869" height="799" alt="image" src="https://github.com/user-attachments/assets/a6b07f05-c56d-4960-aade-9883c53b83d1" />

<img width="1896" height="948" alt="image" src="https://github.com/user-attachments/assets/13b5d3fc-e755-4ade-ba51-e4f769ce5e63" />



─── Opções válidas para "Nivel" ───────────────────────────────────────────

"Detalhado"   → Trace + Debug + Information + Warning + Error  (equivale a "Detalhado" no Azure App Service Logs)
<img width="1017" height="536" alt="image" src="https://github.com/user-attachments/assets/e5a119f8-a735-4ddc-9e8b-3ce32c609a11" />

"Informacoes" → Information + Warning + Error  (equivale a "Informações" no Azure App Service Logs)
<img width="1023" height="296" alt="image" src="https://github.com/user-attachments/assets/ab070810-8a86-4c82-94f9-4b02802ad1b1" />

"Aviso"       → Warning + Error  (equivale a "Aviso" no Azure App Service Logs)
<img width="1013" height="177" alt="image" src="https://github.com/user-attachments/assets/1b707f50-ac71-4648-b1ab-4647426f6a2d" />

 "Erro"        → Error apenas             (equivale a "Erro" no Azure App Service Logs)
 <img width="1009" height="183" alt="image" src="https://github.com/user-attachments/assets/c35d1f8c-30b8-4ab0-9d4e-8d950a708683" />

────────────────────────────────────────────────────────────────────────────




Demonstrar na prática como configurar e validar um fluxo completo de logs em uma aplicação Web API, incluindo:

- Registro de logs em diferentes níveis (Information, Warning, Error)
- Middleware personalizado para interceptação de requisições
- Integração com Azure App Service
- Visualização de logs em tempo real no portal Azure
- Base para monitoramento e observabilidade em ambiente cloud
- A solução está organizada em camadas
- Utiliza Injeção de Dependência nativa do .NET e segue boas práticas de organização e separação de responsabilidades.
- Deploy no **Azure App Service**;
- Execução de endpoints
- Status HTTP das requisições
- Mensagens customizadas do middleware
- Erros e alertas simulados

## Tecnologias Utilizadas

- .NET / ASP.NET Core
- Serilog
- Azure App Service
- Application Insights
- Dependency Injection (DI)
