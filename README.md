# Good Hamburger Project

Bem-vindo ao Projeto Good Hamburger! 

Neste arquivo README, você encontrará informações úteis sobre o funcionamento do projeto.

## Índice

- [Sobre](#sobre)
- [Tecnologias e Frameworks](#tecnologias-e-frameworks)
- [Em funcionamento](#em-funcionamento)
- [Conclusão](#conclusão)

## Sobre

Este é um projeto de desafio tecnico simples com o objetivo de criar uma API para gerenciar os pedidos de uma lanchonete ficticia.

Requisitos:
• Construção de uma API REST em C# com .NET / ASP.NET Core.
• Implementação do CRUD completo: criar, listar, consultar por identificador, atualizar e remover.
• Implementar calculo de desconto, subtotal e total final de cada pedido.
• Validar erros e retornar respostas claras (ex.: itens duplicados, pedido inválido, recurso não encontrado).

Regras de desconto:
• Sanduíche + batata + refrigerante aplicar 20% de desconto.
• Sanduíche + refrigerante aplicar 15% de desconto.
• Sanduíche + batata aplicar 10% de desconto.
• Cada pedido pode conter apenas um sanduíche, uma batata e um refrigerante. 
• Itens duplicados devem retornar uma mensagem de erro clara.

Diferenciais (opcionais):
• Frontend em Blazor consumindo a API.
• Testes automatizados das regras de negócio.

## Tecnologias e Frameworks 

Neste projeto, foi utilizado as seguintes tecnologias:

- C# 
- .NET 9
- ASP .NET
- xUnity
- EntityFrameworkCore
- Swashbuckle (Swagger)

## Em funcionamento

1. Clone este repositório: `git clone https://github.com/M-LaScala/Good-Hamburger-Project`
2. Navegue até o diretório do projeto e abra o arquivo .SLN com o visual studio 2022+
3. Instale os pacote NuGet dependentes

![](./Assets/NuGet.png)

Ao executar a aplicação, se nenhum erro ocorrer na construção da  API, você será direcionado á tela do Swagger. Agora sinta-se à vontade para explorar as funcionalidades disponíveis.

Cenario de exemplo metodo post:
```json
{
  "nome": "Lucas",
  "itensIds": [
    1, 4, 5
  ]
}
```
Retorno esperado:
```json
{
  "id": 1,
  "nome": "Lucas",
  "itens": [
    {
      "id": 1,
      "nome": "X-Burguer",
      "preco": 5,
      "tipo": 0
    },
    {
      "id": 4,
      "nome": "Batata Frita",
      "preco": 2,
      "tipo": 1
    },
    {
      "id": 5,
      "nome": "Refrigerante",
      "preco": 2.5,
      "tipo": 1
    }
  ],
  "subtotal": 9.5,
  "desconto": 1.9,
  "total": 7.6,
  "dataPedido": "2026-04-21T16:43:37.4234503-03:00"
}
```

![](./Assets/Swagger.png)

Caso queria realizar os testes de regra de negocio é possivel executar os mesmos diretamente do visual studio.

![](./Assets/Testes.png)

## Conclusão

Este projeto teve início no ano de 2026, com o propósito de ser um desafio tecnico simples para a criação de uma APIs com ASP.NET. 
O objetivo principal é atingir os requisitos do desafio e como reslutado expor uma aplicação funcional.
O projeto levou cerca de 3 a 5 horas para ser desenvolvido desconsiderando intervalos.
