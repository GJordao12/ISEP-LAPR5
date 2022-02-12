# Determinar O conexões com base nas tags
=================================================================

# Problema

* **Descrição:Sugerir conexões com base nas tags.

# Predicados

## ***todas_combinacoes1/5***

```
todas_combinacoes(X,LTags,LJogadores):-
    findall(L,combination(X,LTags,L),LcombXTags),
    verificar_x(LcombXTags,LJogadores).
```
* **Explicação** Este predicado vai chamar o no com o ID do utilizador logado de modo a obter a sua Lista de Tags, de seguida chama para encontrar todas as soluções possíveis e finalmente, chama para verificar.

## ***verificaLista/2***

```
verificar_x(LCombs,LJogadores):-
    findall(J,(no(_,J,LJ),member(C,LCombs),intersection(LJ,C,C2),equal_set(C,C2)),LJogadores1),
    sort(LJogadores1,LJogadores).
```
* **Explicação** Este predicado vai a verificar que estão a mismo nivel

## ***utilizadorTagsComuns/4***

```
equal_set([],[]).
equal_set([X|Xs],Ys):-
    member(X,Ys),
    delete(Ys,X,Ys2),
    equal_set(Xs,Ys2).
```
* **Explicação** Este predicado vai a eliminar as personas que não estão a mismo nivel.

## ***todas_combinacoes/3*** e ***combinacao/3***
```
combination(0,_,[]):-!.
combination(X,[Tag|L],[Tag|T]):-X1 is X-1, combination(X1,L,T).
combination(X,[_|L],T):- combination(X,L,T).
```
* **Explicação:** Estes predicados encontram todas as combinações de X tags dentro da Lista de Tags do utilizador logado.



```
