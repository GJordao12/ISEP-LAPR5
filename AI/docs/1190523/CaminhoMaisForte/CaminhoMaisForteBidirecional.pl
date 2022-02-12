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
