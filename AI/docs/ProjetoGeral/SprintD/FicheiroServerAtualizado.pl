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
:- dynamic ligacao/5. %informacao ligacao (id,id,força,força)
:- dynamic no/13. %informacao no (id,nome,listaTags)
:- dynamic melhor_sol_mais_segura/2.
:- dynamic grupo_maior/3.

% ----------------------------------------------------------------------------------------------------------: SERVIDOR :-------------------------------------------------------------------------------------------------------------.

% -------------------------------------------------------------------------------------------------------: GERIR SERVIDOR :---------------------------------------------------------------------------------------------------------.
startServer(Port) :-
    http_server(http_dispatch, [port(Port)]),
    importarInformacao(),
    importarInformacao1(),
    m1(),
    findall(X,ligacao(X,_,_,_,_),L),
    findall(Y,no(Y,_,_,_,_,_,_,_,_,_,_,_,_),Z),
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
        no(NAct, Act,_,_,_,_,_,_,_,_,_,_),
        ligacao(NAct, NX, F1, _,_),
        F1 >= M,
        no(NX, X, _,_,_,_,_,_,_,_,_,_,_),
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


percorrer(Palavra,Lista):-forall(no(Id,Nome,ListaUser,_,_,_,_,_,_,_,_,_,_),percorrerUser(Lista,ListaUser,Id,Nome,Palavra,ListaUser)).


percorrerUser(_,[],_,_,_,_):-!.
percorrerUser(Lista,[X|L],Id,Nome,Palavra,ListaUser):-member(X,Lista),delete(ListaUser,X,L1),L2=[Palavra],append(L1,L2,L3)
,retract(no(Id,Nome,_,_,_,_,_,_,_,_,_,_,_)),asserta(no(Id,Nome,L3,_,_,_,_,_,_,_,_,_,_));percorrerUser(Lista,L,Id,Nome,Palavra,ListaUser).

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

todas_combinacoes1(X,LTags,LcombXTags,ID,ListaUtilizadores,ListaNomesFinal,ListaConjuntaFinal):-no(ID,_,LTags,_,_,_,_,_,_,_,_,_,_),todas_combinacoes(X,LTags,LcombXTags),utilizadorTagsComuns(LcombXTags,[],ListaUtilizadores,ID),getNomes(ListaUtilizadores,[],ListaNomesFinal),join_lists(ListaUtilizadores,ListaNomesFinal,[],ListaConjuntaFinal).

join_lists([],[],ListaConjunta,ListaConjuntaFinal):-!,append([],ListaConjunta,ListaConjuntaFinal).
join_lists([X|L],[Y|T],ListaConjunta,ListaConjuntaFinal):- L1=[X],L2=[Y],append(L1,L2,L3),append(L3,ListaConjunta,L4),join_lists(L,T,L4,ListaConjuntaFinal).

getNomes([],ListaNomes,ListaNomesFinal):-!,append([],ListaNomes,ListaNomesFinal).
getNomes([X|L],ListaNomes,ListaNomesFinal):- no(X,Nome,_,_,_,_,_,_,_,_,_,_,_),List=[Nome],append(ListaNomes,List,L1),getNomes(L,L1,ListaNomesFinal).

todas_combinacoes(X,LTags,LcombXTags):-findall(L,combinacao(X,LTags,L),LcombXTags).
combinacao(0,_,[]):-!.
combinacao(X,[Tag|L],[Tag|T]):-X1 is X-1, combinacao(X1,L,T).
combinacao(X,[_|L],T):- combinacao(X,L,T).

utilizadorTagsComuns([],ListaUtilizadores,L6,_):-!,append([],ListaUtilizadores,L6).
utilizadorTagsComuns([X|L],ListaUtilizadores,L6,ID):-(no(IdUtilizador,_,ListaTags,_,_,_,_,_,_,_,_,_,_),IdUtilizador\=ID,not(ligacao(ID,IdUtilizador,_,_,_)),not(ligacao(IdUtilizador,ID,_,_,_)),verificaLista(X,ListaTags),not( member(IdUtilizador,ListaUtilizadores)),List=[IdUtilizador],append(ListaUtilizadores,List,L7),utilizadorTagsComuns([X|L],L7,L6,ID));(utilizadorTagsComuns(L,ListaUtilizadores,L6,ID)).

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

