DROP DATABASE IF EXISTS juego;
CREATE DATABASE juego;

USE juego;

CREATE TABLE ataque(

id INTEGER,
nombre TEXT,
usos INTEGER,
daño INTEGER,
tipo TEXT,
PRIMARY KEY(id)

)ENGINE= InnoDB;

INSERT INTO ataque VALUES(1,'Impactrueno',20,30,'Electrico');
INSERT INTO ataque VALUES(2,'Rayo',10,50,'Electrico');
INSERT INTO ataque VALUES(3,'Trueno',5,80,'Electrico');
INSERT INTO ataque VALUES(4,'Ataque rapido',20,30,'Normal');

CREATE TABLE pokemon(

id INTEGER,
nombre VARCHAR(10),
tipo TEXT,
nivel INTEGER,
vida INTEGER,
ataquefisico INTEGER,
ataque1 INTEGER,
ataque2 INTEGER,
ataque3 INTEGER,
ataque4 INTEGER,
PRIMARY KEY(id),
FOREIGN KEY(ataque1) REFERENCES ataque(id),
FOREIGN KEY(ataque1) REFERENCES ataque(id),
FOREIGN KEY(ataque3) REFERENCES ataque(id),
FOREIGN KEY(ataque4) REFERENCES ataque(id)

)ENGINE= InnoDB;

INSERT INTO pokemon VALUES(1,'Pikachu','Electrico',20,30,40,1,2,3,4);
INSERT INTO pokemon VALUES(2,'Pikachu','Electrico',20,30,40,1,2,3,4);

CREATE TABLE jugador(

nombrecuenta VARCHAR(10),
pass   VARCHAR(10),
apodo  VARCHAR(20),
nivel  TEXT,
pokemon1 INTEGER,
pokemon2 INTEGER,
PRIMARY KEY(apodo)

)ENGINE= InnoDB;

INSERT INTO jugador VALUES('Cristian','cristian','Cris969','Maestro',1,2);
INSERT INTO jugador VALUES('Joel','joel','Pachm','Bronce',1,2);
INSERT INTO jugador VALUES('Diego','diego','Diego96','Diamante',1,2);

CREATE TABLE combate(

id INTEGER,
ganador VARCHAR(10),
PRIMARY KEY(id)

)ENGINE= InnoDB;

INSERT INTO combate VALUES(1,'Cris969');
INSERT INTO combate VALUES(2,'Cris969');

CREATE TABLE puntuacion(

puntuacion INTEGER,
jugador VARCHAR(10),
combate INTEGER,
pokemon1 INTEGER,
pokemon2 INTEGER,
FOREIGN KEY(pokemon1) REFERENCES pokemon(id),
FOREIGN KEY(pokemon2) REFERENCES pokemon(id),
FOREIGN KEY (jugador) REFERENCES jugador(apodo),
FOREIGN KEY (combate) REFERENCES combate(id)

)ENGINE= InnoDB;

INSERT INTO puntuacion VALUES(3000,'Cris969',1,1,2);
INSERT INTO puntuacion VALUES(2000,'Cris969',2,1,2);
INSERT INTO puntuacion VALUES(1000,'Pachm',1,1,2);
INSERT INTO puntuacion VALUES(1000,'Diego.rdto',2,1,2);
