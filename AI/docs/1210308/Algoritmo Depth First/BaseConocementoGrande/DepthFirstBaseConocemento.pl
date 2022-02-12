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

ligacao(1,11,10,8,5,3).
ligacao(1,12,2,6,58,63).
ligacao(1,13,-3,-2,89,3).
ligacao(1,14,1,-5,75,20).
ligacao(11,21,5,7,64,10).
ligacao(11,22,2,-4,2,0).
ligacao(11,23,-2,8,55,26).
ligacao(11,24,6,0,86,56).
ligacao(12,21,4,9,35,34).
ligacao(12,22,-3,-8,81,92).
ligacao(12,23,2,4,64,31).
ligacao(12,24,-2,4,25,54).
ligacao(13,21,3,2,18,2).
ligacao(13,22,0,-3,43,26).
ligacao(13,23,5,9,31,20).
ligacao(13,24,-2, 4,10,2).
ligacao(14,21,2,6,16,13).
ligacao(14,22,6,-3,19,20).
ligacao(14,23,7,0,74,35).
ligacao(14,24,2,2,54,21).
ligacao(21,31,2,1,45,23).
ligacao(21,32,-2,3,18,26).
ligacao(21,33,3,5,33,18).
ligacao(21,34,4,2,42,43).
ligacao(22,31,5,-4,20,3).
ligacao(22,32,-1,6,19,4).
ligacao(22,33,2,1,86,67).
ligacao(22,34,2,3,90,50).
ligacao(23,31,4,-3,43,27).
ligacao(23,32,3,5,40,37).
ligacao(23,33,4,1,64,35).
ligacao(23,34,-2,-3,63,24).
ligacao(24,31,1,-5,74,86).
ligacao(24,32,1,0,90,20).
ligacao(24,33,3,-1,74,54).
ligacao(24,34,-1,5,29,40).
ligacao(31,41,2,4,25,35).
ligacao(31,42,6,3,59,30).
ligacao(31,43,2,1,23,15).
ligacao(31,44,2,1,12,5).
ligacao(32,41,2,3,76,45).
ligacao(32,42,-1,0,76,54).
ligacao(32,43,0,1,34,35).
ligacao(32,44,1,2,38,34).
ligacao(33,41,4,-1,64,50).
ligacao(33,42,-1,3,86,35).
ligacao(33,43,7,2,45,24).
ligacao(33,44,5,-3,64,33).
ligacao(34,41,3,2,66,32).
ligacao(34,42,1,-1,87,54).
ligacao(34,43,2,4,76,43).
ligacao(34,44,1,-2,54,33).
ligacao(41,200,2,0,20,2).
ligacao(42,200,7,-2,43,23).
ligacao(43,200,-2,4,54,23).
ligacao(44,200,-1,-3,42,54).
ligacao(1,51,6,2,20,2).
ligacao(51,61,7,3,76,54).
ligacao(61,200,2,4,54,2).
all_dfs(Nome1,Nome2,LCam):-
    get_time(T1),
    findall(Cam,dfs(Nome1,Nome2,Cam),LCam),
    length(LCam,NLCam),
    get_time(T2),
    write(NLCam),
    write('solutions found in'),
    T is T2-T1,write(T),write('seconds'),nl, write('List of all the paths: '),
    write(LCam),nl,nl.

dfs(Orig,Dest,Cam):-dfs2(Orig,Dest,[Orig],Cam). 
dfs2(Dest,Dest,LA,Cam):-!,reverse(LA,Cam).
dfs2(Act,Dest,LA,Cam):-
    no(NAct,Act,_),
   (ligacao(NAct,NX,_,_,_,_);ligacao(NX,NAct,_,_,_,_)),
    no(NX,X,_),\+ member(X,LA),dfs2(X,Dest,[X|LA],Cam).
compute_value(U1,U2,V):-
    ligacao(U1,U2,ForcaU1_U2,ForcaU2_U1,Likes,Deslikes),
   F is ForcaU1_U2 + ForcaU2_U1,
   LD is Likes - Deslikes,
   V is F * 1 + LD * 0.5,
   multicriteria_function(F,LD,V). 