tamanho_rede_utilizadores_nivelY([UserX|ListaB],[ListaD|ListaC]):- findall(UserY,ligacao(UserX,UserY,_,_,_), ListaD),
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
        no(NAct, Act, _,_,_,_,_,_,_,_,_,_,_),                                                       % se existir no com o nome que se encontra em Act, prossegue
        ( ligacao(NAct, NX, F1, _,_); ligacao(NX, NAct, _, F1,_) ),                 % se existir ligacao NAct->NX ou NX -> NAct, prossegue
        no(NX, X, _,_,_,_,_,_,_,_,_,_,_),                                                           % se existir no ao qual Act está conectado, prossegue
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
dfs2(Act,Dest,LA,Cam):-no(NAct,Act,_,_,_,_,_,_,_,_,_,_,_),(ligacao(NAct,NX,_,_,_);ligacao(NX,NAct,_,_,_)),
    no(NX,X,_,_,_,_,_,_,_,_,_,_,_),\+ member(X,LA),dfs2(X,Dest,[X|LA],Cam).


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

% --------------------------------------------------------------------------------------------: BASE DE CONHECIMENTOS PROLOG :-------------------------------------------------------------------------------------------------.
% -----------------------------------------------------------------------------------------------------: Ligações :------------------------------------------------------------------------------------------------------------.

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
    H.get(forcaLigacaoSecundario),
    H.get(forcaRelacao))),
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
    H.get(listaTags),
    H.get(valorAngustiado),
    H.get(valorMedroso),
    H.get(valorDesapontado),
    H.get(valorComRemorsos),
    H.get(valorRaivoso),
    H.get(valorEsperancoso),
    H.get(valorAliviado),
    H.get(valorOrgulhoso),
    H.get(valorGrato),
    H.get(valorAlegria) )),
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

%---------------------------------------------------------------------------------: SPRINT D :--------------------------------------------------------------------------------------------

%-------------------------------------------------------------------------: ALGORITMO SUGERIR GRUPOS :-----------------------------------------------------------------------------------
:- http_handler('/prolog/sugerir_grupos',getGrupos,[]).

getGrupos(Request):-
    cors_enable(Request, [methods([get])]),
    http_parameters(Request,[nome(Nome,[]), numeroElementos(Numero,[]), numeroTags(Tags,[]), listaObrigatoria(Lista,[])]),
    atom_number(Numero,NElem),
    atom_number(Tags,NTags),
    split_string(Lista, ",", ",", List),
    algoritmo_sugerir_grupos(Nome,NElem,NTags,List),
    grupo_maior(Int,_,_),
    grupo_maior(_,_,Last),
    Result = [Int,Last],
    prolog_to_json(Result,JSONObject),
    reply_json(JSONObject, [json_object(dict)]).

algoritmo_sugerir_grupos(User,NElem,NTags,ListObrig):-
    asserta(grupo_maior([],-1,[])),
    findall(X,no(_,X,_,_,_,_,_,_,_,_,_,_,_),ListUsers),
    length(ListObrig,N),
    NTotal is NTags - N,
    no(_,User,ListTagsUser,_,_,_,_,_,_,_,_,_,_),
    diferenca(ListTagsUser,ListObrig,ListFinal),
    todas_combinacoes_tags(NTotal,ListFinal,ListObrig,CombinacoesTags),
    ver_grupos(CombinacoesTags,ListUsers,NElem).

diferenca_beta([ ],_,[ ]).
diferenca_beta([X|L1],L2,[X|LI]):-not(member(X,L2)),!,diferenca_beta(L1,L2,LI).
diferenca_beta([_|L1],L2,LI):-diferenca_beta(L1,L2,LI).

