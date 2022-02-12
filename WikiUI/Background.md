## Contents
- [Architecture Background](#architecture-background)
	- [Problem Background](#problem-background)
		- [System Overview](#system-overview)
		- [Context](#context)
		- [Driving Requirements](#driving-requirements)
			- [Functional requirements](#functional-requirements)
			- [Quality attributes](#quality-attributes)
				- [Funcionalidade](#funcionalidade)
				- [Usabilidade](#usabilidade)
				- [Confiabilidade (Reliability)](#confiabilidade-reliability)
				- [Desempenho (Performance)](#desempenho-performance)
				- [Suportabilidade](#suportabilidade)
				- [Design constraints](#design-constraints)
				- [Implementation constraints](#implementation-constraints)
				- [Interface constraints](#interface-constraints)
				- [Physical constraints](#physical-constraints)
	- [Solution Background](#solution-background)
		- [Architectural Approaches](#architectural-approaches)
		- [Analysis Results](#analysis-results)
		- [Mapping Requirements to Architecture](#mapping-requirements-to-architecture)

# Architecture Background

## Problem Background

### System Overview

A Graphs4Social, S.A. é uma startup com sede no Porto (Portugal) cuja missão é fornecer aplicações de manipulação e visualização de grafos de redes sociais.
A empresa decidiu recentemente expandir o seu portfolio de produtos entrando na área de jogos, mas mantendo o foco nos grafos de redes sociais.
A empresa decidiu recorrer à subcontratação de serviços de desenvolvimento uma vez que não possui capacidade livre de momento.

---

### Context

Pretende-se o desenvolvimento de um protótipo para um jogo baseado na visualização e manipulação de grafos sociais.

O protótipo inicial deve ser constituído pelos seguintes módulos:
* Jogo 3D com componentes de inteligência artificial;
* Gestão de rede social e dados mestres;
* Leaderboard e consultas.

O Jogo simula uma rede social e o jogador tem por objetivo expandir a sua rede social, com o objetivo último de ter a maior e mais forte rede social possível. O jogo desenrola-se numa série de missões em que o jogador terá que avançar e aumentar a sua rede para subir no leader board.
Um utilizador pode iniciar um jogo/missão em qualquer momento escolhendo o nível de dificuldade pretendido.

---

### Driving Requirements

#### Functional requirements
1. Como utilizador, pretendo escolher quais os "utilizadores objetivos".
2. Como utilizador, pretendo pedir introdução a um outro utilizador.
3. Como utilizador, pretendo aprovar ou desaprovar pedido de introdução.
4. Como utilizador, pretendo editar os meus relacionamentos a nível das tags e força de ligação.
5. Como utilizador, pretendo editar o meu perfil.
6. Como utilizador, pretendo editar o meu estado de humor.
7. Como utilizador, pretendo consultar a rede de ligações na minha perspetiva.
8. Como utilizador não registado, pretendo registar-me como utilizador no sistema.
9. Como utilizador, pretendo pesquisar por outros utilizadores que conheço na rede e pedir-lhes ligação.
10. Como utilizador, pretendo ver os pedidos de ligação que tenho pendente.
11. Como utilizador, quero aceitar ou rejeitar um pedido de introdução.

### Tasks

12. Planear o esqueleto da aplicação. Identificar que módulos e interfaces (detalhadas) de cada módulo. Identificar diferenças de terminologia, se existirem, entre o vários dominios.
13. Setup dos projetos e repositórios Git (Bitbucket).
14. Design arquitetural: o Nível 1: vista lógica e de cenários,
	Nível 2: vista lógica, de processo, de implementação e física,
	Nível 3 (Master Data Rede): vista lógica, de processo e de implementação,
	Adoção de estilos/padrões: cliente-servidor, SOA, DDD, Onion (inclui DI, IoC), DTO.
15. Tecnologia: .Net C#, SGBD relacional (e.g. MS SQL Server), ORM (e.g. Entity Framework).
16. Testes unitários e de integração (com isolamento via mocks).
17. Implantação na cloud (e.g. Heroku, MongoDB Atlas).
18. Pipelines (Bitbucket Pipelines).

---

#### Quality attributes
Os atributos de qualidade são categorizados e sistematizados segundo o modelo FURPS+.

FURPS é um acrónimo que representa um modelo para classificação de atributos de qualidade de software (requisitos funcionais e não-funcionais) distribuído da seguinte forma:

##### Funcionalidade
1. Um utilizador deve conseguir registar-se com as suas credenciais e aceder à sua conta.
2. Deve ser auditada e verificada a integridade da informação a que os sistemas acedem.
3. Com vista à necessidade de saber e necessidade de conhecer, toda a informação deve estar protegida de acessos indevidos. Ou seja, o princípio de minimização de acesso ao que é essencial para cada utilizador/aplicação, criação de túneis para transferência de informação, avaliação da integridade de dados e aplicações, e encriptação/minimização dos dados.

##### Usabilidade
4. A SPA deve permitir acesso a todos os módulos do sistema: master data, planeamento e visualização, bem como RGPD.

5. No âmbito do projeto atual, a administração de utilizadores pode ser efetuada diretamente na base de dados não sendo necessário um módulo de gestão de utilizadores.

##### Confiabilidade (Reliability)

6. A base de dados está normalizada segundo as normas. Assim, a duplicidade de dados é mínimia e a confiabilidade da base de dados é elevada.

7. Os protocolos http já estão implementados na framework do .NET pelo que só têm de ser personalizados

##### Desempenho (Performance)


##### Suportabilidade


##### Design constraints
9. O sistema deve ser composto por uma aplicação web do tipo Single Page Application (SPA) que permite aos utilizadores autorizados acederem aos diferentes módulos da aplicação, bem como por um conjunto de serviços que implementem as componentes de regras de negócio necessárias para o funcionamento da aplicação web.

De um modo geral, as principais funcionalidades de cada módulo são as seguintes:

- Master data – permite a gestão da informação relacionada com a rede (nós, percursos), tipos de viaturas, tipos de tripulantes, linhas e viagens.
- Planeamento – com base nos percursos existentes planear as trocas de tripulações nos pontos de rendição. Planear os serviços de tripulantes com base nos serviços de viatura. Consome a informação gerida no módulo master data e publica informação do planeamento para o módulo de visualização.
- Visualizador 3D –  permite a visualização 2D e 3D da rede, a navegação pela cena e a consulta gráfica de informação sobre as viagens. Consome a informação gerida no módulo master data e no módulo
- UI – interface com o utilizador
- Clientes + RGPD – gestão de informação dos utilizadores finais “clientes” e seus consentimentos no âmbito do RGPD

10.  No âmbito do projeto atual, a administração de utilizadores pode ser efetuada diretamente na base de dados não sendo necessário um módulo de gestão de utilizadores.

##### Implementation constraints
11.   Todos os módulos devem fazer parte do código fonte da mesma SPA e serem disponibilizados como um único artefacto.

##### Interface constraints
12.  A SPA deve permitir acesso a todos os módulos do sistema: master data, planeamento e visualização, bem como RGPD.
13.  O módulo de Planeamento deve consumir dados de rede através da API do master data
14.  O módulo de Visualização deve consumir dados de rede através da API do master data
15.  O módulo de Visualização deve consumir dados de serviços de tripulante através da API do planeamento

##### Physical constraints
16. Existem dois servidores em load balancing, onde estão instaladas as aplicações, serviços e as bases de dados e que se encarregam do armazenamento da informação.

17. Existem ainda dois servidores em failover que distribuem os endereços a todos os sistemas e se encarregam da autenticação de sistemas e utilizadores (DHCP, DNS (se aplicável) e autenticação de servidores, e eventualmente um servidor Kerberos).
18. Algumas das aplicações devem ser implantadas *on premises* e outras em IaaS e PaaS (*on cloud*). Cf. requisitos específicos das UC por sprint.

---

## Solution Background

### Architectural Approaches

Baseado nos requisitos não funcionais e restrições de design, serão adotadas as seguintes abordagens/padrões/estilos:

- Client-Server, porque cada um dos "módulos" MDR, MDV, Planeamento são aplicações servidoras de outras aplicações clientes (e.g. MDR é servidor de MDV e UI, MDV é servidor de Planeamento e UI, e Planeamento é servidor de UI);
- Web Application, em que o frontend é desempenhado por uma SPA (Single Page Application), e que o backend é desempenhado pelos módulos MDR, MDV e Planeamento;
- SOA, porque os servidores (cf. anterior) deverão disponibilizar API, e particularmemte API para serem usadas na web, disponibilizados serviços para os clientes respetivos. Serão adotados os nível 0, 1 e 2 do [Modelo de Maturidade de Richardson](https://martinfowler.com/articles/richardsonMaturityModel.html) aplicado a REST;
- Layered architecture, mais especificamente Onion Architecture, por razões académicas.

Outras abordagens/estilos/padrões, como e.g. interligação entre aplicações baseado em mensagens-eventos foram desconsideradas para não violar os requisitos e restrições definidos, mas também por questões académicas.

---

### Analysis Results

Não existem por agora resultados de análise ou avaliação. Estudos qualitativos acerca dos estilos/padrões adotados (nomeadamente Onion em MDR e MDV, mas também Dependency Injection na UI), permitem empiricamente advogar que a manutenibilidade, evolutabilidade e testabilidade do software são elevadas, ao mesmo tempo que permitem atingir as funcionalidades desejadas.

---

### Mapping Requirements to Architecture

[ListaFuncionalidades](Planeamento.md)
