# Determinar O Caminho Mais Curto
====================================================

# Problema

* **Descrição:** Determinar o **caminho mais curto**.

# Predicados

## ***Caminho mais Curto sem findall***
```
:-dynamic melhor_sol_minlig/2.

dfs(Orig,Dest,Cam):-dfs2(Orig,Dest,[Orig],Cam).

dfs2(Dest,Dest,LA,Cam):-!,reverse(LA,Cam).
dfs2(Act,Dest,LA,Cam):-no(NAct,Act,_),(ligacao(NAct,NX,_,_);ligacao(NX,NAct,_,_)),
    no(NX,X,_),\+ member(X,LA),dfs2(X,Dest,[X|LA],Cam).


plan_minlig(Orig,Dest,LCaminho_minlig):-
		get_time(Ti),
		(melhor_caminho_minlig(Orig,Dest);true),
		retract(melhor_sol_minlig(LCaminho_minlig,_)),
		get_time(Tf),
		T is Tf-Ti,
		write('Tempo de geracao da solucao:'),write(T),nl.

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

```

* **Explicação:** Primeiramente é usado o plan_minlig/3 que vai contar o tempo de execução do algoritmo e vai chamar o melhor_caminho_minlig/2, este por sua vez vai encontrar o caminho mais curto e para isso vai utilizar o dfs/3 para fazer a busca em profundidade do caminho atualizando o caminho se este for mais curto que o anteriormente encontrado.

## ***Caminho mais Curto com findall***

```
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
```

* **Explicação:** Primeiramente são encontrados todos os caminhos através do findall e de seguida é encontrado o caminho mais pequeno.


# Ficheiro Base de Conhecimentos

* O Ficheiro da Base de Conhecimentos relativo ao **Determinar O Caminho Mais Curto** é o [CaminhoMaisCurto](CaminhoMaisCurto.pl)