diferenca(L1,L2,L3):-diferenca_beta(L1,L2,L4),diferenca_beta(L2,L1,L5),uniao(L4,L5,L3).

uniao([ ],L,L).
uniao([X|L1],L2,LU):-member(X,L2),!,uniao(L1,L2,LU).
uniao([X|L1],L2,[X|LU]):-uniao(L1,L2,LU).

todas_combinacoes_tags(X,LTags,ListObrig,LcombXTags):-findall(L,combinacao(X,LTags,L,ListObrig),LcombXTags).

combinacao(0,_,Resultado,ListObrig):- Resultado = ListObrig,!.
combinacao(X,[Tag|L],[Tag|T],ListObrig):-X1 is X-1, combinacao(X1,L,T,ListObrig).
combinacao(X,[_|L],T,ListObrig):- combinacao(X,L,T,ListObrig).

ver_grupos([],_,_):-!.

ver_grupos([H|CombinacoesTags],ListUsers,NElem):-
    ListUsersAux = ListUsers,
    TagsComumGrupo = H,
    criar_grupo(ListUsersAux,TagsComumGrupo,[],Grupo),
    length(Grupo,TamanhoGrupo),
    TamanhoGrupo >= NElem,
    grupo_maior(_,Maior,_),
    TamanhoGrupo >= Maior,
    retract(grupo_maior(_,_,_)),asserta(grupo_maior(Grupo,TamanhoGrupo,H)),
    ver_grupos(CombinacoesTags,ListUsers,NElem),!.

ver_grupos([_|CombinacoesTags],ListUsers,NElem):-
    ver_grupos(CombinacoesTags,ListUsers,NElem).

criar_grupo([],_,Grupo,Resultado):- Resultado = Grupo,!.
criar_grupo([User|ListUsersAux],TagsComumGrupo,Grupo,Resultado):-
    no(_,User,ListaTags,_,_,_,_,_,_,_,_,_,_),
    tem_tags_comum(TagsComumGrupo,ListaTags),
    append_2(Grupo, [User], GruposNovos),
    criar_grupo(ListUsersAux, TagsComumGrupo, GruposNovos,Resultado),!.

criar_grupo([_|ListUsersAux],TagsComumGrupo,Grupo,Resultado):-
    criar_grupo(ListUsersAux, TagsComumGrupo, Grupo, Resultado),!.

tem_tags_comum(List1,List2):-
    forall(member(Element,List1), member(Element,List2)).

append_2([ ], Y, Y).
append_2([X|L1],L2,[X|L3]):-append_2(L1,L2,L3).


%-------------------------------------------------------------------------: ALGORITMOS MODELAÇÃO DE EMOÇÕES :-----------------------------------------------------------------------------------




atualizarAlegriaAngustia(IdUser,AL,AN,R1,R2):-getSomaForcasRelacao(IdUser,SomaFinal),no(IdUser,_,_,AN,_,_,_,_,_,_,_,_,AL),
(SomaFinal>0,(calcularAumento(SomaFinal,AL,2,R1),calcularDiminuicao(SomaFinal,AN,2,R2));(calcularAumento(SomaFinal,2,AN,R1),
calcularDiminuicao(SomaFinal,2,AL,R2))),!.

getSomaForcasRelacao(IdUser,SomaFinal):-getAllUsers(ListaUsers),getAllForcasRelacao(ListaForcasRelacao),
somarForcas(IdUser,ListaUsers,ListaForcasRelacao,0,SomaFinal).

getAllUsers(L):-findall(X1,ligacao(X1,_,_,_,_),L).
getAllForcasRelacao(L1):- findall(X1,ligacao(_,_,_,_,X1),L1).

somarForcas(_,[],[],Soma,SomaFinal):-SomaFinal is Soma.
somarForcas(IdUser,[H|T],[G|L],Soma,SomaFinal):-IdUser=H,Soma1 is Soma+G,somarForcas(IdUser,T,L,Soma1,SomaFinal);somarForcas(IdUser,T,L,Soma,SomaFinal).

