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



% Algoritmo Best First (Com Função Multicritério, vários resultados)

bestfs1_mc(Orig,Dest,Cam,Custo):-
                bestfs12_mc(Dest,[[Orig]],Cam,Custo),
                write('Caminho='),write(Cam),nl.

bestfs12_mc(Dest,[[Dest|T]|_],Cam,Custo):-
                reverse([Dest|T],Cam), calcula_custo_mc(Cam,Custo).

bestfs12_mc(Dest,[[Dest|_]|LLA2],Cam,Custo):-
                !,bestfs12_mc(Dest,LLA2,Cam,Custo).

bestfs12_mc(Dest,LLA,Cam,Custo):-
                member1(LA,LLA,LLA1),LA=[Act|_],
                ((Act==Dest,!,bestfs12_mc(Dest,[LA|LLA1],Cam,Custo)) ; (findall((CX,[X|LA]),
                (no(NAct,Act,_),no(NX,X,_),(ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),CX is FL+FR,
                \+member(X,LA)),Novos),
                Novos\==[],!,
                sort(0,@>=,Novos,NovosOrd),
                retira_custos(NovosOrd,NovosOrd1),
                append(NovosOrd1,LLA1,LLA2),
                %write('LLA2='),write(LLA2),nl,
                bestfs12_mc(Dest,LLA2,Cam,Custo))).

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



% Algoritmo Best First (Sem Função Multicritério, apenas um resultado)

bestfs1_one(Orig,Dest,Cam,Custo):-
                bestfs12_one(Dest,[[Orig]],Cam,Custo).

bestfs12_one(Dest,[[Dest|T]|_],Cam,Custo):-
                reverse([Dest|T],Cam), calcula_custo(Cam,Custo),write('Caminho='),write(Cam),nl,
                !.

bestfs12_one(Dest,[[Dest|_]|_],_,_):-
                !.

bestfs12_one(Dest,LLA,Cam,Custo):-
                member1(LA,LLA,LLA1),LA=[Act|_],
                ((Act==Dest,!,bestfs12_one(Dest,[LA|LLA1],Cam,Custo)) ; (findall((CX,[X|LA]),
                (no(NAct,Act,_),no(NX,X,_),(ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),CX is FL+FR,
                \+member(X,LA)),Novos),
                Novos\==[],!,
                sort(0,@>=,Novos,NovosOrd),
                retira_custos(NovosOrd,NovosOrd1),
                append(NovosOrd1,LLA1,LLA2),
                %write('LLA2='),write(LLA2),nl,
                bestfs12_one(Dest,LLA2,Cam,Custo))).

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

bestfs1(Orig,Dest,Cam,Custo):-
                bestfs12(Dest,[[Orig]],Cam,Custo),
                write('Caminho='),write(Cam),nl.

bestfs12(Dest,[[Dest|T]|_],Cam,Custo):-
                reverse([Dest|T],Cam), calcula_custo(Cam,Custo).

bestfs12(Dest,[[Dest|_]|LLA2],Cam,Custo):-
                !,bestfs12(Dest,LLA2,Cam,Custo).

bestfs12(Dest,LLA,Cam,Custo):-
                member1(LA,LLA,LLA1),LA=[Act|_],
                ((Act==Dest,!,bestfs12(Dest,[LA|LLA1],Cam,Custo)) ; (findall((CX,[X|LA]),
                (no(NAct,Act,_),no(NX,X,_),(ligacao(NAct,NX,FL,_,FR,_);ligacao(NX,NAct,_,FL,_,FR)),CX is FL+FR,
                \+member(X,LA)),Novos),
                Novos\==[],!,
                sort(0,@>=,Novos,NovosOrd),
                retira_custos(NovosOrd,NovosOrd1),
                append(NovosOrd1,LLA1,LLA2),
                %write('LLA2='),write(LLA2),nl,
                bestfs12(Dest,LLA2,Cam,Custo))).

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