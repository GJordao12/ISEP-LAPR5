% ------------------------------------------------------------------------------------------------------------------------------------------------------------------: BASE DE CONHECIMENTO ALGAV :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

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

% -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------: PREDICADOS :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

% -------------------------------------------------------------------------------------------------------------------------------------------------------------------------: CAMINHO MAIS SEGURO :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

%:------------------------------------------------------------------------------------------------------------------------------------------------------------ Caminho Mais Seguro Só No Sentido Da Travessia ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

%:---------------------------------------------------------------------------------------------------------------------------------------------------------------------------- Unidirecional --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.
mais_seguro_uni(Orig, Dest, CaminhoMaisSeguro, M) :-
        get_time(Ti),
        (melhor_caminho_mais_seguro_uni(Orig, Dest,M) ; true),
        retract(melhor_sol_mais_segura(CaminhoMaisSeguro, Forca)),
        get_time(Tf),
        T is Tf-Ti,
        write('Tempo de geracao da solucao obtida: '), write(T), write(' segundos'), nl,
        write('Forca de ligacao da solucao obtida: '), write(Forca), nl,
        write('Caminho mais seguro obtido: '), write(CaminhoMaisSeguro), nl,!.

melhor_caminho_mais_seguro_uni(Orig, Dest,M) :-
        asserta(melhor_sol_mais_segura(_, -1000)),
        dfsMaisSeguroUni(Orig, Dest, LCaminho, Forca,M),
        atualiza_melhor_mais_seguro(LCaminho, Forca),
        fail.

atualiza_melhor_mais_seguro(LCaminho, Forca) :-
        melhor_sol_mais_segura(_, F),
        Forca > F, retract(melhor_sol_mais_segura(_,_)),
        asserta(melhor_sol_mais_segura(LCaminho, Forca)).

dfsMaisSeguroUni(Orig, Dest, Cam, Forca, M) :- dfsMaisSeguroUni2(Orig, Dest, [Orig], Cam, Forca, M).

dfsMaisSeguroUni2(Dest, Dest, LA, Cam, 0, _) :- !, reverse(LA, Cam).

dfsMaisSeguroUni2(Act, Dest, LA, Cam, Forca, M) :-
        no(NAct, Act, _),
        ligacao(NAct, NX, F1, _),
        F1 >= M,
        no(NX, X, _),
        \+ member(X, LA),
        dfsMaisSeguroUni2(X, Dest, [X|LA], Cam, FX, M),
        Forca is (FX + F1).
%:----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

%:-------------------------------------------------------------------------------------------------------------------------------------------------------------------------- Bidirecional ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.
mais_seguro_bi(Orig, Dest, CaminhoMaisSeguro, M) :-
        get_time(Ti),
        (melhor_caminho_mais_seguro_bi(Orig, Dest,M) ; true),
        retract(melhor_sol_mais_segura(CaminhoMaisSeguro, Forca)),
        get_time(Tf),
        T is Tf-Ti,
        write('Tempo de geracao da solucao obtida: '), write(T), write(' segundos'), nl,
        write('Forca de ligacao da solucao obtida: '), write(Forca), nl,
        write('Caminho mais seguro obtido: '), write(CaminhoMaisSeguro), nl,!.

melhor_caminho_mais_seguro_bi(Orig, Dest,M) :-
        asserta(melhor_sol_mais_segura(_, -1000)),
        dfsMaisSeguroBi(Orig, Dest, LCaminho, Forca,M),
        atualiza_melhor_mais_seguro(LCaminho, Forca),
        fail.

dfsMaisSeguroBi(Orig, Dest, Cam, Forca, M) :- dfsMaisSeguroBi2(Orig, Dest, [Orig], Cam, Forca, M).

dfsMaisSeguroBi2(Dest, Dest, LA, Cam, 0, _) :- !, reverse(LA, Cam).

dfsMaisSeguroBi2(Act, Dest, LA, Cam, Forca, M) :-
        no(NAct, Act, _),
        (ligacao(NAct, NX, F1, _);ligacao(NX, NAct, F1, _)),
        F1 >= M,
        no(NX, X, _),
        \+ member(X, LA),
        dfsMaisSeguroBi2(X, Dest, [X|LA], Cam, FX, M),
        Forca is (FX + F1).
