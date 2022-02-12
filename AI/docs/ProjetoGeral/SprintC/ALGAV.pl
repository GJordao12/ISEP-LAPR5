% ------------------------------------------------------------------------------------------------------------------------------------------------------------------: BASE DE CONHECIMENTO ALGAV :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

no(1,ana,[natureza,pintura,musica,sw,porto]).

no(11,antonio,[natureza,pintura,carros,futebol,lisboa]).
no(12,beatriz,[natureza,musica,carros,porto,moda]).
no(13,carlos,[natureza,musica,sw,futebol,coimbra]).
no(14,daniel,[natureza,cinema,jogos,sw,moda]).
no(15,afonso,[natureza,pintura,carros,futebol,lisboa]).
no(16,artur,[natureza,musica,carros,porto,moda]).

no(21,eduardo,[natureza,cinema,teatro,carros,coimbra]).
no(22,isabel,[natureza,musica,porto,lisboa,cinema]).
no(23,jose,[natureza,pintura,sw,musica,carros,lisboa]).
no(24,luisa,[natureza,cinema,jogos,moda,porto]).
no(25,bento,[natureza,pintura,carros,futebol,lisboa]).
no(26,benjamim,[natureza,musica,carros,porto,moda]).

no(31,maria,[natureza,pintura,musica,moda,porto]).
no(32,anabela,[natureza,cinema,musica,tecnologia,porto]).
no(33,andre,[natureza,carros,futebol,coimbra]).
no(34,catia,[natureza,musica,cinema,lisboa,moda]).
no(35,ethan,[natureza,pintura,carros,futebol,lisboa]).
no(36,erik,[natureza,musica,carros,porto,moda]).

no(41,cesar,[natureza,teatro,tecnologia,futebol,porto]).
no(42,diogo,[natureza,futebol,sw,jogos,porto]).
no(43,ernesto,[natureza,teatro,carros,porto]).
no(44,isaura,[natureza,moda,tecnologia,cinema]).
no(45,fabricio,[natureza,pintura,carros,futebol,lisboa]).
no(46,gregorio,[natureza,musica,carros,porto,moda]).

no(51,rodolfo,[natureza,musica,sw]).
no(52,aurora,[natureza,futebol,sw,jogos,porto]).
no(53,anastacia,[natureza,teatro,carros,porto]).
no(54,bianca,[natureza,moda,tecnologia,cinema]).
no(55,cecilia,[natureza,pintura,carros,futebol,lisboa]).
no(56,flora,[natureza,musica,carros,porto,moda]).

%no(61,rita,[moda,tecnologia,cinema]).

%no(200,sara,[natureza,moda,musica,sw,coimbra]).

ligacao(1,11,10,8,12,4).
ligacao(1,12,2,6,4,5).
ligacao(1,13,-3,-2,1,-3).
/*ligacao(1,17,24,8,9,4).*/
ligacao(1,14,1,-5,1,4).
ligacao(11,21,5,7,2,-5).
ligacao(11,22,2,-4,1,4).
ligacao(11,23,-2,8,-5,3).
ligacao(11,24,6,0,4,-5).
ligacao(12,21,4,9,4,5).
ligacao(12,22,-3,-8,1,7).
ligacao(12,23,2,4,3,-6).
ligacao(12,24,-2,4,4,1).
ligacao(13,21,3,2,4,-6).
ligacao(13,22,0,-3,2,-6).
ligacao(13,23,5,9,4,-8).
ligacao(13,24,-2, 4,2,4).
ligacao(14,21,2,6,5,7).
ligacao(14,22,6,-3,2,-5).
ligacao(14,23,7,0,2,8).
ligacao(14,24,2,2,2,5).
ligacao(17,21,8,6,6,9).
ligacao(17,23,2,2,4,5).
ligacao(17,22,20,2,4,5).
ligacao(17,24,17,2,5,4).
ligacao(21,31,2,1,-4,5).
ligacao(21,32,-2,3,6,2).
ligacao(21,34,4,2,1,-5).
ligacao(21,33,3,5,5,2).
ligacao(22,31,5,-4,3,5).
ligacao(22,32,-1,6,3,-5).
ligacao(22,33,2,1,4,6).
ligacao(22,34,2,3,2,-5).
ligacao(23,31,4,-3,4,6).
ligacao(23,32,3,5,2,1).
ligacao(23,33,4,1,2,4).
ligacao(23,34,-2,-3,2,4).
ligacao(24,31,1,-5,2,-4).
ligacao(24,32,1,0,2,4).
ligacao(24,33,3,-1,2,1).
ligacao(24,34,-1,5,1,6).
ligacao(31,41,2,4,4,2).
ligacao(31,42,6,3,3,8).
ligacao(31,43,2,1,-1,3).
ligacao(31,44,2,1,1,6).
ligacao(32,41,2,3,-7,2).
ligacao(32,42,-1,0,2,-4).
ligacao(32,43,0,1,1,4).
ligacao(32,44,1,2,1,1).
ligacao(33,41,4,-1,6,4).
ligacao(33,42,-1,3,4,-4).
ligacao(33,43,7,2,2,6).
ligacao(33,44,5,-3,-5,3).
ligacao(34,41,3,2,4,6).
ligacao(34,42,1,-1,2,-4).
ligacao(34,43,2,4,4,-5).

