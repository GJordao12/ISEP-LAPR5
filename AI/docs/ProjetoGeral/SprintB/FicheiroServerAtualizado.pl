% -------------------------------------------------------------------------------------------------------: BIBLIOTECAS :-----------------------------------------------------------------------------------------.

% Bibliotecas
:- use_module(library(http/thread_httpd)).
:- use_module(library(http/http_dispatch)).

%:- use_module(library(http/http_unix_daemon)).
:- use_module(library(http/http_parameters)).
:- use_module(library(http/http_open)).
:- use_module(library(http/http_cors)).
:- use_module(library(date)).
:- use_module(library(random)).

% Bibliotecas JSON
:- use_module(library(http/json_convert)).
:- use_module(library(http/http_json)).
:- use_module(library(http/json)).

:- json_object agenda_maq_json_array(array:list(agenda_maq_json)).
:- json_object agenda_maq_json(maquina:string,agenda_array:list(agenda_json)).
:- json_object agenda_json(instanteInicial:float,instanteFinal:float,tipoProcessamento:string,lista:list(string)).

% Funções do Servidor
:- set_setting(http:cors, [*]).

%URLS
connections_url("https://localhost:5001/api/Ligacao/Ligacoes/Prolog").
connections_url1("https://localhost:5001/api/UtilizadorObjetivo").

%% Base Conhecimento principal
:- dynamic ligacao/4. %informacao ligacao (id,id,força,força)
:- dynamic no/3. %informacao no (id,nome,listaTags)
:- dynamic melhor_sol_mais_segura/2.

% ----------------------------------------------------------------------------------------------------------: SERVIDOR :-------------------------------------------------------------------------------------------------------------.

% -------------------------------------------------------------------------------------------------------: GERIR SERVIDOR :---------------------------------------------------------------------------------------------------------.
startServer(Port) :-
    http_server(http_dispatch, [port(Port)]),
    importarInformacao(),
    importarInformacao1(),
    m1(),
    findall(X,ligacao(X,_,_,_),L),
    findall(Y,no(Y,_,_),Z),
    write(Z),
    write(L),
    asserta(port(Port)).

stopServer:-
    retract(port(Port)),
    http_stop_server(Port,_).

:- initialization(startServer(5001)).

% --------------------------------------------------------------------------------------------------------: PREDICADOS :-----------------------------------------------------------------------------------------------------.

% ----------------------------------------------------------------------------------------------------: CAMINHO MAIS SEGURO :------------------------------------------------------------------------------------------------.

caminho_mais_seguro(Orig, Dest, M, CaminhoMaisSeguro):-
        (melhor_caminho_mais_seguro(Orig, Dest,M) ; true),
        retract(melhor_sol_mais_segura(CaminhoMaisSeguro, _)).

melhor_caminho_mais_seguro(Orig, Dest,M) :-
        asserta(melhor_sol_mais_segura(_, -1000)),
        dfsMaisSeguro(Orig, Dest, LCaminho, Forca,M),
        atualiza_melhor_mais_seguro(LCaminho, Forca),
        fail.

atualiza_melhor_mais_seguro(LCaminho, Forca) :-
        melhor_sol_mais_segura(_, F),
        Forca > F, retract(melhor_sol_mais_segura(_,_)),
        asserta(melhor_sol_mais_segura(LCaminho, Forca)).

dfsMaisSeguro(Orig, Dest, Cam, Forca, M) :- dfsMaisSeguro2(Orig, Dest, [Orig], Cam, Forca, M).

dfsMaisSeguro2(Dest, Dest, LA, Cam, 0, _) :- !, reverse(LA, Cam).

dfsMaisSeguro2(Act, Dest, LA, Cam, Forca, M) :-
        no(NAct, Act, _),
        ligacao(NAct, NX, F1, _),
        F1 >= M,
        no(NX, X, _),
        \+ member(X, LA),
        dfsMaisSeguro2(X, Dest, [X|LA], Cam, FX, M),
        Forca is (FX + F1).

:-http_handler('/prolog/caminho_mais_seguro',get_caminho_mais_seguro,[]).
   get_caminho_mais_seguro(Request):-
   cors_enable(Request, [methods([get])]),
   http_parameters(Request, [ from(From, []), to(To, []), value(Value, [])]),
   atom_number(Value,X),
   caminho_mais_seguro(From,To,X,LCAM),
   prolog_to_json(LCAM, JSONObject),
   reply_json(JSONObject, [json_object(dict)]).

% ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------.

:- http_handler('/prolog/utilizador_objetivo',getUtilizadorObjetivo,[]).

m1():-forall(sinonimo(Palavra,Lista),percorrer(Palavra,Lista)).


percorrer(Palavra,Lista):-forall(no(Id,Nome,ListaUser),percorrerUser(Lista,ListaUser,Id,Nome,Palavra,ListaUser)).