%:----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

%:----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.


%:-------------------------------------------------------------------------------------------- Caminho Mais seguro considerando a soma das duas forças em cada ramo da travessia e garantindo que em nenhuma delas temos uma força abaixo de um dado limiar ------------------------------------------------------------------------------------------------------------------------------------------.

%:-------------------------------------------------------------------------------------------------------------------------------------------------------------------- Unidirecional -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.
mais_seguro_uni_2(Orig, Dest, CaminhoMaisSeguro, M) :-
        get_time(Ti),
        (melhor_caminho_mais_seguro_uni_2(Orig, Dest,M) ; true),
        retract(melhor_sol_mais_segura(CaminhoMaisSeguro, Forca)),
        get_time(Tf),
        T is Tf-Ti,
        write('Tempo de geracao da solucao obtida: '), write(T), write(' segundos'), nl,
        write('Forca de ligacao da solucao obtida: '), write(Forca), nl,
        write('Caminho mais seguro obtido: '), write(CaminhoMaisSeguro), nl,!.

melhor_caminho_mais_seguro_uni_2(Orig, Dest,M) :-
        asserta(melhor_sol_mais_segura(_, -1000)),
        dfsMaisSeguroUni_2(Orig, Dest, LCaminho, Forca,M),
        atualiza_melhor_mais_seguro(LCaminho, Forca),
        fail.

dfsMaisSeguroUni_2(Orig, Dest, Cam, Forca, M) :- dfsMaisSeguroUni2_2(Orig, Dest, [Orig], Cam, Forca, M).

dfsMaisSeguroUni2_2(Dest, Dest, LA, Cam, 0, _) :- !, reverse(LA, Cam).

dfsMaisSeguroUni2_2(Act, Dest, LA, Cam, Forca, M) :-
        no(NAct, Act, _),
        ligacao(NAct, NX, F1, F2),
        F1 >= M,
        F2 >= M,
        no(NX, X, _),
        \+ member(X, LA),
        dfsMaisSeguroUni2_2(X, Dest, [X|LA], Cam, FX, M),
        Forca is (FX + F1 + F2).
%:----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

%:----------------------------------------------------------------------------------------------------------------------------------------------------------------------- Bidirecional ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.
mais_seguro_bi_2(Orig, Dest, CaminhoMaisSeguro, M) :-
        get_time(Ti),
        (melhor_caminho_mais_seguro_bi_2(Orig, Dest,M) ; true),
        retract(melhor_sol_mais_segura(CaminhoMaisSeguro, Forca)),
        get_time(Tf),
        T is Tf-Ti,
        write('Tempo de geracao da solucao obtida: '), write(T), write(' segundos'), nl,
        write('Forca de ligacao da solucao obtida: '), write(Forca), nl,
        write('Caminho mais seguro obtido: '), write(CaminhoMaisSeguro), nl,!.

melhor_caminho_mais_seguro_bi_2(Orig, Dest,M) :-
        asserta(melhor_sol_mais_segura(_, -1000)),
        dfsMaisSeguroBi_2(Orig, Dest, LCaminho, Forca,M),
        atualiza_melhor_mais_seguro(LCaminho, Forca),
        fail.

dfsMaisSeguroBi_2(Orig, Dest, Cam, Forca, M) :- dfsMaisSeguroBi2_2(Orig, Dest, [Orig], Cam, Forca, M).

dfsMaisSeguroBi2_2(Dest, Dest, LA, Cam, 0, _) :- !, reverse(LA, Cam).

dfsMaisSeguroBi2_2(Act, Dest, LA, Cam, Forca, M) :-
        no(NAct, Act, _),
        (ligacao(NAct, NX, F1, F2);ligacao(NX,NAct, F1, F2)),
        F1 >= M,
        F2 >= M,
        no(NX, X, _),
        \+ member(X, LA),
        dfsMaisSeguroBi2_2(X, Dest, [X|LA], Cam, FX, M),
        Forca is (FX + F1 + F2).
