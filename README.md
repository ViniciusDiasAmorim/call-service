# call-service-flow
🏗️ Tecnologias
✅ Back-end: ASP.NET Core + Entity Framework Core + SQL Server
✅ Front-end: Angular + TypeScript
✅ Autenticação & Segurança: Identity + JWT
✅ Infraestrutura: Docker + Azure App Service
✅ Mensageria: Redis (para filas de e-mails)
✅ Controle de Versionamento: GitHub
✅ Testes Automatizados: xUnit + Moq
✅ Documentação: Swagger (OpenAPI)

🛠️ Features do Projeto
1️⃣ Autenticação e Controle de Acesso
Implementar ASP.NET Identity para autenticação.

Hash de senhas usando Identity + bcrypt.

Perfis de usuário com RBAC (Role-Based Access Control):

Admin: Gerencia usuários, acessa qualquer chamado.

Técnico: Visualiza e responde chamados atribuídos.

Cliente: Abre chamados e acompanha o status.

Implementar JWT Authentication com refresh tokens.

Middleware para restringir acesso baseado no perfil do usuário.

2️⃣ Abertura e Gestão de Chamados
Criar chamados com os seguintes atributos:

Título

Descrição

Status (Aberto, Em Andamento, Resolvido, Cancelado)

Prioridade (Baixa, Média, Alta, Crítica)

Cliente (usuário que abriu o chamado)

Técnico responsável

Data de criação e prazo estimado de resolução

Regras de negócio:

Um técnico só pode ter X chamados ativos ao mesmo tempo (definido pelo admin).

Chamados críticos devem ser automaticamente priorizados.

Se um chamado ficar sem resposta por 48h, um e-mail deve ser enviado ao técnico.

Chamados podem ser reabertos apenas se não estiverem resolvidos há mais de 7 dias.

3️⃣ SLA (Service Level Agreement) e Regras de Tempo
Implementar um serviço background (Hosted Service) que:

Verifica chamados sem resposta por mais de 48h e dispara e-mails.

Escala automaticamente chamados atrasados para um supervisor.

Criar um middleware para log de auditoria, armazenando histórico de ações no banco.

4️⃣ Filtros e Relatórios
Relatórios para administradores com:

Tempo médio de resolução de chamados.

Número de chamados abertos por cliente/técnico.

Quantidade de chamados atrasados por técnico.

Técnicos podem filtrar chamados por:

Prioridade

Status

Cliente

5️⃣ Notificações e Automação
WebSockets (SignalR):

Técnicos recebem notificação em tempo real ao serem atribuídos a um chamado.

Mensageria com Redis:

Enviar e-mails assíncronos para clientes e técnicos.

6️⃣ Implantação e CI/CD
Criar Dockerfile para back-end e front-end.

Deploy no Azure App Service.

Criar pipeline no GitHub Actions para rodar testes antes do deploy.

🔥 Diferenciais (Extras para turbinar o desafio)
🔹 Implementar integração com WhatsApp API para notificação de chamados.
🔹 Criar API pública para integração com outros sistemas.
🔹 Dashboard interativo para administradores.