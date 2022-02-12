# Utilizadores Objetivo
====================================================

# Problema

* **Descrição:** Obter os **utilizadores** que tenham em comum Xtags sendo X parametrizável. Deve ter em atenção que duas tags sintaticamente diferentes podem ter o mesmo significado semântico (e.g. C# e CSharp).

# Predicados para utilizador objetivo

## ***todas_combinacoes1/5***

```
todas_combinacoes1(X,LTags,LcombXTags,ID,ListaUtilizadores):-no(ID,_,LTags),todas_combinacoes(X,LTags,LcombXTags),utilizadorTagsComuns(LcombXTags,[],ListaUtilizadores,ID).

```
* **Explicação** Este predicado vai chamar o no com o ID do utilizador logado de modo a obter a sua Lista de Tags, de seguida chama o predicado todas_combinacoes/3 de modo a obter todas as combinações de tags possiveis e por fim chama o predicado utilizadorTagsComuns/4 de modo a obter todos os utilizadores com X tags em comum.



## ***todas_combinacoes/3*** e ***combinacao/3***
```
todas_combinacoes(X,LTags,LcombXTags):-findall(L,combinacao(X,LTags,L),LcombXTags).
combinacao(0,_,[]):-!.
combinacao(X,[Tag|L],[Tag|T]):-X1 is X-1, combinacao(X1,L,T).
combinacao(X,[_|L],T):- combinacao(X,L,T).

```

* **Explicação:** Estes predicados encontram todas as combinações de X tags dentro da Lista de Tags do utilizador logado.

## ***utilizadorTagsComuns/4***
```
utilizadorTagsComuns([],ListaUtilizadores,L6,_):-!,append([],ListaUtilizadores,L6).
utilizadorTagsComuns([X|L],ListaUtilizadores,L6,ID):-(no(IdUtilizador,_,ListaTags),IdUtilizador\=ID,not(ligacao(ID,IdUtilizador,_,_)),not(ligacao(IdUtilizador,ID,_,_)),verificaLista(X,ListaTags),not( member(IdUtilizador,ListaUtilizadores)),List=[IdUtilizador],append(ListaUtilizadores,List,L7),utilizadorTagsComuns([X|L],L7,L6,ID));(utilizadorTagsComuns(L,ListaUtilizadores,L6,ID)).
```

* **Explicação**: Este predicado vai receber a lista de listas de tags e vai criar uma nova lista de utilizadores tendo em conta as tags em comum entre o utilizador logado e os restantes utilizadores.Primeiro vai buscar a lista de tags dos vários utilizadores e o seu id ,se o id for diferente do id do user logado continua (o próprio user não pode ser seu utilizador objetivo),de seguida é chamado o predicado verificaLista/2,e de seguida caso este user ainda não seja membro da lista de utilizadores é adicionado.Quando a lista de lista de Tags se encontrar vazia é chamada a condição de paragem que guarda a lista de utilizadores numa variável L6.


## ***verificaLista/2***
```
verificaLista([],_):-true.
verificaLista([X|L],L2):- member(X,L2),verificaLista(L,L2).
verificaLista(_,_):-false.
```

* **Explicação**: Este predicado vai receber uma lista de tags da lista com todas as combinações e outra lista que é a lista das Tags de um utilizador.De seguida vai ver se as tags da lista de tags da lista com todas as combinações existem na lista de tags do utilizador. Caso estas existam todas quer dizer que o user pode ser adicionado à lista de users logo vai ser retornado true,caso contrário é retornado false.


# Predicados para tags sinónimas

## ***m1/0***

```
m1():-forall(sinonimo(Palavra,Lista),percorrer(Palavra,Lista)).
```

* **Explicação**: Este é o predicado base que vai percorrer todos os elementos da base de conhecimentos dos sinonimos e chama o predicado percorrer/2.

## ***percorrer/2***

```
percorrer(Palavra,Lista):-forall(no(Id,Nome,ListaUser),percorrerUser(Lista,ListaUser,Id,Nome,Palavra,ListaUser)).
```

* **Explicação**: Este é o predicado que vai percorrer os elementos da base de conhecimento dos nos e vai chamar o percorrerUser/6.

## ***percorrerUser/2***

```
percorrerUser(_,[],_,_,_,_):-!.
percorrerUser(Lista,[X|L],Id,Nome,Palavra,ListaUser):-member(X,Lista),delete(ListaUser,X,L1),L2=[Palavra],append(L1,L2,L3)
,retract(no(Id,Nome,_)),asserta(no(Id,Nome,L3));percorrerUser(Lista,L,Id,Nome,Palavra,ListaUser).
```

* **Explicação**: Este predicado vai percorrer a lista de tags do utilizador e vai ver se a tag pertence à lista de tags sinónimas,se isto acontecer será criada uma nova lista onde estão todos os elementos menos o elemento igual que vai ser substituido por um Id da base de conhecimento dos sinonimos.De seguida é feito um retract de modo a remover o no existente e um assert para colocar o no com a lista de tags atualizada.


# Ficheiro Base de Conhecimentos

* O Ficheiro da Base de Conhecimentos relativo ao **Utilizador Objetivo** é o [Utilizadores Objetivo](UtilizadoresObjetivo.pl)
