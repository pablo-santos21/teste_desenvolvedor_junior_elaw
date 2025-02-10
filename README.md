# Desafio Técnico: Desenvolver um webcrawler

## Descrição:

Este projeto foi desenvolvido para realização do desafio técnico para a vaga de Desenvolvedor C# Júnior. O objetivo principal é construir um webcrawler que recupere algumas informações do site [Proxyservers](https://proxyservers.pro/proxy/list/order/updated/order_dir/desc/) e salve em um arquivo json e no banco de dados.

## Funcionalidades:

- **Raspagem de dados**: Busca pelos dados solicitados, no desafio, em todas as páginas.
- **Contagem de página**: Realiza a contagem de páginas que estão sendo mostradas no momento da raspagem.
- **Contagem de tempo**: Será salvo, no banco de dados, o momento que iniciou a raspagem e o momento de término.
- **Contagem de linhas**: Mostra quantas linhas foram afetadas pela raspagem.
- **Print das páginas utilizadas**: As páginas que foram utilizadas para a realização dessa raspagem, será salva na pasta definida.
- **Criação de pastas**: Ao rodar o programa e fazer a raspagem de dados, será criado duas pastas:
  1. **JsonDirectory** - onde será salvos os Json com as informações: **Ip, Port, Country, Protocol**, no formato:
     - proxies-{timestamp}.json
  2. **HtmlDirectory** - onde será salvos os html com as informações das páginas específicas, no formato:
     - page-{num-page}.html
- **multithread**: O sistema roda com multithread com no máximo 3 threads simultâneas.

## Instruções de Uso:

- Clone o repositório:

```bash
git clone https://github.com/pablo-santos21/teste_desenvolvedor_junior_elaw
```

- Entre no diretório:

```bash
cd teste_desenvolvedor_junior_elaw
```

- Rode o programa:

```bash
dotnet run
```

- A aplicação será iniciada e todos os arquivos serão gerados em seus devidos diretórios.

## Desafios Enfrentados:

Os maiores desafios encontrados nesse projeto foram:

1. Entender melhor o que é um web crawler, como realiza-lo com C#.
2. entender e usar o HtmlAgilityPack e Selenium.
3. Estudar formas de usar o Selenium sem que abrisse uma página web a cada requisição.
4. Pegar a informação de Port, pois ela não vem um dado pronto para uso, ao usar o HtmlAgilityPack.
5. Aprender a configurar o multithread.

## Fontes de Pesquisa:

1. [Html Agility Pack](https://html-agility-pack.net/)
2. [Learn Microsoft - Multi-thread](https://learn.microsoft.com/pt-br/dotnet/api/system.threading.thread?view=net-8.0)
3. [Medium - Simple web site crawler using .NET Core and C#](https://medium.com/@saurabh.dasgupta1/simple-web-site-crawler-using-net-core-and-c-5c31021922bb)
4. [Medium - Multi-threading em C#](https://marcionizzola.medium.com/multi-threading-em-c-programa%C3%A7%C3%A3o-concorrente-3d872ce4c8ae)
5. [Mysql Connector](https://mysqlconnector.net/)
6. [Selenium](https://www.selenium.dev/selenium/docs/api/dotnet/webdriver/OpenQA.Selenium.html)
7. [Zenrows - Web Crawler](https://www.zenrows.com/blog/web-scraping-c-sharp#csharp-scraping-libraries)

## Autor:

**Pablo Santos**  
**Email:** pablovieira.san@gmail.com

## Agradecimentos:

Agradeço à equipe da **Elaw** pela oportunidade de participar deste desafio, foi muito gratificante e desafiador.
