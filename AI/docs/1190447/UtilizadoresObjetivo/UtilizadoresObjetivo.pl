% Base de Conhecimento

no(1,ana,[natureza,pintura,musica,sw,porto]).
no(11,antonio,[natureza,pintura,carros,futebol,lisboa]).
no(12,beatriz,[natureza,musica,carros,porto,moda]).
no(13,carlos,[natureza,musica,sw,futebol,coimbra]).
no(14,daniel,[natureza,cinema,jogos,sw,moda]).
no(21,eduardo,[natureza,cinema,teatro,carros,coimbra]).
no(22,isabel,[natureza,musica,porto,lisboa,cinema]).
no(23,jose,[natureza,pintura,sw,musica,carros,lisboa]).
no(24,luisa,[natureza,cinema,jogos,moda,porto]).
no(31,maria,[natureza,pintura,musica,moda,porto]).
no(32,anabela,[natureza,cinema,musica,tecnologia,porto]).
no(33,andre,[natureza,carros,futebol,coimbra]).
no(34,catia,[natureza,musica,cinema,lisboa,moda]).
no(41,cesar,[natureza,teatro,tecnologia,futebol,porto]).
no(42,diogo,[natureza,futebol,sw,jogos,porto]).
no(43,ernesto,[natureza,teatro,carros,porto]).
no(44,isaura,[natureza,moda,tecnologia,cinema]).
no(200,sara,[natureza,moda,musica,sw,coimbra]).
no(51,rodolfo,[natureza,musica,sw]).
no(61,rita,[moda,tecnologia,cinema]).


ligacao(1,11,10,8).
ligacao(1,12,2,6).
ligacao(1,13,-3,-2).
ligacao(1,14,1,-5).
ligacao(11,21,5,7).
ligacao(11,22,2,-4).
ligacao(11,23,-2,8).
ligacao(11,24,6,0).
ligacao(12,21,4,9).
ligacao(12,22,-3,-8).
ligacao(12,23,2,4).
ligacao(12,24,-2,4).
ligacao(13,21,3,2).
ligacao(13,22,0,-3).
ligacao(13,23,5,9).
ligacao(13,24,-2, 4).
ligacao(14,21,2,6).
ligacao(14,22,6,-3).
ligacao(14,23,7,0).
ligacao(14,24,2,2).
ligacao(21,31,2,1).
ligacao(21,32,-2,3).
ligacao(21,33,3,5).
ligacao(21,34,4,2).
ligacao(22,31,5,-4).
ligacao(22,32,-1,6).
ligacao(22,33,2,1).
ligacao(22,34,2,3).
ligacao(23,31,4,-3).
ligacao(23,32,3,5).
ligacao(23,33,4,1).
ligacao(23,34,-2,-3).
ligacao(24,31,1,-5).
ligacao(24,32,1,0).
ligacao(24,33,3,-1).
ligacao(24,34,-1,5).
ligacao(31,41,2,4).
ligacao(31,42,6,3).
ligacao(31,43,2,1).
ligacao(31,44,2,1).
ligacao(32,41,2,3).
ligacao(32,42,-1,0).
ligacao(32,43,0,1).
ligacao(32,44,1,2).
ligacao(33,41,4,-1).
ligacao(33,42,-1,3).
ligacao(33,43,7,2).
ligacao(33,44,5,-3).
ligacao(34,41,3,2).
ligacao(34,42,1,-1).
ligacao(34,43,2,4).
ligacao(34,44,1,-2).
ligacao(41,200,2,0).
ligacao(42,200,7,-2).
ligacao(43,200,-2,4).
ligacao(44,200,-1,-3).
ligacao(1,51,6,2).
ligacao(51,61,7,3).
ligacao(61,200,2,4).

:- dynamic no/3.

criarNo():-asserta(no(1,ola,[js,ts])),asserta(no(2,xau,[ts,js])).


sinonimo(t4d,[ts,typescript]).
sinonimo(t3c,[csharp]).
sinonimo(t1a,[javascript,js]).
sinonimo(t2b,[cplusplus]).


m1():-forall(sinonimo(Palavra,Lista),percorrer(Palavra,Lista)).


percorrer(Palavra,Lista):-forall(no(Id,Nome,ListaUser),percorrerUser(Lista,ListaUser,Id,Nome,Palavra,ListaUser)).


percorrerUser(_,[],_,_,_,_):-!.
percorrerUser(Lista,[X|L],Id,Nome,Palavra,ListaUser):-member(X,Lista),delete(ListaUser,X,L1),L2=[Palavra],append(L1,L2,L3)
,retract(no(Id,Nome,_)),asserta(no(Id,Nome,L3));percorrerUser(Lista,L,Id,Nome,Palavra,ListaUser).


todas_combinacoes1(X,[],[],ID,ListaUtilizadores):-no(ID,_,LTags),todas_combinacoes(X,LTags,LcombXTags),utilizadorTagsComuns(LcombXTags,[],ListaUtilizadores,ID).

todas_combinacoes(X,LTags,LcombXTags):-findall(L,combinacao(X,LTags,L),LcombXTags).
combinacao(0,_,[]):-!.
combinacao(X,[Tag|L],[Tag|T]):-X1 is X-1, combinacao(X1,L,T).
combinacao(X,[_|L],T):- combinacao(X,L,T).

utilizadorTagsComuns([],ListaUtilizadores,L6,_):-!,append([],ListaUtilizadores,L6).
utilizadorTagsComuns([X|L],ListaUtilizadores,L6,ID):-(no(IdUtilizador,_,ListaTags),IdUtilizador\=ID,not(ligacao(ID,IdUtilizador,_,_)),not(ligacao(IdUtilizador,ID,_,_)),verificaLista(X,ListaTags),not( member(IdUtilizador,ListaUtilizadores)),List=[IdUtilizador],append(ListaUtilizadores,List,L7),utilizadorTagsComuns([X|L],L7,L6,ID));(utilizadorTagsComuns(L,ListaUtilizadores,L6,ID)).

verificaLista([],_):-true.
verificaLista([X|L],L2):- member(X,L2),verificaLista(L,L2).
verificaLista(_,_):-false.

