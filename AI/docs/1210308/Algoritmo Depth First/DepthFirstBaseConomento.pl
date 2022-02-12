% Base de Conhecimento

no(1,ana,[natureza,pintura,musica,sw,porto]).
no(11,antonio,[natureza,pintura,carros,futebol,lisboa]).
no(12,beatriz,[natureza,musica,carros,porto,moda]).
no(13,carlos,[natureza,musica,sw,futebol,coimbra]).
no(14,daniel,[natureza,cinema,jogos,sw,moda]).
no(21,eduardo,[natureza,cinema,teatro,carros,coimbra]).
no(22,isabel,[natureza,musica,porto,lisboa,cinema]).
no(23,jose,[natureza,pintura,sw,musica,carros,lisboa]).
ligacao(1,11,10,8,5,3).
ligacao(1,12,2,6,58,63).
ligacao(1,13,-3,-2,89,3).
ligacao(1,14,1,-5,75,20).
ligacao(11,21,5,7,64,10).
ligacao(11,22,2,-4,2,0).
ligacao(11,23,-2,8,55,26).
ligacao(12,21,4,9,35,34).
ligacao(12,22,-3,-8,81,92).
ligacao(12,23,2,4,64,31).
ligacao(13,21,3,2,18,2).
ligacao(13,22,0,-3,43,26).
ligacao(13,23,5,9,31,20).
ligacao(14,21,2,6,16,13).
ligacao(14,22,6,-3,19,20).
ligacao(14,23,7,0,74,35).
ligacao(21,22,2,1,45,23).
ligacao(22,23,-2,3,18,26).


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