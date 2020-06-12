DROP DATABASE IF EXISTS juego;
CREATE DATABASE juego;

USE juego;

CREATE TABLE jugador(

nombrecuenta VARCHAR(10),
pass   VARCHAR(10),
apodo  VARCHAR(20),
PRIMARY KEY(apodo)

)ENGINE= InnoDB;

INSERT INTO jugador VALUES('Cristian','cristian','Cris969');
INSERT INTO jugador VALUES('Joel','joel','Pachm');
INSERT INTO jugador VALUES('Diego','diego','Diego96');
INSERT INTO jugador VALUES('Miguel', 'compasion', 'Trabajamos_duro');

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

FOREIGN KEY (jugador) REFERENCES jugador(apodo),
FOREIGN KEY (combate) REFERENCES combate(id)

)ENGINE= InnoDB;

INSERT INTO puntuacion VALUES(3000,'Cris969',2);
INSERT INTO puntuacion VALUES(2000,'Cris969',2);
INSERT INTO puntuacion VALUES(1000,'Pachm',2);
INSERT INTO puntuacion VALUES(1000,'Diego.rdto',2);