calcularAumento(SomaFinal,AL,AN,R1):-AN=2,calcularAumento2(SomaFinal,AL,Resultado),R1 is Resultado;calcularAumento2(-SomaFinal,AN,Resultado),R1 is Resultado.

calcularDiminuicao(SomaFinal,AN,AL,R2):-AL=2,calcularDiminuicao2(SomaFinal,AN,Resultado),R2 is Resultado;calcularDiminuicao2(-SomaFinal,AL,Resultado),R2 is Resultado.

calcularAumento2(SomaFinal,Valor,Resultado):-Resultado is Valor+(1-Valor)*(min(SomaFinal,200)/200).

calcularDiminuicao2(SomaFinal,Valor,Resultado):-Resultado is Valor*(1-(min(SomaFinal,200)/200)).






atualizarEsperancaMedo(IdUser,GrupoSugerido,E,M,A,D,ResultadoEsperanca,ResultadoAlivio,ResultadoMedo,ResultadoDececao):-no(IdUser,_,_,_,M,D,_,_,E,A,_,_,_),
calcularEsperanca(GrupoSugerido,IdUser,E,D,ResultadoEsperanca,ResultadoDececao),
calcularMedo(GrupoSugerido,IdUser,M,A,ResultadoMedo,ResultadoAlivio).

calcularEsperanca(GrupoSugerido,IdUser,E,D,ResultadoEsperanca,ResultadoDececao):- esperanca(IdUser,ListaUEsperanca),
procurarUsersEmComum(GrupoSugerido,ListaUEsperanca,0,ResultadoFinal),
length(ListaUEsperanca,X),definirAumentoOuDiminuicaoEsperanca(X,ResultadoFinal,E,D,ResultadoEsperanca,ResultadoDececao).

calcularMedo(GrupoSugerido,IdUser,M,A,ResultadoMedo,ResultadoAlivio):-medo(IdUser,ListaUMedo),procurarUsersEmComum(GrupoSugerido,ListaUMedo,0,ResultadoFinal),
length(ListaUMedo,X),definirAumentoOuDiminuicaoMedo(X,ResultadoFinal,M,A,ResultadoMedo,ResultadoAlivio).

procurarUsersEmComum([],_,Count,ResultadoFinal):-!,ResultadoFinal is Count.
procurarUsersEmComum([H|T],ListaU,Count,ResultadoFinal):-procurarUsersEmComum2(H,ListaU,Count,Result),
procurarUsersEmComum(T,ListaU,Result,ResultadoFinal).

procurarUsersEmComum2(_,[],Count,Result):-!,Result is Count.
procurarUsersEmComum2(H,[X|Y],Count,Result):-H=X,Count1 is Count+1,procurarUsersEmComum2(H,[],Count1,Result);procurarUsersEmComum2(H,Y,Count,Result).

definirAumentoOuDiminuicaoEsperanca(X,ResultadoFinal,E,D,ResultadoEsperanca,ResultadoDececao):-(ResultadoFinal/X) = (1/2),ResultadoEsperanca is E,ResultadoDececao is D;
(ResultadoFinal/X) > (1/2),aumentoEmocao(X,ResultadoFinal,E,ResultadoEsperanca),diminuicaoEmocao(X,X-ResultadoFinal,D,ResultadoDececao);
diminuicaoEmocao(X,ResultadoFinal,E,ResultadoEsperanca),aumentoEmocao(X,X-ResultadoFinal,D,ResultadoDececao).

definirAumentoOuDiminuicaoMedo(X,ResultadoFinal,M,A,ResultadoMedo,ResultadoAlivio):-(ResultadoFinal/X) = (1/2),ResultadoMedo is M,ResultadoAlivio is A;(ResultadoFinal/X) > (1/2),
aumentoEmocao(X,ResultadoFinal,M,ResultadoMedo),diminuicaoEmocao(X,X-ResultadoFinal,A,ResultadoAlivio);diminuicaoEmocao(X,ResultadoFinal,M,ResultadoMedo),
aumentoEmocao(X,X-ResultadoFinal,A,ResultadoAlivio).

