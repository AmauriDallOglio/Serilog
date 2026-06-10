# Serilog API — Logging e Observabilidade no Azure

Projeto demonstrativo desenvolvido em **ASP.NET Core** com foco na implementação de **logging estruturado utilizando Serilog**, integração com **Azure App Service** e monitoramento via **Application Insights**, com diferentes níveis de severidade (Information, Warning, Error).

<img width="1869" height="799" alt="image" src="https://github.com/user-attachments/assets/a6b07f05-c56d-4960-aade-9883c53b83d1" />

<img width="1896" height="948" alt="image" src="https://github.com/user-attachments/assets/13b5d3fc-e755-4ade-ba51-e4f769ce5e63" />



─── Opções válidas para "Nivel" ───────────────────────────────────────────

"Detalhado"   → Trace + Debug + Information + Warning + Error  (equivale a "Detalhado" no Azure App Service Logs)
<img width="1017" height="434" alt="image" src="https://github.com/user-attachments/assets/76b60180-7b21-4c5e-84b5-966b76a97ee5" />

"Informacoes" → Information + Warning + Error  (equivale a "Informações" no Azure App Service Logs)
<img width="1021" height="246" alt="image" src="https://github.com/user-attachments/assets/246c0bea-92a2-411f-bf6b-9dfdd17c76e1" />


"Aviso"       → Warning + Error  (equivale a "Aviso" no Azure App Service Logs)
<img width="1062" height="545" alt="image" src="https://github.com/user-attachments/assets/251c9f6c-72af-4831-bd0f-55379b2dd397" />

 "Erro"        → Error apenas             (equivale a "Erro" no Azure App Service Logs)
<img width="1064" height="340" alt="image" src="https://github.com/user-attachments/assets/a7347976-6ab0-4b82-ab0a-543faf741616" />


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