%:----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

%:----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

% -----------------------------------------------------------------------------------------------------------------------------------------------------------------------: UTILIZADOR OBJETIVO :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

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

% -------------------------------------------------------------------------------------------------------------------------------------------------------------------: TAMANHO DA REDE DE UTILIZADORES :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

tamanho_rede_utilizadores(Origem,Nivel,Solucao):- tamanho_rede_utilizadores_nivelX([Origem],Nivel,ListaA),
                                                    flatten(ListaA,ListaB),
                                                    sort(ListaB,ListaE),
                                                    length(ListaE,Solucao).

tamanho_rede_utilizadores_nivelX(_,0,[]):-!.

tamanho_rede_utilizadores_nivelX(ListaB,Nivel,[ListaC|ListaA]):- NY is Nivel - 1,
                                                                 tamanho_rede_utilizadores_nivelY(ListaB,ListaI),
                                                                 flatten(ListaI,ListaC),
                                                                 tamanho_rede_utilizadores_nivelX(ListaC,NY,ListaA).
tamanho_rede_utilizadores_nivelY([],[]):-!.

tamanho_rede_utilizadores_nivelY([UserX|ListaB],[ListaD|ListaC]):- findall(UserY,ligacao(UserX,UserY,_,_), ListaD),
                                                                   tamanho_rede_utilizadores_nivelY(ListaB,ListaC).



% -------------------------------------------------------------------------------------------------------------------------------------------------------------------: CAMINHO MAIS FORTE :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

% Algoritmo caminho mais forte (Bidirecional)

:- dynamic melhor_sol_maisForteLig/2.
:- dynamic contagem_solucoes/1.

plan_maisForteLig(Orig, Dest, LCaminho_maisForteLig) :-
        get_time(Ti),                                                    % retorna o tempo atual como TimeStamp (inicia contagem de tempo)
        (melhor_caminho_maisForteLig(Orig, Dest) ; true),                % chamada ao metodo melhor_caminho_strongerLig, sendo que caso este dê false, como temos "; true", será sempre feito o que vem a seguir
        retract(melhor_sol_maisForteLig(LCaminho_maisForteLig, Forca)),  % remocao da melhor solucao
        retract(contagem_solucoes(NSol)),                                % remocao da contagem do numero de solucoes
        get_time(Tf),                                                    % retorna o tempo atual como TimeStamp (finaliza contagem de tempo)
        T is Tf-Ti,                                                      % determinar tempo decorrido
        write('Numero de solucoes obtidas: '), write(NSol), nl,
        write('Tempo de geracao da solucao obtida: '), write(T), write(' segundos'), nl,
        write('Forca de ligacao da solucao obtida: '), write(Forca), nl,
        write('Caminho mais forte obtido:'), write(LCaminho_maisForteLig), nl.


melhor_caminho_maisForteLig(Orig, Dest) :-
        asserta(melhor_sol_maisForteLig(_, -1000)),            % guardar informacao da melhor solucao
        asserta(contagem_solucoes(0)),                         % guardar contagem de solucoes
        dfsMaisForte(Orig, Dest, LCaminho, Forca),
        atualiza_melhor_maisFortelig(LCaminho, Forca),
        fail.                                                  % obrigar a voltar atras e gerar um novo caminho (substitui desse modo o findall,
                                                               % porque garante que todas as solucoes serao geradas)


atualiza_melhor_maisFortelig(LCaminho, Forca) :-
        retract(contagem_solucoes(NS)),                        % remocao da contagem de solucoes
        NS1 is NS + 1,                                         % incremento em 1 do numero de solucoes
        asserta(contagem_solucoes(NS1)),                       % guardar número de solucoes atualizado
        melhor_sol_maisForteLig(_, F),
        Forca > F, retract(melhor_sol_maisForteLig(_, _)),     % caso o valor seja superior limpamos o caminho mais forte ate ao momento
        asserta(melhor_sol_maisForteLig(LCaminho, Forca)).     % bem como a sua forca e colocamos o novo (superior)