aumentoEmocao(X,ResultadoFinal,EmocaoValor,ResultadoAposMudanca):-ResultadoAposMudanca is EmocaoValor+(1-EmocaoValor)*(ResultadoFinal/X).
diminuicaoEmocao(X,ResultadoFinal,EmocaoValor,ResultadoAposMudanca):-ResultadoAposMudanca is EmocaoValor*(1-(ResultadoFinal/X)).


esperanca('1b316e04-454b-4d06-a528-64792b63a100',['1b316e04-454b-4d06-a528-64792b63a101','1b316e04-454b-4d06-a528-64792b63a102','1b316e04-454b-4d06-a528-64792b63a103']).
esperanca('1b316e04-454b-4d06-a528-64792b63a101',['1b316e04-454b-4d06-a528-64792b63a105','1b316e04-454b-4d06-a528-64792b63a106']).

medo('1b316e04-454b-4d06-a528-64792b63a100',['1b316e04-454b-4d06-a528-64792b63a105','1b316e04-454b-4d06-a528-64792b63a106','1b316e04-454b-4d06-a528-64792b63a104']).
medo('1b316e04-454b-4d06-a528-64792b63a101',['1b316e04-454b-4d06-a528-64792b63a103','1b316e04-454b-4d06-a528-64792b63a102']).





atualizarOrgulhoRemorso(IdUser,Grupo,ListaTagsGrupo,O,RE,G,RA,ResultadoOrgulho,ResultadoRemorso,ResultadoGratidao,ResultadoRaiva):-
    no(IdUser,_,_,_,_,_,RE,RA,_,_,O,G,_),calculaOrgulho(Grupo,IdUser,O,RA,ResultadoOrgulho,ResultadoRaiva,ListaTagsGrupo),
    calculaRemorso(Grupo,IdUser,RE,G,ResultadoRemorso,ResultadoGratidao,ListaTagsGrupo),!.


calculaOrgulho(Grupo,IdUser,O,RA,ResultadoOrgulho,ResultadoRaiva,ListaTagsGrupo):-findall(X,orgulho(IdUser,X,_),L),findall(X,orgulho(IdUser,_,X),L1),
percorrerListasOrgulho(L,L1,ListaTagsGrupo,Grupo,0,0,ResultadoTotalFinal,ResultadoTotalX)
,definirAumentoOuDiminuicaoOrgulho(ResultadoTotalX,ResultadoTotalFinal,O,RA,ResultadoOrgulho,ResultadoRaiva),!.

calculaRemorso(Grupo,IdUser,RE,G,ResultadoRemorso,ResultadoGratidao,ListaTagsGrupo):-findall(X,remorso(IdUser,X,_),L),findall(X,remorso(IdUser,_,X),L1),
percorrerListasRemorso(L,L1,ListaTagsGrupo,Grupo,0,0,ResultadoTotalFinal,ResultadoTotalX)
,definirAumentoOuDiminuicaoRemorso(ResultadoTotalX,ResultadoTotalFinal,RE,G,ResultadoRemorso,ResultadoGratidao),!.

percorrerListasRemorso([],[],_,_,ResultadoFinal,X,ResultadoTotalFinal,ResultadoTotalX):-ResultadoTotalFinal is ResultadoFinal,ResultadoTotalX is X,!.
percorrerListasRemorso([H|L],[P|T],ListaTagsGrupo,Grupo,ResultadoFinal,X,ResultadoTotalFinal,ResultadoTotalX):-
percorrerListasRemorso2(H,P,ListaTagsGrupo,Grupo,ResultadoFinalRetornado,XRetornado),ResultadoTotalFinal1 is ResultadoFinalRetornado+ResultadoFinal,
X1 is XRetornado+X,percorrerListasRemorso(L,T,ListaTagsGrupo,Grupo,ResultadoTotalFinal1,X1,ResultadoTotalFinal,ResultadoTotalX).