percorrerUser(_,[],_,_,_,_):-!.
percorrerUser(Lista,[X|L],Id,Nome,Palavra,ListaUser):-member(X,Lista),delete(ListaUser,X,L1),L2=[Palavra],append(L1,L2,L3)
,retract(no(Id,Nome,_)),asserta(no(Id,Nome,L3));percorrerUser(Lista,L,Id,Nome,Palavra,ListaUser).

getUtilizadorObjetivo(Request):-
cors_enable(Request, [methods([get])]),
 http_parameters(Request,
                    [ tag(Tag, [between(1,100)]),
                      id(Id, [])
                    ]),
    term_to_atom(X, Id),
    todas_combinacoes1(Tag,_,_,X,_,_,ListaConjuntaFinal),
    prolog_to_json(ListaConjuntaFinal, JSONObject),
    reply_json(JSONObject, [json_object(dict)]).

todas_combinacoes1(X,LTags,LcombXTags,ID,ListaUtilizadores,ListaNomesFinal,ListaConjuntaFinal):-no(ID,_,LTags),todas_combinacoes(X,LTags,LcombXTags),utilizadorTagsComuns(LcombXTags,[],ListaUtilizadores,ID),getNomes(ListaUtilizadores,[],ListaNomesFinal),join_lists(ListaUtilizadores,ListaNomesFinal,[],ListaConjuntaFinal).

join_lists([],[],ListaConjunta,ListaConjuntaFinal):-!,append([],ListaConjunta,ListaConjuntaFinal).
join_lists([X|L],[Y|T],ListaConjunta,ListaConjuntaFinal):- L1=[X],L2=[Y],append(L1,L2,L3),append(L3,ListaConjunta,L4),join_lists(L,T,L4,ListaConjuntaFinal).

getNomes([],ListaNomes,ListaNomesFinal):-!,append([],ListaNomes,ListaNomesFinal).
getNomes([X|L],ListaNomes,ListaNomesFinal):- no(X,Nome,_),List=[Nome],append(ListaNomes,List,L1),getNomes(L,L1,ListaNomesFinal).

todas_combinacoes(X,LTags,LcombXTags):-findall(L,combinacao(X,LTags,L),LcombXTags).
combinacao(0,_,[]):-!.
combinacao(X,[Tag|L],[Tag|T]):-X1 is X-1, combinacao(X1,L,T).
combinacao(X,[_|L],T):- combinacao(X,L,T).

utilizadorTagsComuns([],ListaUtilizadores,L6,_):-!,append([],ListaUtilizadores,L6).
utilizadorTagsComuns([X|L],ListaUtilizadores,L6,ID):-(no(IdUtilizador,_,ListaTags),IdUtilizador\=ID,not(ligacao(ID,IdUtilizador,_,_)),not(ligacao(IdUtilizador,ID,_,_)),verificaLista(X,ListaTags),not( member(IdUtilizador,ListaUtilizadores)),List=[IdUtilizador],append(ListaUtilizadores,List,L7),utilizadorTagsComuns([X|L],L7,L6,ID));(utilizadorTagsComuns(L,ListaUtilizadores,L6,ID)).

verificaLista([],_):-true.
verificaLista([X|L],L2):- member(X,L2),verificaLista(L,L2).
verificaLista(_,_):-false.

% --------------------------------------------------------------------------------------------: TAMANHO DA REDE DE UTILIZADORES :----------------------------------------------------------------------------------------.
:- http_handler('/prolog/tamanho_rede',getTamanho,[]).

:- http_handler('/prolog/base_conhecimento',atualizarTudoPrimeiro,[]).

getTamanho(Request):-
    cors_enable(Request, [methods([get])]),
    http_parameters(Request, [ id(Id, []), nivel(Nivel, [])]),
    atom_number(Nivel,X),
    tamanho_rede_utilizadores(Id,X,Resultado),
    prolog_to_json(Resultado,JSONObject),
    reply_json(JSONObject, [json_object(dict)]).

tamanho_rede_utilizadores(Origem,Nivel,Resultado):- tamanho_rede_utilizadores_nivelX([Origem],Nivel,ListaA),
                                                    flatten(ListaA,ListaB),
                                                    sort(ListaB,ListaE),
                                                    append([],ListaE,Solucao),
                                                    ((member(Origem,Solucao),delete(Solucao,Origem,Resultado)) ; (Resultado = Solucao)),!.

tamanho_rede_utilizadores_nivelX(_,0,[]):-!.

tamanho_rede_utilizadores_nivelX(ListaB,Nivel,[ListaC|ListaA]):- NY is Nivel - 1,
                                                                 tamanho_rede_utilizadores_nivelY(ListaB,ListaI),
                                                                 flatten(ListaI,ListaC),
                                                                 tamanho_rede_utilizadores_nivelX(ListaC,NY,ListaA).