% -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------: PREDICADOS :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.


% Algoritmo Best First (Com Função Multicritério, vários resultados)

bestfs1_mc(Orig,Dest,Cam,Custo,N):-
                bestfs12_mc(Dest,[[Orig]],Cam,Custo,N),
                write('Caminho='),write(Cam),nl.

bestfs12_mc(Dest,[[Dest|T]|_],Cam,Custo,_):-
                reverse([Dest|T],Cam), calcula_custo_mc(Cam,Custo).

bestfs12_mc(Dest,[[Dest|_]|LLA2],Cam,Custo,N):-
                !,bestfs12_mc(Dest,LLA2,Cam,Custo,N).

bestfs12_mc(Dest,LLA,Cam,Custo,N):-
                member1(LA,LLA,LLA1),LA=[Act|_],
                length(LA,Tamanho),
                Tamanho =< N,
                ((Act==Dest,!,bestfs12_mc(Dest,[LA|LLA1],Cam,Custo,N)) ; (findall((CX,[X|LA]),
                (no(NAct,Act,_),no(NX,X,_),(ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),CX is FL+FR,
                \+member(X,LA)),Novos),
                Novos\==[],!,
                sort(0,@>=,Novos,NovosOrd),
                retira_custos(NovosOrd,NovosOrd1),
                append(NovosOrd1,LLA1,LLA2),
                %write('LLA2='),write(LLA2),nl,
                bestfs12_mc(Dest,LLA2,Cam,Custo,N))).

member1(LA,[LA|LAA],LAA).
member1(LA,[_|LAA],LAA1):- member1(LA,LAA,LAA1).

retira_custos([],[]).
retira_custos([(_,LA)|L],[LA|L1]):- retira_custos(L,L1).

calcula_custo_mc([Act,X],R):-!,
                no(NAct,Act,_),
                no(NX,X,_),
                (ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),
                multicriterio(FL, FR, R).

calcula_custo_mc([Act,X|L],P):-calcula_custo_mc([X|L],P1), 
                no(NAct,Act,_),
                no(NX,X,_),
                (ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),
                multicriterio(FL, FR, R),
                P is P1 + R.


% Algoritmo Best First (Com Função Multicritério, apenas um resultado)

bestfs1_mc_one(Orig,Dest,Cam,Custo,N):-
                bestfs12_mc_one(Dest,[[Orig]],Cam,Custo,N).

bestfs12_mc_one(Dest,[[Dest|T]|_],Cam,Custo,_):-
                reverse([Dest|T],Cam), 
                calcula_custo_mc(Cam,Custo),
                write('Caminho='),write(Cam),nl,
                !.

bestfs12_mc_one(Dest,[[Dest|_]|_],_,_,_):-
                !.

bestfs12_mc_one(Dest,LLA,Cam,Custo,N):-
                member1(LA,LLA,LLA1),LA=[Act|_],
                length(LA,Tamanho),
                Tamanho =< N,
                ((Act==Dest,!,bestfs12_mc_one(Dest,[LA|LLA1],Cam,Custo,N)) ; (findall((CX,[X|LA]),
                (no(NAct,Act,_),no(NX,X,_),(ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),CX is FL+FR,
                \+member(X,LA)),Novos),
                Novos\==[],!,
                sort(0,@>=,Novos,NovosOrd),
                retira_custos(NovosOrd,NovosOrd1),
                append(NovosOrd1,LLA1,LLA2),
                %write('LLA2='),write(LLA2),nl,
                bestfs12_mc_one(Dest,LLA2,Cam,Custo,N))).