percorrerListasOrgulho([],[],_,_,ResultadoFinal,X,ResultadoTotalFinal,ResultadoTotalX):-
    ResultadoTotalFinal is ResultadoFinal,ResultadoTotalX is X,!.
percorrerListasOrgulho([H|L],[P|T],ListaTagsGrupo,Grupo,ResultadoFinal,X,ResultadoTotalFinal,ResultadoTotalX):-
percorrerListasOrgulho2(H,P,ListaTagsGrupo,Grupo,ResultadoFinalRetornado,XRetornado),
ResultadoTotalFinal1 is ResultadoFinalRetornado+ResultadoFinal,
X1 is XRetornado+X,percorrerListasOrgulho(L,T,ListaTagsGrupo,Grupo,ResultadoTotalFinal1,X1,ResultadoTotalFinal,ResultadoTotalX).

percorrerListasOrgulho2(ListaUOrgulho,Tag,ListaTagsGrupo,Grupo,ResultadoFinal,X):-member(Tag,ListaTagsGrupo),
procurarUsersEmComum(Grupo,ListaUOrgulho,0,ResultadoFinal),
length(ListaUOrgulho,X);ResultadoFinal is 0,X is 0.

percorrerListasRemorso2(ListaUOrgulho,Tag,ListaTagsGrupo,Grupo,ResultadoFinal,X):-member(Tag,ListaTagsGrupo),
procurarUsersEmComumRemorso(Grupo,ListaUOrgulho,0,ResultadoFinal),
length(ListaUOrgulho,X);ResultadoFinal is 0,X is 0.

procurarUsersEmComumRemorso(_,[],Count,ResultadoFinal):-!,ResultadoFinal is Count.
procurarUsersEmComumRemorso(Grupo,[H|T],Count,ResultadoFinal):-procurarUsersEmComumRemorso2(Grupo,H,Count,Result),
procurarUsersEmComumRemorso(Grupo,T,Result,ResultadoFinal).

procurarUsersEmComumRemorso2(Grupo,H,Count,Result):- \+ member(H,Grupo),Result is Count+1;Result is Count.


definirAumentoOuDiminuicaoOrgulho(X,ResultadoFinal,O,RA,ResultadoOrgulho,ResultadoRaiva):-(ResultadoFinal/X) = (1/2),ResultadoOrgulho is O,ResultadoRaiva is RA;
(ResultadoFinal/X) > (1/2),
aumentoEmocao(X,ResultadoFinal,O,ResultadoOrgulho),diminuicaoEmocao(X,X-ResultadoFinal,RA,ResultadoRaiva);diminuicaoEmocao(X,ResultadoFinal,O,ResultadoOrgulho),
aumentoEmocao(X,X-ResultadoFinal,RA,ResultadoRaiva).


definirAumentoOuDiminuicaoRemorso(X,ResultadoFinal,RE,G,ResultadoRemorso,ResultadoGratidao):-X=0,ResultadoRemorso is RE,ResultadoGratidao is G;
(ResultadoFinal/X) = (1/2),ResultadoRemorso is RE,ResultadoGratidao is G;(ResultadoFinal/X) > (1/2),
aumentoEmocao(X,ResultadoFinal,RE,ResultadoRemorso),diminuicaoEmocao(X,X-ResultadoFinal,G,ResultadoGratidao);diminuicaoEmocao(X,ResultadoFinal,RE,ResultadoRemorso),
aumentoEmocao(X,X-ResultadoFinal,G,ResultadoGratidao).


orgulho('1b316e04-454b-4d06-a528-64792b63a100',['1b316e04-454b-4d06-a528-64792b63a102','1b316e04-454b-4d06-a528-64792b63a103'],'lapr5').
orgulho('1b316e04-454b-4d06-a528-64792b63a100',['1b316e04-454b-4d06-a528-64792b63a105'],'nba').


