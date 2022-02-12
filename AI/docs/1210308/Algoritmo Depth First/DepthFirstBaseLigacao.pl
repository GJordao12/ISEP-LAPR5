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
    ligacao(U1,U2,ForcaU1_U2,ForcaU2_U1),
   F is ForcaU1_U2 + ForcaU2_U1.
   