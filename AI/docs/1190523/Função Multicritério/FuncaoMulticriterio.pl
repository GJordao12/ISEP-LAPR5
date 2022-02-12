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