tamanho_rede_utilizadores_nivelY([],[]):-!.

tamanho_rede_utilizadores_nivelY([UserX|ListaB],[ListaD|ListaC]):- findall(UserY,ligacao(UserX,UserY,_,_), ListaD),
                                                                   tamanho_rede_utilizadores_nivelY(ListaB,ListaC).

% --------------------------------------------------------------------------------------------: CAMINHO MAIS FORTE :----------------------------------------------------------------------------------------------------.

:- http_handler('/prolog/caminho_mais_forte', get_caminho_mais_forte,[]).

get_caminho_mais_forte(Request):-
   cors_enable(Request, [methods([get])]),
   http_parameters(Request, [nome1(Nome1, []), nome2(Nome2, [])]),
   plan_maisForteUniLig(Nome1,Nome2,LCAM),
   prolog_to_json(LCAM, JSONObject),
   reply_json(JSONObject, [json_object(dict)]).

:- dynamic melhor_sol_maisForteUniLig/2.
:- dynamic contagem_solucoes_uni/1.


plan_maisForteUniLig(Orig, Dest, LCaminho_maisForteLig) :-
        (melhor_caminho_maisForteUniLig(Orig, Dest) ; true),                    % chamada ao metodo melhor_caminho_strongerLig, sendo que caso este dê false, como temos "; true", será sempre feito o que vem a seguir
        retract(melhor_sol_maisForteUniLig(LCaminho_maisForteLig, _)).     % remocao da melhor solucao



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


%----------------------------------------------------------------------------:Caminho Mais Curto:--------------------------------------------

:- http_handler('/prolog/caminhoMaisCurto',getCaminhoMaisCurto,[]).

getCaminhoMaisCurto(Request):-
cors_enable(Request, [methods([get])]),
 http_parameters(Request,
                    [ nome(Nome, []),
                      nome1(Nome1, [])
                    ]),
    plan_minlig(Nome,Nome1,LCaminho_minlig),
    prolog_to_json(LCaminho_minlig, JSONObject),
    reply_json(JSONObject, [json_object(dict)]).

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

% ---------------------------------------------------------------------------------------------: BASE DE CONHECIMENTOS PROLOG :-------------------------------------------------------------------------------------------------.
% ------------------------------------------------------------------------------------------------------: Ligações :------------------------------------------------------------------------------------------------------------.

processarLigacoes(Data):-
    connections_url(URL),
    setup_call_cleanup(
        http_open(URL, In, [cert_verify_hook(cert_accept_any),request_header('Accept'='application/json')]),
        json_read_dict(In, Data),
        close(In)
    ).

parse_ligacoes([]).
parse_ligacoes([H|Data]):-
    atom_string(Principal,H.get(principal)),
    atom_string(Secundario,H.get(secundario)),
    asserta(ligacao(
    Principal,
    Secundario,
    H.get(forcaLigacaoPrincipal),
    H.get(forcaLigacaoSecundario))),
    parse_ligacoes(Data).

adicionarLigacoes():-
    processarLigacoes(Data),
    parse_ligacoes(Data).

importarInformacao():-
    adicionarLigacoes().

apagarInformacao():-
    retractall(ligacao).

atualizarLigacoes():-
    retractall(ligacao),
    adicionarLigacoes().

atualizarTudoPrimeiro(_Request):-
    atualizarTudo(),
    format('Content-type: text/plain~n~n'),
    format('Updated').

atualizarTudo():-
    apagarData(),
    importarData().

importarData():-
    atualizarLigacoes(),
    atualizarNos().

apagarData():-
    apagarInformacao(),
    apagarInformacao1().

% -------------------------------------------------------------------------------------------------------------------------------------------------------------

processarNos(Data):-
    connections_url1(URL),
    setup_call_cleanup(
        http_open(URL, In, [cert_verify_hook(cert_accept_any),request_header('Accept'='application/json')]),
        json_read_dict(In, Data),
        close(In)
    ).

parse_nos([]).
parse_nos([H|Data]):-
    atom_string(IdUser,H.get(id)),
    atom_string(Username,H.get(nome)),
    asserta(no(
    IdUser,
    Username,
     H.get(listaTags))),
    parse_nos(Data).

adicionarNos():-
    processarNos(Data),
    parse_nos(Data).

importarInformacao1():-
    adicionarNos().

apagarInformacao1():-
    retractall(no).

atualizarNos():-
    retractall(no),
    adicionarNos().

sinonimo(t4d,[ts,typescript]).
sinonimo(t3c,[csharp]).
sinonimo(t1a,[javascript,js]).
sinonimo(t2b,[cplusplus]).
