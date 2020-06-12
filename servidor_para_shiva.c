#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>
#include <netinet/tcp.h>

////Estructuras////
//
typedef struct{ //Ficha
	int pos_ficha;
	int estado_ficha;
}Tficha;
typedef struct{ //Usuario chat
	char apodo [20];
	int socket;
}Tusuario;
typedef struct{ //Jugador
	char apodo[20];
	int socket;
	int color;
	Tficha fichas[4]; //Vector de 4 fichas para la partida
}Tjugador;
typedef struct{ //Lista de conectados
	Tusuario conectados[100];
	int numeroconectados;
}Tlistaconectados;
typedef struct{ //Chat
	Tusuario usuarios [4];
	int numUsuarios;
	int numInvitaciones;
	int estado;
}TChat;
typedef struct{ //Partida
	Tjugador jugadores [4];
	int numUsuarios;
	int numInvitaciones;
	int estado;
}TPartida;
typedef struct{ //Lista de sockets
	int sockets [100];
	int num_socket;
}Tlistasockets;
//
////Variables globales////
//
	MYSQL *conn;
	Tlistaconectados lista_conectados;
	Tlistasockets lista_sockets;
	TChat chats [100];
	TPartida partidas [100];
	//Estructura para el acceso excluyente
	pthread_mutex_t accesoexcluyente;