% member1(LA,[LA|LAA],LAA).
% member1(LA,[_|LAA],LAA1):- member1(LA,LAA,LAA1).

% retira_custos([],[]).
% retira_custos([(_,LA)|L],[LA|L1]):- retira_custos(L,L1).

% calcula_custo_mc([Act,X],R):-!,
%                 no(NAct,Act,_),
%                 no(NX,X,_),
%                 (ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),
%                 multicriterio(FL, FR, R).

% calcula_custo_mc([Act,X|L],P):-calcula_custo_mc([X|L],P1), 
%                 no(NAct,Act,_),
%                 no(NX,X,_),
%                 (ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),
%                 multicriterio(FL, FR, R),
%                 P is P1 + R.


% Algoritmo Best First (Sem Função Multicritério, apenas um resultado)

bestfs1_one(Orig,Dest,Cam,Custo,N):-
                bestfs12_one(Dest,[[Orig]],Cam,Custo,N).

bestfs12_one(Dest,[[Dest|T]|_],Cam,Custo,_):-
                reverse([Dest|T],Cam), calcula_custo(Cam,Custo),write('Caminho='),write(Cam),nl,
                !.

bestfs12_one(Dest,[[Dest|_]|_],_,_,_):-
                !.

bestfs12_one(Dest,LLA,Cam,Custo,N):-
                member1(LA,LLA,LLA1),LA=[Act|_],
                length(LA,Tamanho),
                Tamanho =< N,
                ((Act==Dest,!,bestfs12_one(Dest,[LA|LLA1],Cam,Custo,N)) ; (findall((CX,[X|LA]),
                (no(NAct,Act,_),no(NX,X,_),(ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),CX is FL+FR,
                \+member(X,LA)),Novos),
                Novos\==[],!,
                sort(0,@>=,Novos,NovosOrd),
                retira_custos(NovosOrd,NovosOrd1),
                append(NovosOrd1,LLA1,LLA2),
                %write('LLA2='),write(LLA2),nl,
                bestfs12_one(Dest,LLA2,Cam,Custo,N))).

% member1(LA,[LA|LAA],LAA).
% member1(LA,[_|LAA],LAA1):- member1(LA,LAA,LAA1).

% retira_custos([],[]).
% retira_custos([(_,LA)|L],[LA|L1]):- retira_custos(L,L1).

calcula_custo([Act,X],C):- !, no(NAct,Act,_), no(NX,X,_), (ligacao(NAct,NX,S,_,_,_) ; ligacao(NX,NAct,_,S,_,_)), C is S.
calcula_custo([Act,X|L],P):-
                    calcula_custo([X|L],P1),
                    no(NAct,Act,_), no(NX,X,_),
                    (ligacao(NAct,NX,S,_,_,_) ; ligacao(NX,NAct,_,S,_,_)),
                    P is P1 + S.


% Algoritmo Best First (Sem Função Multicritério, vários resultados)

bestfs1(Orig,Dest,Cam,Custo,N):-
                bestfs12(Dest,[[Orig]],Cam,Custo,N),
                write('Caminho='),write(Cam),nl.

bestfs12(Dest,[[Dest|T]|_],Cam,Custo,_):-
                reverse([Dest|T],Cam), calcula_custo(Cam,Custo).

bestfs12(Dest,[[Dest|_]|LLA2],Cam,Custo,N):-
                !,bestfs12(Dest,LLA2,Cam,Custo,N).

bestfs12(Dest,LLA,Cam,Custo,N):-
                member1(LA,LLA,LLA1),LA=[Act|_],
                length(LA, Tamanho),
                Tamanho =< N,
                ((Act==Dest,!,bestfs12(Dest,[LA|LLA1],Cam,Custo,N)) ; (findall((CX,[X|LA]),
                (no(NAct,Act,_),no(NX,X,_),(ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),CX is FL+FR,
                \+member(X,LA)),Novos),
                Novos\==[],!,
                sort(0,@>=,Novos,NovosOrd),
                retira_custos(NovosOrd,NovosOrd1),
                append(NovosOrd1,LLA1,LLA2),
                %write('LLA2='),write(LLA2),nl,
                bestfs12(Dest,LLA2,Cam,Custo,N))).

