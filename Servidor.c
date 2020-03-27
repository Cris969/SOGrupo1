#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>

//Estructuras
typedef struct{
	//Nombre del ataque 
	char nombre[20];
	//Daño del ataque 
	int damage;
	//Informacion del ataque 
	char info[200];
	//Tipo del ataque1
	char tipo[20];
}Tataque;
typedef struct{
	//Nombre del pokemon 
	char nombre[20];
	//Un pokemon solo puede tener 4 ataques
	Tataque ataques[4];
	//Tipo del pokemon 
	char tipo[20];
}Tpokemon;
//Jugadores conectados
typedef struct{
	//Apodo del jugador conectado
	char apodo[10];
	//Nivel del jugador 
	char nivel[20];
	//Socket 
	int socket;
	//Equipo formado por 2 pokemon del jugador
	Tpokemon equipo[2];
}Tjugador;
typedef struct{
	//Lista de jugadores conectados
	Tjugador jugadores[100];
	//Numero de jugadores conectados 
	int numerojugadores;
}Tlistajugadores;
//En la primera version solo 1 combate a la vez
typedef struct{
	//En el combate participan solo 2 jugadores
	Tjugador jugador1;
	Tjugador jugador2;
	//Ganador del combate
	char ganador[10];
	//En el combate participaran 2 pokemon de cada jugador
	Tpokemon pokemons[2];
	//Puntuacion jugador 1
	int puntuacion1;
	//Puntuacion jugador 2
	int puntuacion2;
	//Identificador del combate
	int id;
}Combate;

//Variables globales 
	MYSQL *conn;
	