dfsMaisForte(Orig, Dest, Cam, Forca) :- dfsMaisForte2(Orig, Dest, [Orig], Cam, Forca).

dfsMaisForte2(Dest, Dest, LA, Cam, 0) :- !, reverse(LA, Cam).

dfsMaisForte2(Act, Dest, LA, Cam, Forca) :-
        no(NAct, Act, _),                                               % se existir no com nome que se encontra em Act, prossegue
        ( ligacao(NAct, NX, F1, F2); ligacao(NX, NAct, F2, F1) ),       % se existir ligacao NAct->NX ou NX -> NAct, prossegue
        no(NX, X, _),                                                   % se existir no ao qual Act está conectado, prossegue
        \+ member(X, LA),                                               % se X nao estiver presente em LA, prossegue
        dfsMaisForte2(X, Dest, [X|LA], Cam, FX),                        % e volta a ser chamado o metodo dfsMaisForte2, colocando X como cabeça de LA
        Forca is (FX + F1 + F2).                                        % Forca passara a ter o valor de FX que vem de tras, somado com as forcas de ligacao F1 e F2



% Algoritmo caminho mais forte (Unidirecional)

:- dynamic melhor_sol_maisForteUniLig/2.
:- dynamic contagem_solucoes_uni/1.

plan_maisForteUniLig(Orig, Dest, LCaminho_maisForteLig) :-
        get_time(Ti),                                                           % guardar informacao da melhor solucao
        (melhor_caminho_maisForteUniLig(Orig, Dest) ; true),                    % chamada ao metodo melhor_caminho_strongerLig, sendo que caso este dê false, como temos "; true", será sempre feito o que vem a seguir
        retract(melhor_sol_maisForteUniLig(LCaminho_maisForteLig, Forca)),      % remocao da melhor solucao
        retract(contagem_solucoes_uni(NSol)),                                   % remocao da contagem do numero de solucoes
        get_time(Tf),                                                           % retorna o tempo atual como TimeStamp (finaliza contagem de tempo)
        T is Tf-Ti,                                                             % determinar tempo decorrido
        write('Numero de solucoes obtidas: '), write(NSol), nl,
        write('Tempo de geracao da solucao obtida: '), write(T), write(' segundos'), nl,
        write('Forca de ligacao da solucao obtida: '), write(Forca), nl,
        write('Caminho mais forte obtido:'), write(LCaminho_maisForteLig), nl.



melhor_caminho_maisForteUniLig(Orig, Dest) :-
        asserta(melhor_sol_maisForteUniLig(_, -1000)),                          % guardar informacao da melhor solucao (inicalmente a força será -10000)
        asserta(contagem_solucoes_uni(0)),                                      % guardar contagem de solucoes (inicalmente terá o valor de 0)
        dfsMaisForteUni(Orig, Dest, LCaminho, Forca),
        atualiza_melhor_maisForteUniLig(LCaminho, Forca),
        fail.                                                                   % obrigar a voltar atras e gerar um novo caminho (substitui desse modo o findall,
                                                                                % porque garante que todas as solucoes serao geradas)



atualiza_melhor_maisForteUniLig(LCaminho, Forca) :-
        retract(contagem_solucoes_uni(NS)),                                     % remocao da contagem de solucoes
        NS1 is NS + 1,                                                          % incremento em 1 do numero de solucoes
        asserta(contagem_solucoes_uni(NS1)),                                    % guardar número de solucoes atualizado
        melhor_sol_maisForteUniLig(_, F),
        Forca > F, retract(melhor_sol_maisForteUniLig(_, _)),                   % caso o valor seja superior limpamos o caminho mais forte ate ao momento
        asserta(melhor_sol_maisForteUniLig(LCaminho, Forca)).                   % bem como a sua forca e colocamos o novo (superior)



dfsMaisForteUni(Orig, Dest, Cam, Forca) :- dfsMaisForteUni2(Orig, Dest, [Orig], Cam, Forca).

dfsMaisForteUni2(Dest, Dest, LA, Cam, 0) :- !, reverse(LA, Cam).