% member1(LA,[LA|LAA],LAA).
% member1(LA,[_|LAA],LAA1):- member1(LA,LAA,LAA1).

% retira_custos([],[]).
% retira_custos([(_,LA)|L],[LA|L1]):- retira_custos(L,L1).

% calcula_custo([Act,X],C):- !, no(NAct,Act,_), no(NX,X,_), (ligacao(NAct,NX,S,_,_,_) ; ligacao(NX,NAct,_,S,_,_)), C is S.
% calcula_custo([Act,X|L],P):-
%                    calcula_custo([X|L],P1),
%                    no(NAct,Act,_), no(NX,X,_),
%                   (ligacao(NAct,NX,S,_,_,_) ; ligacao(NX,NAct,_,S,_,_)),
%                   P is P1 + S.

% Função Multicritério

multicriterio(FL, FR, R):- 
    ((FL < 50, with_FL_0(FL,FR,R)); 
    (FL < 100, with_FL_50(FL,FR,R)); 
    (FL = 100, with_FL_100(FL,FR,R))),!.

with_FL_0(FL, FR, R):-
    ((FR =< -200, with_FL_0_FR_smaller(FL,FR,R));
    (FR >= 200, with_FL_0_FR_upper(FL,FR,R));
    with_FL_0_FR_equal(FL,FR,R)).

with_FL_50(FL,FR,R):-
    ((FR =< -200, with_FL_50_FR_smaller(FL,FR,R));
    (FR >= 200, with_FL_50_FR_upper(FL,FR,R));
    with_FL_50_FR_equal(FL,FR,R)).

with_FL_100(FL,FR,R):-
    ((FR =< -200, with_FL_100_FR_smaller(FL,FR,R));
    (FR >= 200, with_FL_100_FR_upper(FL,FR,R));
    with_FL_100_FR_equal(FL,FR,R)).

with_FL_0_FR_smaller(_,_,R):- R is 0.
with_FL_0_FR_upper(_,_,R):- R is 50.
with_FL_0_FR_equal(_,_,R):- R is 25.

with_FL_50_FR_smaller(_,_,R):- R is 25.
with_FL_50_FR_upper(_,_,R):- R is 75.
with_FL_50_FR_equal(_,_,R):- R is 50.

with_FL_100_FR_smaller(_,_,R):- R is 50.
with_FL_100_FR_upper(_,_,R):- R is 100.
with_FL_100_FR_equal(_,_,R):- R is 75.

% Algoritmo A* (Sem Função MultiCritério)

aStar(Orig,Dest,Cam,Custo,N):-
    forcas_ligacao_decrescente(Orig,N,RedeUtilizadores),
    aStar2(Dest,[(_,0,[Orig])],RedeUtilizadores,Cam,Custo,N),!.

aStar2(Dest,[(_,Custo,[Dest|T])|_],_,Cam,Custo,_):-
        reverse([Dest|T],Cam).

aStar2(Dest,[(_,Ca,LA)|Outros],RedeUtilizadores,Cam,Custo,N):-
        LA=[Act|_],
        length(LA,C),
        findall((CEX,CaX,[X|LA]),
        (C=<N,
        Dest\==Act,
        (ligacao(Act,X,ForcaX,_,_,_);ligacao(X,Act,_,ForcaX,_,_)),
        \+ member(X,LA),
        CaX is ForcaX + Ca,
        estimativa(ForcaX,X,Dest,C,N,RedeUtilizadores,EstX),
        CEX is CaX + EstX),Novos),
        append(Outros,Novos,Todos),
        sort(0,@>=,Todos,TodosOrd),
        eliminar(TodosOrd,RedeUtilizadores,RedeUtilizadoresAtualizado),
        aStar2(Dest,TodosOrd,RedeUtilizadoresAtualizado,Cam,Custo,N).
											
