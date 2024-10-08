# Gerenciador de Senhas

Um gerenciador de senhas simples e seguro, desenvolvido em C# utilizando .NET e MySQL, que permite aos usuários armazenar e gerenciar suas senhas de forma eficiente e segura.

## Tecnologias Utilizadas

- **C#**: Linguagem de programação utilizada para desenvolver a aplicação.
- **.NET**: Framework utilizado para construção de aplicações modernas e seguras.
- **MySQL**: Banco de dados utilizado para armazenar as informações das senhas.
- **Hashing**: Técnica de segurança utilizada para armazenar senhas de forma segura.
- **Criptografia**: Métodos de criptografia para proteger os dados sensíveis.

## Funcionalidades

- Armazenamento seguro de senhas.
- Criação e edição de entradas de senha.
- Criptografia de dados sensíveis.

## Criptografia

As credenciais são armazenadas de forma criptografada, tanto as utilizadas para realizar login quanto aquelas que você deseja salvar para não esquecer. A criptografia aplicada nas credenciais salvas utiliza apenas uma operação XOR com a senha da sua conta, que, por sua vez, é armazenada usando o algoritmo de hash SHA-256.
 
## Telas
### Login
![Print da tela de Login](/Imagens/login.png)
### Registrar
![Print da tela de Registro](/Imagens/registrar.png)
### Lista de senhas
![Print da tela para listar as senhas](/Imagens/lista.png)
### Detalhes e edição da senha
![Print da tela de detalhes da senha](/Imagens/detalhes.png)