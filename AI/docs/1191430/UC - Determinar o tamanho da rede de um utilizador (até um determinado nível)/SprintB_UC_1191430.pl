%UC - DETERMINAR TAMANHO DA REDE DE UM UTILIZADOR (ATÉ UM DETERMINADO NÍVEL)

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



tamanho_rede_utilizadores(Origem,Nivel,Solucao):- tamanho_rede_utilizadores_nivelX([Origem],Nivel,ListaA),
                                                  append(ListaA,ListaB),                                                        % Responsável pela linearização da lista A, isto é, concebe uma única lista apartir de um agregrado de listas.
                                                  sort(ListaB,ListaE),                                                          % Retira os duplicados da Lista B, e adiciona na lista E.
                                                  length(ListaE,Solucao).                                                       % Determina o tamanho da listaE.
tamanho_rede_utilizadores_nivelX(_,0,[]):-!.                                                                                    % Condição de paragem, quando chegar a 0 quer dizer que passou por todos os níveis desejados.

tamanho_rede_utilizadores_nivelX(ListaB,Nivel,[ListaC|ListaA]):- NY is Nivel - 1,                                               % Decrementa o nível recebido até 0. ListaA: Cauda da lista de retorno.
                                                                 tamanho_rede_utilizadores_nivelY(ListaB,ListaI),               % ListaI: Lista Intermediária
                                                                 append(ListaI,ListaC),                                         % Faz a lineariazação da lista intermediária para a lista C
                                                                 tamanho_rede_utilizadores_nivelX(ListaC,NY,ListaA).
tamanho_rede_utilizadores_nivelY([],[]):-!.                                                                                     % Condição de paragem, até a lista ficar vazia.

tamanho_rede_utilizadores_nivelY([UserX|ListaB],[ListaD|ListaC]):- findall(UserY,ligacao(UserX,UserY,_,_), ListaD),             % FindAll - Verifica todas as ocorrências e coloca-as na lista D.
                                                                   tamanho_rede_utilizadores_nivelY(ListaB,ListaC).             % Pega no utilizador da primeira lista e verifica todas as suas ligações,
                                                                                                                                % após isso, põe-nos na lista D, que que vai ser adicionada a uma lista,
                                                                                                                                % composta pelo seu header (Lista D) e cauda (Lista C: Inicialmente vazia).
                                                                                                                                % Isto irá se repetir até que todos os utilizadores provenientes da lista B
                                                                                                                                % sejam precorridos.