estimativa(_,Dest,Dest,_,_,_,0):- !.
estimativa(ForcaX,_,_,Atual,Destino,ListaInt,Estimativa):-
        Index is Destino - Atual,
        apaga_primeiro(ForcaX,ListaInt,Resultado),
        somar_Forca_N(0,Index,Resultado,Estimativa).

apaga_primeiro(_, [], []).
apaga_primeiro(Term, [Term|Tail], Tail).
apaga_primeiro(Term, [Head|Tail], [Head|Result]) :-
apaga_primeiro(Term, Tail, Result).

somar_Forca_N(N,N,_,0):-!.
somar_Forca_N(C,N,[H|T],S):- 
        C1 is C+1, 
        somar_Forca_N(C1,N,T,S1), 
        S is H + S1.

eliminar([(_,_,[B,A|_])|_],L,NL):-
        ((ligacao(A,B,Custo,_,_,_),!);(ligacao(B,A,_,Custo,_,_))),
        apaga_primeiro(Custo,L,NL).

% Forças de ligação decrescente - Retorna uma lista ordenada das forças de ligação correspondente para a rede de utilizadores até N nível.
forcas_ligacao_decrescente(Orig,N,Lista):-
        tamanho_rede_utilizadores(Orig,N,RedeUtilizadores),
        forcas_ligacao_decrescente_2(RedeUtilizadores,ListaInt),
        flatten(ListaInt,ListaInt2),
        sort(0,@>=,ListaInt2,Lista).								

forcas_ligacao_decrescente_2([_],[]):-!.
forcas_ligacao_decrescente_2([User1|Y],[RedeUtilizadores|Z]):-
        findall([F1,F2],
        (member(User2,Y),
        (ligacao(User1,User2,F1,F2,_,_);ligacao(User2,User1,F2,F1,_,_))),RedeUtilizadores),
        forcas_ligacao_decrescente_2(Y,Z).

% Tamanho rede de utilizadores - Retorna a lista de users até determinado nível
tamanho_rede_utilizadores(Origem,Nivel,Solucao):- 
        tamanho_rede_utilizadores_nivelX([Origem],Nivel,ListaA),
        flatten(ListaA,ListaB),
        sort(ListaB,ListaE),
        Solucao = ListaE.

tamanho_rede_utilizadores_nivelX(_,0,[]):-!.

tamanho_rede_utilizadores_nivelX(ListaB,Nivel,[ListaC|ListaA]):- 
        NY is Nivel - 1,
        tamanho_rede_utilizadores_nivelY(ListaB,ListaI),
        flatten(ListaI,ListaC),
        tamanho_rede_utilizadores_nivelX(ListaC,NY,ListaA).

tamanho_rede_utilizadores_nivelY([],[]):-!.

tamanho_rede_utilizadores_nivelY([UserX|ListaB],[ListaD|ListaC]):- 
        findall(UserY,ligacao(UserX,UserY,_,_,_,_), ListaD),
        tamanho_rede_utilizadores_nivelY(ListaB,ListaC).

aStar_cmc(Orig,Dest,Cam,Custo,N):-
    forcas_ligacao_decrescente_cmc(Orig,N,RedeUtilizadores),
    aStar2_cmc(Dest,[(_,0,[Orig])],RedeUtilizadores,Cam,Custo,N),!.

aStar2_cmc(Dest,[(_,Custo,[Dest|T])|_],_,Cam,Custo,_):-
        reverse([Dest|T],Cam).

aStar2_cmc(Dest,[(_,Ca,LA)|Outros],RedeUtilizadores,Cam,Custo,N):-
        LA=[Act|_],
        length(LA,C),
        findall((CEX,CaX,[X|LA]),
        (C=<N,
        Dest\==Act,
        (ligacao(Act,X,ForcaX,_,ForcaY,_);ligacao(X,Act,_,ForcaX,_,ForcaY)),
        \+ member(X,LA),multicriterio(ForcaX,ForcaY,Resultado),
        CaX is Resultado + Ca,
        estimativa(Resultado,X,Dest,C,N,RedeUtilizadores,EstX),
        CEX is CaX + EstX),Novos),
        append(Outros,Novos,Todos),
        sort(0,@>=,Todos,TodosOrd),
        eliminar(TodosOrd,RedeUtilizadores,RedeUtilizadoresAtualizado),
        aStar2_cmc(Dest,TodosOrd,RedeUtilizadoresAtualizado,Cam,Custo,N).

