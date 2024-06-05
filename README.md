# Sistema de HelpDesk

Este projeto é um sistema de HelpDesk desenvolvido em C#. O objetivo é fornecer uma interface para gerenciar tickets de suporte técnico, permitindo que os usuários registrem, acompanhem, atualizem e excluam chamados. A aplicação se conecta a um banco de dados MySQL para armazenar e recuperar informações sobre os usuários e os tickets.

# FUNCIONALIDADES:
- Login e Cadastro de Usuários:

O sistema permite que os usuários se cadastrem com informações pessoais e de contato.
Os usuários podem fazer login utilizando seu email e senha.
Gerenciamento de Chamados:

- Abrir Chamado: Permite ao usuário registrar um novo chamado com informações como prioridade, descrição, observação e data.

- Acompanhar Status da Chamada: Permite ao usuário verificar o status atual de um chamado usando o número do ticket.

- Atualizar Chamada: Permite ao usuário atualizar as informações de um chamado existente.

- Excluir Chamada: Permite ao usuário excluir um chamado utilizando o número do ticket

# Estrutura do Projeto:

- Menu: Apresenta as opções iniciais de login e cadastro para o usuário.

- Cadastro: Coleta as informações do usuário e as insere no banco de dados.

- Login: Verifica as credenciais do usuário e apresenta o menu de gerenciamento de chamados.

- Gera_Ticket: Permite ao usuário registrar um novo chamado e salva as informações no banco de dados.

- AcompanharStatusChamada: Recupera e exibe o status de um chamado específico.

- AtualizarChamada: Atualiza as informações de um chamado existente.

- ExcluirChamada: Remove um chamado do banco de dados.
