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

:- dynamic melhor_sol_mais_segura/2.

%:------------------------------------------------------------------------------ Caminho Mais Seguro Só No Sentido Da Travessia ---------------------------------------------------------------------------------------.

%:--------------------------------------------------------------------------------------------- Unidirecional ---------------------------------------------------------------------------------------------------------. 
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
%:---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

%:-------------------------------------------------------------------------------------------- Bidirecional -----------------------------------------------------------------------------------------------------------. 
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
%:---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

%:---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.


%:----------------------- Caminho Mais seguro considerando a soma das duas forças em cada ramo da travessia e garantindo que em nenhuma delas temos uma força abaixo de um dado limiar --------------------------------.

%:--------------------------------------------------------------------------------------------- Unidirecional ---------------------------------------------------------------------------------------------------------. 
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
%:---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

%:-------------------------------------------------------------------------------------------- Bidirecional -----------------------------------------------------------------------------------------------------------. 
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
%:---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

%:---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.