forcas_ligacao_decrescente_cmc(Orig,N,Lista):-
        tamanho_rede_utilizadores(Orig,N,RedeUtilizadores),
        forcas_ligacao_decrescente_2_cmc(RedeUtilizadores,ListaInt),
        flatten(ListaInt,ListaInt2),
        sort(0,@>=,ListaInt2,Lista).								

forcas_ligacao_decrescente_2_cmc([_],[]):-!.
forcas_ligacao_decrescente_2_cmc([User1|Y],[RedeUtilizadores|Z]):-
        findall([F1,F2,F3,F4],
        (member(User2,Y),
        (ligacao(User1,User2,F1,F2,F3,F4);ligacao(User2,User1,F2,F1,F4,F3))),RedeUtilizadores),
        forcas_ligacao_decrescente_2(Y,Z).

% Algoritmo caminho mais forte (Bidirecional)

:- dynamic melhor_sol_maisForteLig/2.
:- dynamic contagem_solucoes/1.

plan_maisForteLig(Orig, Dest, LCaminho_maisForteLig) :-
        get_time(Ti),
        (melhor_caminho_maisForteLig(Orig, Dest) ; true),
        retract(melhor_sol_maisForteLig(LCaminho_maisForteLig, Forca)),
        retract(contagem_solucoes(NSol)),
        get_time(Tf),
        T is Tf-Ti,
        write('Numero de solucoes obtidas: '), write(NSol), nl,
        write('Tempo de geracao da solucao obtida: '), write(T), write(' segundos'), nl,
        write('Forca de ligacao da solucao obtida: '), write(Forca), nl,
        write('Caminho mais forte obtido:'), write(LCaminho_maisForteLig), nl.


melhor_caminho_maisForteLig(Orig, Dest) :-
        asserta(melhor_sol_maisForteLig(_, -1000)),
        asserta(contagem_solucoes(0)),
        dfsMaisForte(Orig, Dest, LCaminho, Forca),
        atualiza_melhor_maisFortelig(LCaminho, Forca),
        fail.


atualiza_melhor_maisFortelig(LCaminho, Forca) :-
        retract(contagem_solucoes(NS)),
        NS1 is NS + 1,
        asserta(contagem_solucoes(NS1)),
        melhor_sol_maisForteLig(_, F),
        Forca > F, retract(melhor_sol_maisForteLig(_, _)),
        asserta(melhor_sol_maisForteLig(LCaminho, Forca)).



dfsMaisForte(Orig, Dest, Cam, Forca) :- dfsMaisForte2(Orig, Dest, [Orig], Cam, Forca).

dfsMaisForte2(Dest, Dest, LA, Cam, 0) :- !, reverse(LA, Cam).

dfsMaisForte2(Act, Dest, LA, Cam, Forca) :-
        no(NAct, Act, _),
        ( ligacao(NAct, NX, F1, F2, FR1, FR2); ligacao(NX, NAct, F2, F1, FR2, FR1) ),
        no(NX, X, _),
        \+ member(X, LA),
        dfsMaisForte2(X, Dest, [X|LA], Cam, FX),
        Forca is (FX + F1 + F2 + FR1 + FR2).


% Algoritmo caminho mais forte (Unidirecional)

:- dynamic melhor_sol_maisForteUniLig/2.
:- dynamic contagem_solucoes_uni/1.

plan_maisForteUniLig(Orig, Dest, LCaminho_maisForteLig) :-
                  get_time(Ti),
                  (melhor_caminho_maisForteUniLig(Orig, Dest) ; true),
                  retract(melhor_sol_maisForteUniLig(LCaminho_maisForteLig, Forca)),
                  retract(contagem_solucoes_uni(NSol)),
                  get_time(Tf),
                  T is Tf-Ti,
                  write('Numero de solucoes obtidas: '), write(NSol), nl,
                  write('Tempo de geracao da solucao obtida: '), write(T), write(' segundos'), nl,
                  write('Forca de ligacao da solucao obtida: '), write(Forca), nl,
                  write('Caminho mais forte obtido:'), write(LCaminho_maisForteLig), nl.