//
////Funciones y procedimientos////
//
//Consulta mayor puntuacion de un jugador 
int Mayor_puntuacion_jugador(char apodo[10]){

	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char consulta[200];
	sprintf(consulta,"SELECT puntuacion.puntuacion FROM puntuacion,jugador,combate WHERE jugador.apodo='%s' AND puntuacion.jugador='%s' AND puntuacion.combate = combate.id ORDER BY puntuacion.puntuacion DESC",apodo,apodo);
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
	
	//Miramos si existe el jugador
	sprintf(consulta,"SELECT apodo FROM jugador WHERE apodo ='%s'",apodo);
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
		sprintf(consulta,"SELECT ganador FROM combate WHERE ganador='%s'",apodo);
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
int registrar_jugador_enBBDD(char nombre[10],char pass[10],char apodo[20]){
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char consulta[200];
	
	sprintf(consulta,"SELECT nombrecuenta, apodo FROM jugador WHERE nombrecuenta='%s' AND apodo='%s'",nombre,apodo);
	err= mysql_query(conn,consulta);
	if(err!=0){
		printf("Error al realizar la consulta %u %s\n", mysql_errno(conn),mysql_error(conn));
		exit(1);
	}
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	if(row == NULL){//No existe el jugador
		sprintf(consulta, "INSERT INTO jugador VALUES ('%s', '%s', '%s', 'Bronce', 1, 2)", nombre, pass, apodo);
		err = mysql_query(conn, consulta);
	}
	else
	   return 0;
	   
	if (err!=0){
		printf("Error al realizar la consulta %u %s\n", mysql_errno(conn),mysql_error(conn));
		return 0;
	}
	else
		return 1;
}
//Logear jugador
int logear_jugador(char nombre[10],char pass[10], char apodo[20]){
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char consulta[200];
	
	sprintf(consulta,"SELECT nombrecuenta,pass,apodo FROM jugador WHERE nombrecuenta='%s' AND pass='%s'",nombre,pass);
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
//Añadir jugador a la lista de conectados
int add_jugador_aConectados(char nombre[20],int socket,Tlistaconectados *lista){
		
	int resultado = 1;
	int i = 0;
		
	//Busqueda
	while(i<lista->numeroconectados && resultado == 1){
		if(strcmp(nombre,lista->conectados[i].apodo)==0){
			//Implica que el jugador ya esta conectado
			resultado = 0;
		}
		else
		   i++;
	}
	if(resultado == 1){
		strcpy(lista->conectados[lista->numeroconectados].apodo,nombre);
		lista->conectados[lista->numeroconectados].socket = socket;
		lista->numeroconectados++;
		//Jugador añadido, devuelve 1
		return resultado;
	}
	else{
		//Jugador no añadido porque ya esta conectado , devuelve 0
		return resultado;
	}
	
}	
//Eliminar jugador de la lista de conectados
int eliminar_jugador_deConectados(char nombre[20],Tlistaconectados *lista){
			
	int resultado = 0;
	int i = 0;
			
	//Busqueda
	while(i<lista->numeroconectados && resultado == 0){
		if(strcmp(nombre,lista->conectados[i].apodo)==0){
		//Implica que el jugador esta conectado
		resultado = 1;
		}
		else{
			i++;
		}
	}
	if(resultado == 1){
		for(i;i<lista->numeroconectados-1;i++){
			strcpy(lista->conectados[i].apodo,lista->conectados[i+1].apodo);
			lista->conectados[i].socket = lista->conectados[i+1].socket;
		}
		lista->numeroconectados--;
		//Jugador eliminado, devuelve 1
		return resultado;
	}
	else if (resultado==0){
		//Jugador no conectado o que no existe, por tanto no se puede eliminar, devuelve 0
		return resultado;
	}
}
//Devuelve el socket de un jugador conectado
int socket_segun_apodoConectado(char nombre[20],Tlistaconectados *lista){
	
	int resultado = 0;
	int i = 0;
			
	//Busqueda
	while(i<lista->numeroconectados && resultado == 0){
		if(strcmp(nombre,lista->conectados[i].apodo)==0){
		//Implica que el jugador esta conectado
		    resultado = 1;
		}
		else{
			i++;
		}
	}
	if(resultado == 1){
		//Devuelve el socket del jugador conectado
		return lista->conectados[i].socket;
	}
	else{
		//Jugador no conectado o no existente , devuelve 0
		return resultado;
	}
}
//Funcion que crea el codigo con el numero de jugadores conectados y su nombre  
void codigo_conConectados(char codigo[],Tlistaconectados *lista){
				
	int i = 0;
				
	//Añade al principio del codigo el numero de jugadores conectados
	sprintf(codigo,"6/%d",lista->numeroconectados);
	//Añade los jugadores conectados al codigo , siempre que haya conectados
	if(lista->numeroconectados > 0){
		for(i = 0; i<lista->numeroconectados;i++){
			//Los nombres separados por /
			sprintf(codigo,"%s/%s",codigo,lista->conectados[i].apodo);
		}
	}
}
//
/*void a(char nombrecodigo[1000], char socketcodigo[1000], Tlistaconectados *lista){
								
	char *p;
					
    p = strtok(nombrecodigo,"/");
    sprintf(socketcodigo,"%d",socket_segun_conectado(p,lista));
    p = strtok(NULL,"/");
	while(p != NULL){
		sprintf(socketcodigo,"%s/%d",socketcodigo,socket_segun_conectado(p,lista));
		p = strtok(NULL,"/");
	}
}*/
//Procedimiento para notificar a todos los usuarios conectados los cambios de la lista de conectados	
void notificar_cambio_enConectados(Tlistaconectados *lista){
	char notificacion [1000];
	
	codigo_conConectados(notificacion, lista);
	printf("%s\n", notificacion);
	
	if(lista->numeroconectados>0){
		for(int j=0;j<lista->numeroconectados;j++){
			write(lista->conectados[j].socket,notificacion,strlen(notificacion));
	}
	}
}
//Función que devuelve el apodo según el socket
void apodo_segun_socketConectado(Tlistaconectados *lista, int socket, char apodo[20]){
	int j = 0;
	int encontrado=0;
	while((encontrado==0)&&(j<lista->numeroconectados))
	{
		if(socket==lista->conectados[j].socket)
		{
			strcpy(apodo, lista->conectados[j].apodo);
			encontrado = 1;
		}
		else
			j++;
	}
	
}
//Funcion para crear estructura de chat
int crear_chat (TChat chats[], int num_invitados){
	int j = 0;
	int encontrado = 0;
	
	while ((encontrado == 0) && (j<100)){
		if (chats[j].estado == 0)
			encontrado = 1;
		else
			j++;
	}
	
	if (encontrado == 1){
		chats[j].numUsuarios = num_invitados + 1;
		chats[j].numInvitaciones = num_invitados;
		chats[j].estado = 1;
		return j;
	}
	
	else if(encontrado == 0)
		return -1;
}
//
void enviar_invitaciones_deChat(TChat *chat, int idChat){
	char notificacion [1000];
	char host [20];
	strcpy(host, chat->usuarios[0].apodo);
	sprintf(notificacion, "7/2/%s/%d\0", host, idChat);
	printf("%s\n", notificacion);
	
	for(int j = 0; j<chat->numInvitaciones; j++){
		write(chat->usuarios[j+1].socket, notificacion, strlen(notificacion));
	}
}
//
void invitacion_aChat_rechazada (TChat *chat, int idChat, int socket){
	int j = 0;
	int encontrado = 0;
	
	while ((encontrado == 0) && (j<chat->numUsuarios)){
		if(chat->usuarios[j].socket == socket)
			encontrado == 1;
	}
	if (encontrado == 1){
		for (j; j<chat->numUsuarios-1; j++){
			strcpy(chat->usuarios[j].apodo, chat->usuarios[j+1].apodo);
			chat->usuarios[j].socket = chat->usuarios[j+1].socket;
		}
		chat->numUsuarios--;
		chat->numInvitaciones--;
	}
}
//
void invitacion_aChat_aceptada(TChat *chat, int idChat){
	chat->numInvitaciones--;
	
	for (int j = 0; j<chat->numUsuarios; j++){
		printf("%d: %s, %d\n", j, chat->usuarios[j].apodo, chat->usuarios[j].socket);
	}
	

}
//
void iniciar_chat (TChat *chat, int idChat){
	char notificacion [1000];
	sprintf(notificacion, "7/1/%d/1\0", idChat);
	printf("%s\n", notificacion);
	
	for (int j = 0; j<chat->numUsuarios; j++){
		write(chat->usuarios[j].socket, notificacion, strlen(notificacion));
		sleep(1);
	}
}
//
void inicializar_estado_chats(TChat chats[]){
	
	for(int k = 0; k<100; k++){
		chats[k].estado = 0;
	}
}
//
int eliminar_jugador_deBBDD (char usuario [20], char apodo [20], char pass [20]){
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int err;
	char consulta[200];
	
	sprintf(consulta,"SELECT nombrecuenta, apodo FROM jugador WHERE nombrecuenta='%s' AND apodo='%s'",usuario ,apodo);
	err= mysql_query(conn,consulta);
	if(err!=0){
		printf("Error al realizar la consulta %u %s\n", mysql_errno(conn),mysql_error(conn));
		exit(1);
	}
	resultado = mysql_store_result(conn);
	row = mysql_fetch_row(resultado);
	if(row != NULL){//Existe el jugador
		sprintf(consulta, "DELETE FROM jugador WHERE nombrecuenta ='%s'AND pass ='%s' AND apodo ='%s'", usuario, pass, apodo);
		err = mysql_query(conn, consulta);
	}
	else
	   return 0;
	
	if (err!=0){
		printf("Error al realizar la consulta %u %s\n", mysql_errno(conn),mysql_error(conn));
		return 0;
	}
	else
		return 1;
	
}
//
void add_socket_lista (Tlistasockets *lista, int socket){
	int j = 0;
	int encontrado = 0;
	while ((j<lista->num_socket) && (encontrado == 0)){
		if(lista->sockets[j] == -1){
			encontrado = 1;
			lista->sockets[j] = socket;
			lista->num_socket++;
		}
		else
		   j++;
	}
}
//
void quitar_socket_lista (Tlistasockets *lista, int socket){
	int j = 0;
	int encontrado = 0;
	while ((j<lista->num_socket) && (encontrado == 0)){
		if(lista->sockets[j] == -1){
			encontrado = 1;
		}
		else
		   j++;
	}
	if (encontrado == 1){
		for(int k = 0; k<lista->num_socket-1; k++){
			lista->sockets[k] = lista->sockets[k+1];
		}
		lista->num_socket--;
	}
}
//
void inicializar_vector_sockets(Tlistasockets *lista){
	lista->num_socket = 0;
	for(int j = 0; j<100; j++)
		lista->sockets[j] = -1;
}
//
int dame_socket_lista (Tlistasockets *lista){
	int pos = lista->num_socket;
	int s = lista->sockets[pos];
	return s;
}
//
void inicializar_estado_partidas (TPartida partidas[]){
	
	for(int k = 0; k<100; k++){
		partidas[k].estado = 0;
		for (int j = 0; j<4; j++){
			partidas[k].jugadores[j].fichas[0].estado_ficha = 0;
			partidas[k].jugadores[j].fichas[1].estado_ficha  = 0;
			partidas[k].jugadores[j].fichas[2].estado_ficha  = 0;
			partidas[k].jugadores[j].fichas[3].estado_ficha  = 0;
		}
	}
	
}
//
int crear_partida(TPartida partidas[], int num_invitados){
	int j = 0;
	int encontrado = 0;
	
	while ((encontrado == 0) && (j<100)){
		if (partidas[j].estado == 0)
			encontrado = 1;
		else
			j++;
	}
	
	if (encontrado == 1){
		partidas[j].numUsuarios = num_invitados + 1;
		partidas[j].numInvitaciones = num_invitados;
		partidas[j].estado = 1;
		return j;
	}
	
	else if(encontrado == 0)
		return -1;
}
//
void enviar_invitaciones_dePartida (TPartida *partida, int idPartida){
	char notificacion [1000];
	char host [20];
	strcpy(host, partida->jugadores[0].apodo);
	sprintf(notificacion, "9/2/%s/%d\0", host, idPartida);
	printf("%s\n", notificacion);
	
	for(int j = 0; j<partida->numInvitaciones; j++){
		write(partida->jugadores[j+1].socket, notificacion, strlen(notificacion));
	}
}
//
int invitacion_aPartida_aceptada(TPartida *partida, int idPartida){
	partida->numInvitaciones--;
	
	for (int j = 0; j<partida->numUsuarios; j++){
		printf("%d: %s, %d\n", j, partida->jugadores[j].apodo, partida->jugadores[j].socket);
	}
}
//
int invitacion_aPartida_rechazada(TPartida *partida, int idPartida, int socket){
	int j = 0;
	int encontrado = 0;
	
	while ((encontrado == 0) && (j<partida->numUsuarios)){
		if(partida->jugadores[j].socket == socket)
			encontrado == 1;
	}
	if (encontrado == 1){
		for (j; j<partida->numUsuarios-1; j++){
			strcpy(partida->jugadores[j].apodo, partida->jugadores[j+1].apodo);
			partida->jugadores[j].socket = partida->jugadores[j+1].socket;
			partida->jugadores[j].color = partida->jugadores[j+1].color;
		}
		partida->numUsuarios--;
		partida->numInvitaciones--;
	}
}
//
void iniciar_partida (TPartida *partida, int idPartida){
	char notificacion [1000];
	
	
	for(int j = 0; j<partida->numUsuarios; j++){
		int color = partida->jugadores[j].color;
		sprintf(notificacion, "9/1/%d/1/%d\0", idPartida, color);
		printf("%s\n", notificacion);
		printf("%d\n", partida->numUsuarios);
		write(partida->jugadores[j].socket, notificacion, strlen(notificacion));
	}
}
//
void quitar_jugador_dePartida (TPartida *partida, char apodo[20]){
	int encontrado = 0;
	int k = 0;
	while ((k<partida->numUsuarios) && (encontrado == 0)){
		if (strcmp(partida->jugadores[k].apodo, apodo) == 0)
			encontrado = 1;
		else
			k++;
	}
	
	if (encontrado == 1){
		for(k; k<partida->numUsuarios-1; k++){
			strcpy(partida->jugadores[k].apodo, partida->jugadores[k+1].apodo);
			partida->jugadores[k].socket = partida->jugadores[k+1].socket;
			for (int l = 0; l<4; l++){
				partida->jugadores[k].fichas[l].pos_ficha = partida->jugadores[k+1].fichas[l].pos_ficha;
				partida->jugadores[k].fichas[l].estado_ficha = partida->jugadores[k+1].fichas[l].estado_ficha;
			}
		}
		partida->numUsuarios--;
	}
	
}
//
void mover_ficha (TPartida *partida, int color, int ficha, int nueva_pos){
	int j = 0;
	int encontrado = 0;
	
	while ((j<4) && (encontrado == 0)){
		if (partida->jugadores[j].color == color)
			encontrado == 1;
		else
			j++;
	}
	
	if (encontrado == 1){
		partida->jugadores[j].fichas[ficha].pos_ficha = nueva_pos;
	}
}
//
////Función del thread para atender a cada cliente////
void *Atender_Cliente(void *socket){

	int sock_conn = * (int *) socket;
	int ret;
	char peticion[1000];
	char respuesta[1000];
	int terminar = 0;
	int resultado;	
	int res;
	
	while(terminar == 0){
		//Servicio
		//read hace 2 cosas , guarda lo que llega en el socket en el buffer peticion
		//y devuelve el numero de Bytes del mensaje , es decir su tamaÃ±o
		//el tamaño de un char es de 1 Byte , o lo que es lo mismo 8 bits
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf("Recibido\n");
		//Añadimos el caracter de fin de string para que no escriba lo que hay despues en el buffer
		peticion[ret] = '\0';
		printf("Petición recibida: %s\n", peticion);
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
				int resultado = logear_jugador(nombre,pass,apodo);
				if(resultado == 1){
					sprintf(respuesta,"0/%d/%s\0",resultado,apodo);
					pthread_mutex_lock (&accesoexcluyente);//Indicamos que no se interrumpa
					resultado = add_jugador_aConectados(apodo,sock_conn,&lista_conectados);
					pthread_mutex_unlock (&accesoexcluyente);//Indicamos que ya se puede interrumpir
					
					if(resultado == 1){
						printf("Se ha conectado %s\n",apodo);
						write(sock_conn, respuesta, strlen(respuesta));
						sleep(1);
					}
					else{
						printf("Error al añadir un jugador a la lista de conectados");
					}
					notificar_cambio_enConectados(&lista_conectados);
				}
				else if (resultado==-1){
					sprintf(respuesta,"0/%d\0",resultado);
					terminar=1;
					write(sock_conn,respuesta,strlen(respuesta));
					printf("%s\n", respuesta);
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
				pthread_mutex_lock (&accesoexcluyente);
				resultado = registrar_jugador_enBBDD(nombre,pass,apodo);
				pthread_mutex_unlock (&accesoexcluyente);
				if(resultado == 1){
					printf("Se ha registrado el jugador %s correctamente\n",apodo);
					pthread_mutex_lock (&accesoexcluyente);
					add_jugador_aConectados(apodo,sock_conn,&lista_conectados);
					pthread_mutex_unlock (&accesoexcluyente);
				}
				else{
					printf("Error al registrar al jugador %s\n",apodo);
				}
				sprintf(respuesta,"4/%d\0",resultado);
				write(sock_conn,respuesta,strlen(respuesta));
				sleep(1);
				notificar_cambio_enConectados(&lista_conectados);
				break;
			//Desconectar
			case 5:
				apodo[20];
				apodo_segun_socketConectado(&lista_conectados, sock_conn, apodo);
				pthread_mutex_lock (&accesoexcluyente);
				resultado = eliminar_jugador_deConectados(apodo,&lista_conectados);
				pthread_mutex_unlock (&accesoexcluyente);
				if(resultado == 1){
					printf("Se acabo el servicio para el jugador %s\n",apodo);
					notificar_cambio_enConectados(&lista_conectados);
					terminar = 1;
				}
				break;
				
			
			
			case 7: //Peticion relacionada con chat
				
				// 7/tipo de peticion/...
				// Volvemos a realizar el proceso de analizar la "subpeticion"
				p = strtok(NULL, "/");
				int subcodigo = atoi(p);
				int idChat;
				
				switch (subcodigo){
					
					case 1: //Invitacion y creacion de chat
						// 7/1/numInvitados/invitado1/invitado2/.../invitadoN
						p = strtok(NULL, "/");
						int numInvitados = atoi (p);
						pthread_mutex_lock (&accesoexcluyente);
						idChat = crear_chat(chats, numInvitados);
						printf("IDCHAT: %d\n", idChat);
						pthread_mutex_unlock (&accesoexcluyente);
						char apodo_cliente [20];
						apodo_segun_socketConectado(&lista_conectados, sock_conn, apodo_cliente);
						
						//Introducimos en la 1ª posicion de usuario del chat al que solicita
						//la creacion del chat
						strcpy(chats[idChat].usuarios[0].apodo, apodo_cliente);
						chats[idChat].usuarios[0].socket = sock_conn;
						
						//Despues metemos en el resto de posiciones al resto de usuarios
						int j;
						pthread_mutex_lock (&accesoexcluyente);
						for(j = 0; j<numInvitados; j++){
							p = strtok (NULL, "/");
							strcpy(chats[idChat].usuarios[j+1].apodo, p);
							chats[idChat].usuarios[j+1].socket = socket_segun_apodoConectado(p, &lista_conectados);
						}
						pthread_mutex_unlock (&accesoexcluyente);
						enviar_invitaciones_deChat(&chats[idChat], idChat);
						
						break;
						
					case 2: //Respuesta a la invitacion de chat
						p = strtok (NULL, "/");
						idChat = atoi(p);
						p = strtok (NULL, "/");
						int res = atoi(p);
						
						if(res == 0){
							pthread_mutex_lock (&accesoexcluyente);
							invitacion_aChat_rechazada(&chats[idChat], idChat, sock_conn);
							pthread_mutex_unlock (&accesoexcluyente);
						}
						else if (res == 1)
							invitacion_aChat_aceptada(&chats[idChat], idChat);
						
						if(chats[idChat].numInvitaciones == 0){
							char notificacion [1000];
							sprintf(notificacion, "7/1/%d/1\0", idChat);
							printf("%s\n", notificacion);
							printf("%d\n", chats[idChat].numUsuarios);
							
							for(int j = 0; j<chats[idChat].numUsuarios; j++)
								write(chats[idChat].usuarios[j].socket, notificacion, strlen(notificacion));
						}
						break;
						
					case 3: //Mensaje a chat
						p = strtok (NULL, "/");
						idChat = atoi(p);
						p = strtok (NULL, "/");
						char mensaje [50];
						strcpy(mensaje, p);
						
						//Construimos la notificacion
						printf("Control\n");
						char apodo_tx [20];
						apodo_segun_socketConectado(&lista_conectados, sock_conn, apodo_tx);
						char notificacion [1000];
						sprintf(notificacion, "7/3/%d/%s/%s\0", idChat, apodo_tx, mensaje);
						printf("%s\n", notificacion);
						for(int j = 0; j<chats[idChat].numUsuarios; j++)
							write(chats[idChat].usuarios[j].socket, notificacion, strlen(notificacion));
						break;
					
				}
			
				break;
				
			case 8: //Baja del sistema -> 8/usuario/apodo/pass
				p = strtok (NULL, "/");
				char usuario [20];
				strcpy(usuario, p);
				p = strtok (NULL, "/");
				strcpy(apodo, p);
				p = strtok (NULL, "/");
				char password [20];
				strcpy(password, p);
				pthread_mutex_lock (&accesoexcluyente);
				resultado = eliminar_jugador_deBBDD(usuario, apodo, password);
				pthread_mutex_unlock (&accesoexcluyente);
				if(resultado == 1){
					printf("Se ha dado de baja a %s correctamente\n",apodo);
					pthread_mutex_lock (&accesoexcluyente);
					eliminar_jugador_deConectados(apodo,&lista_conectados);
					pthread_mutex_unlock (&accesoexcluyente);
				}
				else{
					printf("Error al eliminar a %s\n",apodo);
				}
				sprintf(respuesta,"8/%d\0",resultado);
				write(sock_conn,respuesta,strlen(respuesta));
				sleep(1);
				notificar_cambio_enConectados(&lista_conectados);
				break;
				
			case 9: //Peticion relacionada con partida de parchis
			
				// 9/tipo de peticion/...
				// Volvemos a realizar el proceso de analizar la "subpeticion"
				p = strtok(NULL, "/");
				int subcodigo2 = atoi(p);
				int idPartida;
			
				switch (subcodigo2){
				
					case 0: //Baja de jugador de la partida
						//9/0/idPartida
						p = strtok(NULL, "/");
						idPartida = atoi(p);
						apodo [20];
						apodo_segun_socketConectado(&lista_conectados, sock_conn, apodo);
						pthread_mutex_lock (&accesoexcluyente);
						quitar_jugador_dePartida(&partidas[idPartida], apodo);
						pthread_mutex_unlock (&accesoexcluyente);
						break;
						
					case 1: //Invitacion y creacion de partida
						// 9/1/numInvitados/invitado1/invitado2/.../invitadoN
						p = strtok(NULL, "/");
						int numInvitados = atoi (p);
						pthread_mutex_lock (&accesoexcluyente);
						idPartida = crear_partida(partidas, numInvitados);
						printf("IDPARTIDA: %d\n", idChat);
						pthread_mutex_unlock (&accesoexcluyente);
						char apodo_cliente [20];
						apodo_segun_socketConectado(&lista_conectados, sock_conn, apodo_cliente);
					
						//Introducimos en la 1ª posicion de usuario del chat al que solicita
						//la creacion del chat
						pthread_mutex_lock (&accesoexcluyente);
						strcpy(partidas[idPartida].jugadores[0].apodo, apodo_cliente);
						partidas[idPartida].jugadores[0].socket = sock_conn;
						partidas[idPartida].jugadores[0].color = 0;
						pthread_mutex_unlock(&accesoexcluyente);
					
						//Despues metemos en el resto de posiciones al resto de usuarios
						int j;
						pthread_mutex_lock (&accesoexcluyente);
						for(j = 0; j<numInvitados; j++){
							p = strtok (NULL, "/");
							strcpy(partidas[idPartida].jugadores[j+1].apodo, p);
							partidas[idPartida].jugadores[j+1].socket = socket_segun_apodoConectado(p, &lista_conectados);
							partidas[idPartida].jugadores[j+1].color = j+1;
						}
						pthread_mutex_unlock (&accesoexcluyente);
						enviar_invitaciones_dePartida(&partidas[idPartida], idPartida);
						break;
						
					case 2: //Respuesta a la invitacion de partida
						p = strtok (NULL, "/");
						idPartida = atoi(p);
						p = strtok (NULL, "/");
						int res = atoi(p);
						
						if(res == 0){
							pthread_mutex_lock (&accesoexcluyente);
							invitacion_aPartida_rechazada(&partidas[idPartida], idPartida, sock_conn);
							pthread_mutex_unlock (&accesoexcluyente);
						}
						else if (res == 1){
							pthread_mutex_lock (&accesoexcluyente);
							invitacion_aPartida_aceptada(&partidas[idPartida], idPartida);
							pthread_mutex_unlock (&accesoexcluyente);
						}
						if(partidas[idPartida].numInvitaciones == 0){
							iniciar_partida(&partidas[idPartida], idPartida);
						}
						break;
						
					case 5: //Movimiento en partida
						
						//9/5/idPartida/color/ficha/nueva_pos
						p = strtok (NULL, "/");
						idPartida = atoi(p);
						p = strtok (NULL, "/");
						int color = atoi (p);
						p = strtok (NULL, "/");
						int ficha = atoi (p);
						p = strtok (NULL, "/");
						int nueva_pos = atoi (p);
						
						pthread_mutex_lock (&accesoexcluyente);
						//mover_ficha(&partidas[idPartida], color, ficha, nueva_pos);
						pthread_mutex_unlock (&accesoexcluyente);
						
						//Construimos notificacion con nueva posicion de la ficha de color
						char notificacion [1000];
						sprintf(notificacion, "9/5/%d/%d/%d/%d\0", idPartida, color, ficha, nueva_pos);
						printf("Movimiento: %s\n", notificacion);
						/*for(int j = 0; j<partidas[idPartida].numUsuarios; j++)
							write(partidas[idPartida].jugadores[j].socket, notificacion, strlen(notificacion));*/
						
						break;
					case 6: //Mensaje a chat de partida
						p = strtok (NULL, "/");
						idPartida = atoi(p);
						p = strtok (NULL, "/");
						char mensaje [50];
						strcpy(mensaje, p);
						
						//Construimos la notificacion
						printf("Control\n");
						char apodo_tx [20];
						apodo_segun_socketConectado(&lista_conectados, sock_conn, apodo_tx);
						notificacion [1000];
						sprintf(notificacion, "9/6/%d/%s/%s\0", idPartida, apodo_tx, mensaje);
						printf("%s\n", notificacion);
						for(int j = 0; j<partidas[idPartida].numUsuarios; j++)
							write(partidas[idPartida].jugadores[j].socket, notificacion, strlen(notificacion));
						break;
						
					default:
						printf("Mensaje erroneo, 9/\n");
						break;
					
			}
			break;
				
			default:
				printf("Error en el codigo\n");
				break;
		}
		
	}
	//Se acabo el servicio para el cliente
	close(sock_conn);
	//Quitamos su socket del vector
	quitar_socket_lista(&lista_sockets, sock_conn);
}
//
////Main////
//
int main(int argc, char *argv[])
{
	int sock_listen,sock_conn;
	struct sockaddr_in serv_adr;
	int sockets[100];
	int i;
	pthread_t thread;
	lista_conectados.numeroconectados = 0;
	pthread_mutex_init (&accesoexcluyente, NULL);
	
	//Inicializamos el estado de los chats y el vector de sockets
	inicializar_estado_chats(chats);
	inicializar_vector_sockets(&lista_sockets);
	inicializar_estado_partidas(partidas);
	
	
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexion, indicando nuestras claves de acceso
	// al servidor de bases de datos (user,pass)
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "T1_juego", 0, NULL, 0);
	if (conn==NULL)
	{
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	// INICIALIZACIONES
	//Abrimos el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creando socket\n");
	// Hacemos el bind en el port
	memset(&serv_adr, 0, sizeof(serv_adr));// inicializa a cero serv_addr
	serv_adr.sin_family = AF_INET;
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY); /* Lo mete en IP local */
	serv_adr.sin_port = htons(50052);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf("Error en el bind\n");
	// Limitamos el numero de conexiones pendientes
	if (listen(sock_listen, 10) < 0)
		printf("Error en el listen\n");
	for(i=0;;i++){
		printf("Escuchando\n");
		//sock_conn es el socket que utilizaremos para el cliente
		sock_conn = accept(sock_listen, NULL, NULL);
		printf("He recibido conexion\n");
		//Desactivamos el algoritmo de Nagle.
		int flag = 1;
		int result = setsockopt(sock_conn,IPPROTO_TCP,TCP_NODELAY,(char *) &flag,sizeof(int));
		if(result==-1)
			printf("Error al desactivar el algoritmo de Nagle.\n");
		//Almacenamos en el vector de sockets , el socket con el que nos comunicaremos con el usuario recien conectado
		sockets[i] = sock_conn;
		add_socket_lista(&lista_sockets, sock_conn);
		//Crear thread y decirle lo que tiene que hacer
		//&thread[i] es un parametro de salida
		//&sockets[i] es un parametro de entrada que le pasa a la funcion Atender_Cliente
		int socket = dame_socket_lista (&lista_sockets);
		int s = lista_sockets.sockets[lista_sockets.num_socket];
		pthread_create(&thread,NULL,Atender_Cliente,&sockets[i]);
	}
	//Cerramos la conexion con la base de datos
	mysql_close(conn);
	return 0;
}