remorso('1b316e04-454b-4d06-a528-64792b63a100',['1b316e04-454b-4d06-a528-64792b63a105','1b316e04-454b-4d06-a528-64792b63a106'],'nba').
remorso('1b316e04-454b-4d06-a528-64792b63a101',['1b316e04-454b-4d06-a528-64792b63a103','1b316e04-454b-4d06-a528-64792b63a102'],'lapr5').

%---------------------------------------------------------Algoritmo para caminho mais forte com emoções ------------------

:- http_handler('/prolog/caminhoMaisForteComEmocoes',getCaminhoMaisForteComEmocoes,[]).

getCaminhoMaisForteComEmocoes(Request):-
   cors_enable(Request, [methods([get])]),
   http_parameters(Request, [ from(From, []), to(To, []), value(Value, [])]),
   atom_number(Value,X),
   bestfs1_mc_one(From,To,LCAM,_,X),
   prolog_to_json(LCAM, JSONObject),
   reply_json(JSONObject, [json_object(dict)]).

% Algoritmo Best First (Com Função Multicritério, apenas um resultado)

bestfs1_mc_one(Orig,Dest,Cam,Custo,N):-
                bestfs12_mc_one(Dest,[[Orig]],Cam,Custo,N).

bestfs12_mc_one(Dest,[[Dest|T]|_],Cam,Custo,_):-
                reverse([Dest|T],Cam),
                calcula_custo_mc(Cam,Custo),
                !.

bestfs12_mc_one(Dest,[[Dest|_]|_],_,_,_):-
                !.

bestfs12_mc_one(Dest,LLA,Cam,Custo,N):-
                member1(LA,LLA,LLA1),LA=[Act|_],
                length(LA,Tamanho),
                Tamanho =< (N + 1),
                ((Act==Dest,!,bestfs12_mc_one(Dest,[LA|LLA1],Cam,Custo,N)) ; (findall((CX,[X|LA]),
                (no(NAct,Act,_,_,_,_,_,_,_,_,_,_,_),no(NX,X,_,NAngustiado,NMedroso,NDesapontado,NRemorsos,NNervoso,_,_,_,_,_),
                verifyEmotions(NAngustiado,NMedroso,NDesapontado,NRemorsos,NNervoso),
                (ligacao(NAct,NX,FL,_,FR);ligacao(NX,NAct,_,FL,FR)),CX is FL+FR,
                \+member(X,LA)),Novos),
                Novos\==[],!,
                sort(0,@>=,Novos,NovosOrd),
                retira_custos(NovosOrd,NovosOrd1),
                append(NovosOrd1,LLA1,LLA2),
                bestfs12_mc_one(Dest,LLA2,Cam,Custo,N))).

member1(LA,[LA|LAA],LAA).
member1(LA,[_|LAA],LAA1):- member1(LA,LAA,LAA1).

retira_custos([],[]).
retira_custos([(_,LA)|L],[LA|L1]):- retira_custos(L,L1).

calcula_custo_mc([Act,X],R):-!,
                 no(NAct,Act,_,_,_,_,_,_,_,_,_,_,_),
                 no(NX,X,_,_,_,_,_,_,_,_,_,_,_),
                 (ligacao(NAct,NX,FL,_,FR);ligacao(NX,NAct,_,FL,FR)),
                 multicriterio(FL, FR, R).

calcula_custo_mc([Act,X|L],P):-calcula_custo_mc([X|L],P1),
                 no(NAct,Act,_,_,_,_,_,_,_,_,_,_,_),
                 no(NX,X,_,_,_,_,_,_,_,_,_,_,_),
                 (ligacao(NAct,NX,FL,_,FR);ligacao(NX,NAct,_,FL,FR)),
                 multicriterio(FL, FR, R),
                 P is P1 + R.


% Verificação dos níveis de emoção

verifyEmotions(NAngustiado,NMedroso,NDesapontado,NRemorsos,NNervoso):-
        LVL is 0.5,
        NAngustiado =< LVL,
        NMedroso =< LVL,
        NDesapontado =< LVL,
        NRemorsos =< LVL,
        NNervoso =< LVL.

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