//Consulta mayor puntuacion de un jugador 
int Mayor_puntuacion_jugador(char apodo[10]){

	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char consulta[200];

	//Cambiar por sprintf
	strcpy(consulta,"SELECT puntuacion.puntuacion FROM puntuacion,jugador,combate WHERE jugador.apodo='");
	strcat(consulta,apodo);
	strcat(consulta,"' AND puntuacion.jugador='");
	strcat(consulta,apodo);
	strcat(consulta,"' AND puntuacion.combate = combate.id ORDER BY puntuacion.puntuacion DESC");
	err=mysql_query (conn,consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL)
		return -1;
	else{
		int puntuacion = atoi(row[0]);
		return puntuacion;
	}	
}
//Datos de un combate
int Datos_combate(int id, char ganador[20], char jugador1[20],char jugador2[20],int *puntuacion1,int *puntuacion2){
		
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char consulta[300];
	
	sprintf(consulta,"SELECT combate.ganador,puntuacion.jugador,puntuacion.puntuacion FROM combate,puntuacion WHERE combate.id = %d AND puntuacion.combate = %d",id,id);
	err = mysql_query(conn,consulta);
	if(err!=0){
		printf("Error al realizar la consulta %u %s\n", mysql_errno(conn),mysql_error(conn));
		exit(1);
	}
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	if(row==NULL){
		printf("No se han recogido datos de la consulta\n");
		return -1;
	}
	else{
		strcpy(ganador,row[0]);
		strcpy(jugador1,row[1]);
		*puntuacion1 = atoi(row[2]);
		row = mysql_fetch_row(resultado);
		strcpy(jugador2,row[1]);
		*puntuacion2 = atoi(row[2]);
		return 1;
	}
}
//Numero de partidas ganadas por un jugador
int Numero_partidas_ganadas(char apodo[20]){
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char consulta[200];
	
	//Cambiar por sprintf
	//Miramos si existe el jugador
	strcpy(consulta,"SELECT apodo FROM jugador WHERE apodo ='");
	strcat(consulta,apodo);
	strcat(consulta,"'");
	err = mysql_query(conn,consulta);
	if(err!=0){
		printf("Error al realizar la consulta %u %s\n", mysql_errno(conn),mysql_error(conn));
		exit(1);
	}
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	if(row == NULL){
		printf("No se han recogido datos de la consulta");
		return -1;
	}
	//Miramos el numero de partidas ganadas
	else{
		strcpy(consulta,"SELECT ganador FROM combate WHERE ganador='");
		strcat(consulta,apodo);
		strcat(consulta,"'");
		err=mysql_query (conn, consulta);
		if(err!=0){
			printf("Error al realizar la consulta %u %s\n", mysql_errno(conn),mysql_error(conn));
			exit(1);
		}
		//Recogida del resultado de la consulta
		int cont=0;
		resultado=mysql_store_result(conn);
		row=mysql_fetch_row(resultado);
		if(row==NULL){
			//No ha ganado ninguna partida
			return 0;
		}
		else{
			while(row!=NULL){
				cont++;
				row=mysql_fetch_row(resultado);
			}
			return cont;
		}
	}
}
//Registrar jugador
int Registrar_jugador(char nombre[10],char pass[10],char apodo[20]){
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char consulta[200];
	
	strcpy(consulta,"SELECT nombrecuenta,pass,apodo FROM jugador WHERE nombrecuenta='");
	strcat(consulta,nombre);
	strcat(consulta,"' AND pass='");
	strcat(consulta,pass);
	strcat(consulta,"' AND apodo='");
	strcat(consulta,apodo);
	strcat(consulta,"'");
	err= mysql_query(conn,consulta);
	if(err!=0){
		printf("Error al realizar la consulta %u %s\n", mysql_errno(conn),mysql_error(conn));
		exit(1);
	}
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	if(row == NULL){
		//No existe el jugador
		return 1;
	}
	else{
		//Existe el jugador
		return -1;
	}
}
//Logear jugador
int Logear_jugador(char nombre[10],char pass[10], char apodo[20]){
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char consulta[200];
	
	strcpy(consulta,"SELECT nombrecuenta,pass,apodo FROM jugador WHERE nombrecuenta='");
	strcat(consulta,nombre);
	strcat(consulta,"' AND pass='");
	strcat(consulta,pass);
	strcat(consulta,"'");
	err= mysql_query(conn,consulta);
	if(err!=0){
		printf("Error al realizar la consulta %u %s\n", mysql_errno(conn),mysql_error(conn));
		exit(1);
	}
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	if(row == NULL){
		//No existe el jugador
		return -1;
	}
	else{
		strcpy(apodo,row[2]);
		//Existe el jugador
		return 1;
	}
}
int main(int argc, char *argv[])
{
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
	
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexion, indicando nuestras claves de acceso
	// al servidor de bases de datos (user,pass)
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "juego", 0, NULL, 0);
	if (conn==NULL)
	{
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	// INICIALIZACIONES
	//Abrimos el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creando socket");
	// Hacemos el bind en el port
	memset(&serv_adr, 0, sizeof(serv_adr));// inicializa a cero serv_addr
	serv_adr.sin_family = AF_INET;
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY); /* Lo mete en IP local */
	serv_adr.sin_port = htons(9050);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf("Error en el bind");
	// Limitamos el numero de conexiones pendientes
	if (listen(sock_listen, 10) < 0)
		printf("Error en el listen");
	for(;;){
		printf("Escuchando\n");
		sock_conn = accept(sock_listen, NULL, NULL);
		//sock_conn es el socket que utilizaremos para el cliente
		//Servicio
		//read hace 2 cosas , guarda lo que llega en el socket en el buffer peticion
		//y devuelve el numero de Bytes del mensaje , es decir su tamaÃ±o
		//el tamaño de un char es de 1 Byte , o lo que es lo mismo 8 bits
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf("Vamos bien\n");
		//Añadimos el caracter de fin de string para que no escriba lo que hay despues en el buffer
		peticion[ret] = '\0';
		//Vamos a ver que quieren(analizar la peticion)
		char *p = strtok(peticion,"/");
		int codigo = atoi(p);
		switch(codigo){
			//Login
			case 0:
				p = strtok(NULL,"/");
				char apodo[20];
				char nombre[10];
				char pass[10];
				strcpy(nombre,p);
				p = strtok(NULL,"/");
				strcpy(pass,p);
				int resultado = Logear_jugador(nombre,pass,apodo);
				if(resultado == 1){
					sprintf(respuesta,"%d/%s",resultado,apodo);
				}
				else{
					sprintf(respuesta,"%d",resultado);
				}
				write(sock_conn,respuesta,strlen(respuesta));
				break;
			//Consulta Cristian
			case 1:
				p = strtok(NULL,"/");
				apodo[20];
				strcpy(apodo,p);
				sprintf(respuesta,"%d",Mayor_puntuacion_jugador(apodo));
				write(sock_conn,respuesta,strlen(respuesta));
				break;
			//Consulta Diego	
			case 2:
				p = strtok(NULL,"/");
				apodo[20];
				strcpy(apodo,p);
				sprintf(respuesta,"%d",Numero_partidas_ganadas(apodo));
				write(sock_conn,respuesta,strlen(respuesta));
				break;
			//Consulta Joel	
			case 3:
				p = strtok(NULL,"/");
				int id = atoi(p);
				char ganador[20],jugador1[20],jugador2[20];
				int puntuacion1,puntuacion2;
				resultado = Datos_combate(id,ganador,jugador1,jugador2,&puntuacion1,&puntuacion2);
				if(resultado == -1){
					sprintf(respuesta,"%d",resultado);
					write(sock_conn,respuesta,strlen(respuesta));
				}
				else{
					sprintf(respuesta,"%d/%s/%s/%d/%s/%d",resultado,ganador,jugador1,puntuacion1,jugador2,puntuacion2);
					write(sock_conn,respuesta,strlen(respuesta));
				}
				break;
			//Registrarse
			case 4:
				p = strtok(NULL,"/");
				apodo[20];
				nombre[10];
				pass[10];
				strcpy(nombre,p);
				p = strtok(NULL,"/");
				strcpy(pass,p);
				p = strtok(NULL,"/");
				strcpy(apodo,p);
				resultado = Registrar_jugador(nombre,pass,apodo);
				sprintf(respuesta,"%d",resultado);
				write(sock_conn,respuesta,strlen(respuesta));
				break;
		    default:
				printf("Error en el codigo");
				break;
		}
		close(sock_conn); /* Necessari per a que el client detecti EOF */
	}
	//Cerramos la conexion con la base de datos
	mysql_close(conn);
	return 0;

}