dfsMaisForteUni2(Act, Dest, LA, Cam, Forca) :-
        no(NAct, Act, _),                                                       % se existir no com o nome que se encontra em Act, prossegue
        ( ligacao(NAct, NX, F1, _); ligacao(NX, NAct, _, F1) ),                 % se existir ligacao NAct->NX ou NX -> NAct, prossegue
        no(NX, X, _),                                                           % se existir no ao qual Act está conectado, prossegue
        \+ member(X, LA),                                                       % se X nao estiver presente em LA, prossegue
        dfsMaisForteUni2(X, Dest, [X|LA], Cam, FX),                             % e volta a ser chamado o metodo dfsMaisForte2, colocando X como cabeça de LA
        Forca is (FX + F1).                                                     % Forca passara a ter o valor de FX que vem de tras, somado com a forcas de ligacao F1


%----------------------------------------------------------------Caminho Mais Curto--------------------------------------


:-dynamic melhor_sol_minlig/2.

dfs(Orig,Dest,Cam):-dfs2(Orig,Dest,[Orig],Cam).

dfs2(Dest,Dest,LA,Cam):-!,reverse(LA,Cam).
dfs2(Act,Dest,LA,Cam):-no(NAct,Act,_),(ligacao(NAct,NX,_,_);ligacao(NX,NAct,_,_)),
    no(NX,X,_),\+ member(X,LA),dfs2(X,Dest,[X|LA],Cam).


plan_minlig(Orig,Dest,LCaminho_minlig):-
		(melhor_caminho_minlig(Orig,Dest);true),
		retract(melhor_sol_minlig(LCaminho_minlig,_)).

melhor_caminho_minlig(Orig,Dest):-
		asserta(melhor_sol_minlig(_,10000)),
		dfs(Orig,Dest,LCaminho),
		atualiza_melhor_minlig(LCaminho),
		fail.

atualiza_melhor_minlig(LCaminho):-
		melhor_sol_minlig(_,N),
		length(LCaminho,C),
		C<N,retract(melhor_sol_minlig(_,_)),
		asserta(melhor_sol_minlig(LCaminho,C)).

%Com findall

caminho_mais_curto(X,Y,[],CaminhoMaisCurto):-
	get_time(Ti),
	allcon(X,Y,R),encontrar_caminho(R,CaminhoMaisCurto),
	get_time(Tf),
	T is Tf-Ti,
	write('Tempo de geracao da solucao:'),write(T),nl.

encontrar_caminho([X|L],CaminhoMaisCurtoFinal):-encontrar_caminho2(L,X,CaminhoMaisCurtoFinal).
encontrar_caminho2([],CaminhoMaisCurto,CaminhoMaisCurtoFinal):-!,append([],CaminhoMaisCurto,CaminhoMaisCurtoFinal).
encontrar_caminho2([X|L],CaminhoMaisCurto,CaminhoMaisCurtoFinal):-length(X,Result),length(CaminhoMaisCurto,Result2),Result<Result2,
encontrar_caminho2(L,X,CaminhoMaisCurtoFinal);encontrar_caminho2(L,CaminhoMaisCurto,CaminhoMaisCurtoFinal).
allcon(X,Y,R):-findall(R1,dfs(X,Y,R1),R).

%-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
todas_combinations(X,LTags,LJogadores):-
    findall(L,combination(X,LTags,L),LcombXTags),
    verificar_x(LcombXTags,LJogadores).
verificar_x(LCombs,LJogadores):-
    findall(J,(no(_,J,LJ),member(C,LCombs),intersection(LJ,C,C2),equal_set(C,C2)),LJogadores1),
    sort(LJogadores1,LJogadores).
equal_set([],[]).
equal_set([X|Xs],Ys):-
    member(X,Ys),
    delete(Ys,X,Ys2),
    equal_set(Xs,Ys2).
combination(0,_,[]):-!.
combination(X,[Tag|L],[Tag|T]):-X1 is X-1, combination(X1,L,T).
combination(X,[_|L],T):- combination(X,L,T).