melhor_caminho_maisForteUniLig(Orig, Dest) :-
                  asserta(melhor_sol_maisForteUniLig(_, -1000)),
                  asserta(contagem_solucoes_uni(0)),
                  dfsMaisForteUni(Orig, Dest, LCaminho, Forca),
                  atualiza_melhor_maisForteUniLig(LCaminho, Forca),
                  fail.




atualiza_melhor_maisForteUniLig(LCaminho, Forca) :-
                  retract(contagem_solucoes_uni(NS)),
                  NS1 is NS + 1,
                  asserta(contagem_solucoes_uni(NS1)),
                  melhor_sol_maisForteUniLig(_, F),
                  Forca > F, retract(melhor_sol_maisForteUniLig(_, _)),
                  asserta(melhor_sol_maisForteUniLig(LCaminho, Forca)).



dfsMaisForteUni(Orig, Dest, Cam, Forca) :- dfsMaisForteUni2(Orig, Dest, [Orig], Cam, Forca).

dfsMaisForteUni2(Dest, Dest, LA, Cam, 0) :- !, reverse(LA, Cam).

dfsMaisForteUni2(Act, Dest, LA, Cam, Forca) :-
                  no(NAct, Act, _),
                  ( ligacao(NAct, NX, F1, _, FR1, _); ligacao(NX, NAct, _, F1, _, FR1) ),
                  no(NX, X, _),
                  \+ member(X, LA),
                  dfsMaisForteUni2(X, Dest, [X|LA], Cam, FX),
                  Forca is (FX + F1 + FR1).

% Algoritmo DFS com funcao multicriterio

all_dfs(Nome1,Nome2,Cam):-
    get_time(T1),
    dfs(Nome1,Nome2,Cam),!,
    length(Cam,NLCam),
    get_time(T2),
    write(NLCam),
    write('solutions found in'),
    T is T2-T1,write(T),write('seconds'),nl, write('List of all the paths: '),
    write(Cam),nl,nl.

dfs(Orig,Dest,Cam):-dfs2(Orig,Dest,[Orig],Cam).
dfs2(Dest,Dest,LA,Cam):-!,reverse(LA,Cam).
dfs2(Act,Dest,LA,Cam):-
    no(NAct,Act,_),
   (ligacao(NAct,NX,_,_,_,_);ligacao(NX,NAct,_,_,_,_)),
    no(NX,X,_),\+ member(X,LA),dfs2(X,Dest,[X|LA],Cam).
compute_value(U1,U2,V):-!,
    ligacao(U1,U2,ForcaU1_U2,ForcaU2_U1,Likes,Deslikes),
   F is ForcaU1_U2 + ForcaU2_U1,
   LD is Likes - Deslikes,
   V is F * 1 + LD * 0.5,
   multicriterio(F,LD,V).

% Algoritmo DFS com outra funcao multicriterio

dfs1MC(Orig,Dest,Cam,K,Total):-
    retractall(total()),    
    asserta(total(0)),
    dfs1MC(Orig,Dest,[Orig],Cam,0,K),
    retract(total(Total)).

dfs1MC(Dest,Dest,LA,Cam,_,_):-
    reverse(LA,Cam).

dfs1MC(Act,Dest,LA,Cam,N,K):-
    ligacao(Act,X,A,_,B,_),
    \+ member(X,LA),multicriterio(A,B,T),
    retract(total(Total1)),
    Total is Total1 + T,
    asserta(total(Total)),
    N1 is N + 1,
    N1 =< K,
    dfs1MC(X,Dest,[X|LA],Cam,N1,K).

% Algoritmo DFS sem funcao multicriterio
dfs1(Orig,Dest,Cam,K,Total):-
    retractall(total()),    
    asserta(total(0)),
    dfs1(Orig,Dest,[Orig],Cam,0,K),
    retract(total(Total)).

dfs1(Dest,Dest,LA,Cam,_,_):-
    reverse(LA,Cam).

dfs1(Act,Dest,LA,Cam,N,K):-
    ligacao(Act,X,T,_,_,_),
    \+ member(X,LA),
    retract(total(Total1)),
    Total is Total1 + T,
    asserta(total(Total)),
    N1 is N + 1,
    N1 =< K,
    dfs1(X,Dest,[X|LA],Cam,N1,K).    
