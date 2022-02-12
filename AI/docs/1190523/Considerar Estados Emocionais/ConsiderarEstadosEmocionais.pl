% Verificação dos níveis de emoção

verifyEmotions(NAngustiado,NMedroso,NDesapontado,NRemorsos,NNervoso):-
        LVL is 0.5,
        NAngustiado =< LVL, 
        NMedroso =< LVL, 
        NDesapontado =< LVL, 
        NRemorsos =< LVL, 
        NNervoso =< LVL.

% Algoritmo Best First (Com Função Multicritério, apenas um resultado e consideração de estados de humor)

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
                (no(NAct,Act,_,_,_,_,_,_,_,_,_,_,_),no(NX,X,_,NAngustiado,NMedroso,NDesapontado,NRemorsos,NNervoso,_,_,_,_,_),
                verifyEmotions(NAngustiado,NMedroso,NDesapontado,NRemorsos,NNervoso),
                (ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),CX is FL+FR,
                \+member(X,LA)),Novos),
                Novos\==[],!,
                sort(0,@>=,Novos,NovosOrd),
                retira_custos(NovosOrd,NovosOrd1),
                append(NovosOrd1,LLA1,LLA2),
                %write('LLA2='),write(LLA2),nl,
                bestfs12_mc_one(Dest,LLA2,Cam,Custo,N))).

member1(LA,[LA|LAA],LAA).
member1(LA,[_|LAA],LAA1):- member1(LA,LAA,LAA1).

retira_custos([],[]).
retira_custos([(_,LA)|L],[LA|L1]):- retira_custos(L,L1).

calcula_custo_mc([Act,X],R):-!,
                 no(NAct,Act,_,_,_,_,_,_,_,_,_,_,_),
                 no(NX,X,_,_,_,_,_,_,_,_,_,_,_),
                 (ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),
                 multicriterio(FL, FR, R).

calcula_custo_mc([Act,X|L],P):-calcula_custo_mc([X|L],P1), 
                 no(NAct,Act,_,_,_,_,_,_,_,_,_,_,_),
                 no(NX,X,_,_,_,_,_,_,_,_,_,_,_),
                 (ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),
                 multicriterio(FL, FR, R),
                 P is P1 + R.


% Algoritmo Best First (Com Função Multicritério, vários resultados e consideração de estados de humor)

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
                (no(NAct,Act,_,_,_,_,_,_,_,_,_,_,_),no(NX,X,_,NAngustiado,NMedroso,NDesapontado,NRemorsos,NNervoso,_,_,_,_,_),
                verifyEmotions(NAngustiado,NMedroso,NDesapontado,NRemorsos,NNervoso),
                (ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),CX is FL+FR,
                \+member(X,LA)),Novos),
                Novos\==[],!,
                sort(0,@>=,Novos,NovosOrd),
                retira_custos(NovosOrd,NovosOrd1),
                append(NovosOrd1,LLA1,LLA2),
                %write('LLA2='),write(LLA2),nl,
                bestfs12_mc(Dest,LLA2,Cam,Custo,N))).

%member1(LA,[LA|LAA],LAA).
%member1(LA,[_|LAA],LAA1):- member1(LA,LAA,LAA1).

%retira_custos([],[]).
%retira_custos([(_,LA)|L],[LA|L1]):- retira_custos(L,L1).

%calcula_custo_mc([Act,X],R):-!,
%                no(NAct,Act,_),
%                no(NX,X,_),
%                (ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),
%                multicriterio(FL, FR, R).

%calcula_custo_mc([Act,X|L],P):-calcula_custo_mc([X|L],P1), 
%                no(NAct,Act,_),
%                no(NX,X,_),
%                (ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),
%                multicriterio(FL, FR, R),
%                P is P1 + R.

% Algoritmo A* (Com Função MultiCritério e consideração de estados de humor)

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
        no(Act,_,_,_,_,_,_,_,_,_,_,_,_),
        no(X,_,_,NAngustiado,NMedroso,NDesapontado,NRemorsos,NNervoso,_,_,_,_,_),
        verifyEmotions(NAngustiado,NMedroso,NDesapontado,NRemorsos,NNervoso),
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


% Algoritmo DFS com funcao multicriterio e consideração de estados de humor

all_dfs(Nome1,Nome2,Cam):-
    dfs(Nome1,Nome2,Cam),!,
    length(Cam,NLCam),
    write(NLCam), nl,
    write('List of all the paths: '),
    write(Cam),nl,nl.

dfs(Orig,Dest,Cam):-dfs2(Orig,Dest,[Orig],Cam).
dfs2(Dest,Dest,LA,Cam):-!,reverse(LA,Cam).
dfs2(Act,Dest,LA,Cam):-
    no(NAct,Act,_,_,_,_,_,_,_,_,_,_,_),
    (ligacao(NAct,NX,_,_,_,_);ligacao(NX,NAct,_,_,_,_)),
    no(NX,X,_,NAngustiado,NMedroso,NDesapontado,NRemorsos,NNervoso,_,_,_,_,_),
    verifyEmotions(NAngustiado,NMedroso,NDesapontado,NRemorsos,NNervoso),
    \+ member(X,LA),
    dfs2(X,Dest,[X|LA],Cam).

compute_value(U1,U2,V):-!,
    ligacao(U1,U2,ForcaU1_U2,ForcaU2_U1,Likes,Deslikes),
    F is ForcaU1_U2 + ForcaU2_U1,
    LD is Likes - Deslikes,
    V is F * 1 + LD * 0.5,
    multicriterio(F,LD,V).

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