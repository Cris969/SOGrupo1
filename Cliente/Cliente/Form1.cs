using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Resources;

namespace Cliente
{
    public partial class Form1 : Form
    {
        ///////////////////////////////////// VARIABLES Y OBJETOS GLOBALES /////////////////////////////////////
        Socket socket;
        IPEndPoint remoteEP;
        Thread atender;

        delegate void DelegadoParaListarConectados(string [] mensaje);

        bool conectado = false;
        string usuario;
        string apodo;
        string apodoConsulta, IDcombateConsulta;

        int consultaID;  //Identificador de la consulta que el usuario seleccione en el menú.
                         // 1-> Mayor puntuacion de un jugador
                         // 2-> Nº de partidas ganadas de un jugador
                         // 3-> Datos de una partida

        public Form1()
        {
            InitializeComponent();

            panel_inicioSesion.Visible = false;
            subpanel_menuConsultas.Visible = false;
            panel_consultas.Visible = false;
            panel_estadoConexion.Visible = false;
            panel_listaConectados.Visible = false;
            label_nickRegistro.Visible = false;
            textBox_nick.Visible = false;
            button_registrarse.Visible = false;
        }

        private void AtenderServidor()
        {
            while (true)
            {
                string apodoTx, apodoRx;

                //Recibimos mensaje del servidor
                byte[] data = new byte[80];
                socket.Receive(data);
                string[] mensaje = Encoding.ASCII.GetString(data).Split('\0');
                string [] trozoMensaje=mensaje[0].Split('/');
                int IDmensaje = Convert.ToInt32(trozoMensaje[0]);

                switch (IDmensaje)
                {
                    case 0: //Respuesta al login
                        //El codigo esperado es 1/Apodo si esta todo ok
                        //-1 si falla 
                        //Comprobamos que el resultdo sea el correcto o no.
                        if (Convert.ToInt32(trozoMensaje[1]) == -1)
                        {
                            MessageBox.Show("Contraseña o usuario incorrecto/s");
                            Desconexion();
                        }
                        else
                        {
                            apodo = Convert.ToString(trozoMensaje[2]);
                            MessageBox.Show("Inicio de sesión correcto.");
                        }
                        break;
                    case 1: //Respuesta consulta mayor puntuación de un jugador
                        MessageBox.Show("La mayor puntuación de " + apodoConsulta + " es: " + trozoMensaje[1]);
                        break;
                    case 2: //Respuesta consulta datos de una partida
                            //El codigo recibido es Resultado/Ganador/Jugador1/Puntuacion1/Jugador2/Puntuacion2
                        MessageBox.Show("Jugador1: " + trozoMensaje[3] + " Puntuacion1: " + trozoMensaje[4]);
                        MessageBox.Show("Jugador2: " + trozoMensaje[5] + " Puntuacion2: " + trozoMensaje[6]);
                        MessageBox.Show("Ganador: " + trozoMensaje[2]);
                        break;
                    case 3: //Respuesta consulta # partidas ganadas
                        MessageBox.Show("El # de partidas ganadas de " + apodoConsulta + " es: " + trozoMensaje[1]);
                        break;
                    case 4: //Respuesta al registro
                        break;
                    case 6: //Notificación de cambio en lista de conectados
                                                
                        DelegadoParaListarConectados delegado = new DelegadoParaListarConectados(ListarConectados);
                        dataGridView_listaConectados.Invoke(delegado, new object[] { trozoMensaje });

                        break;
                    case 7:
                        apodoTx = trozoMensaje[1];
                        apodoRx = trozoMensaje[2];
                        string respuesta;

                        DialogResult dr = MessageBox.Show(apodoTx+" te invita a jugar. ¿Aceptar invitación?", "ACEPTAR INVITACIÓN", MessageBoxButtons.YesNo);
                        switch (dr)
                        {
                            case DialogResult.No:
                                //Construimos la respuesta para el servidor: 8/apodoTx/apodoRx/0
                                respuesta = ("8/" + apodoTx+"/"+apodoRx+"/0\0");
                                socket.Send(Encoding.ASCII.GetBytes(respuesta));
                                break;
                            case DialogResult.Yes:
                                //Construimos la respuesta para el servidor: 8/apodoTx/apodoRx/1
                                respuesta = ("8/" + apodoTx  + "/" + apodoRx + "/1\0");
                                socket.Send(Encoding.ASCII.GetBytes(respuesta));
                                MessageBox.Show("Se jugará la partida!");
                                break;
                        }

                        break;
                    case 8:
                        apodoRx = trozoMensaje[2];
                        int res = Convert.ToInt32(trozoMensaje[3]);
                        if (res == 0)
                        {
                            MessageBox.Show(apodoRx+" ha rechazado la invitación.");
                        }
                        else if (res == 1)
                        {
                            MessageBox.Show(apodoRx + " ha aceptado la invitación!");
                        }
                        break;
                    default:
                        MessageBox.Show("Mensaje recibido erróneo.");
                        break;
                        

                }
            }
        }

        private void ListarConectados(string [] trozoMensaje)
        {
            panel_listaConectados.Visible = true;

            dataGridView_listaConectados.RowCount = Convert.ToInt32(trozoMensaje[1]);
            dataGridView_listaConectados.ColumnCount = 1;
            for (int i = 0; i < Convert.ToInt32(trozoMensaje[1]); i++)
            {
                dataGridView_listaConectados[0, i].Value = trozoMensaje[i + 2];
            }
            dataGridView_listaConectados.ColumnHeadersVisible = false;
        }

        ///////////////////////////////////// FUNCIONAMIENTO MENU LATERAL /////////////////////////////////////
        //EsconderSubMenu() y MostrarSubMenu() para hacer funcionar
        //el menú de la derecha
        private void EsconderSubMenu()
        {
            subpanel_menuConsultas.Visible = false;
        }
        private void MostrarSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                EsconderSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        ///////////////////////////////////// CONEXIÓN Y DESCONEXIÓN /////////////////////////////////////
        //Realizamos la conexión y desconexión al servidor.
        private void Conexion()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            remoteEP = new IPEndPoint(IPAddress.Parse("192.168.1.40"), 9130);
            try
            {
                socket.Connect(remoteEP);
                conectado = true;
                panel_estadoConexion.Invalidate();

                //Ponemos en marcha el thread que atenderá los mensajes del servidor
                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
            }

            catch (SocketException)
            {
                MessageBox.Show("Unable to connect to server.");
                return;
            }
        }
        private void Desconexion()
        {
            try
            {
                conectado = false;
                EsconderSubMenu();
                cerrar_lista_conectados();
                panel_estadoConexion.Invalidate();

                //Construimos la consulta para el servidor (5/usuario)
                //para informar de la desconexión al servidor.
                string consulta = ("5/" + apodo);
                socket.Send(Encoding.ASCII.GetBytes(consulta));

                //Cerramos la conexión
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                conectado = false;

                //Cerramos el thread que atiende los mensajes del servidor
                atender.Abort();
            }

            catch (SocketException)
            {
                MessageBox.Show("Unable to disconnect to server.");
            }

        }

        //Botón para desconectarse del servidor.
        private void button_desconexion_Click(object sender, EventArgs e)
        {
            if (conectado == false)
            {
                MessageBox.Show("No está conectado.");
            }

            else if (conectado == true)
            {
                try
                {
                    Desconexion();
                    panel_estadoConexion.Invalidate();
                    panel_consultas.Visible = false;
                    panel_inicioSesion.Visible = false;
                    panel_estadoConexion.Visible = false;
                }

                catch (SocketException)
                {
                    MessageBox.Show("Error al deconectar.");
                    panel_estadoConexion.Invalidate();
                }
            }
        }

        ///////////////////////////////////// LISTA DE CONECTADOS /////////////////////////////////////
        //Procedimiento para cerrar la lista.
        private void cerrar_lista_conectados()
        {
            panel_listaConectados.Visible = false;
        }

        //Consulta al servidor para obtener una lista de conectados.
        private void button_listar_conectados_Click(object sender, EventArgs e)
        {
        }
        private void button_actualizarConectados_Click(object sender, EventArgs e)
        {
        }
        private void button_cerrarLista_Click(object sender, EventArgs e)
        {
            cerrar_lista_conectados();
        }

        ///////////////////////////////////// INICIO DE SESIÓN /////////////////////////////////////
        //Al iniciar sesión realizamos la conexión al servidor.
        private void button_inicio_sesion_Click(object sender, EventArgs e)
        {
            panel_inicioSesion.Visible = true;
            panel_estadoConexion.Visible = true;
            panel_estadoConexion.Invalidate();
            label_nickRegistro.Visible = false;
            textBox_nick.Visible = false;
            button_login.Visible = true;
            button_registrarse.Visible = false;
        }
        private void button_login_Click(object sender, EventArgs e)
        {
            if (conectado == true)
            {
                MessageBox.Show("Para iniciar sesión de nuevo primero deberá cerrar la conexión.");
            }
            else
            {
                usuario = textBox_usuario.Text;
                string password = textBox_contraseña.Text;

                //Control de errores.
                if ((usuario == "") || (password == ""))
                {
                    MessageBox.Show("Por favor, rellene los campos.");
                    return;
                }

                Conexion();

                if (conectado == true)
                {
                    //Construimos la consulta para nuestro servidor (0/Usuario/Contraseña)
                    //El servidor comprobará si nuestro usuario existe o no
                    //En caso de que exista y la contrseña sea correcta devolverá un 1/apodo
                    //En caso de algún fallo devolverá un -1.

                    string consulta = ("0/" + usuario + "/" + password);
                    socket.Send(Encoding.ASCII.GetBytes(consulta));
                }
            }
        }

        ///////////////////////////////////// REGISTRO /////////////////////////////////////
        //Procedimientos para registrarse
        //Para registrarse hay que estar deconectado
        //Una vez registrado se mantiene conectado con el usuario nuevo.
        private void linkLabel_registrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (conectado == true)
            {
                MessageBox.Show("Para registrarse debe cerrar la sesión actual.");
                return;
            }
            else if (conectado == false)
            {
                label_nickRegistro.Visible = true;
                textBox_nick.Visible = true;
                button_registrarse.Visible = true;
                button_login.Visible = false;
            }
        }
        private void button_registrarse_Click(object sender, EventArgs e)
        {
            //Control de errores.
            if ((textBox_usuario.Text == "") || (textBox_contraseña.Text == "") || (textBox_nick.Text == ""))
            {
                MessageBox.Show("Por favor, rellene los campos.");
                return;
            }
            else
            {
                try
                {
                    Conexion();
                    //Construimos la consulta para nuestro servidor (4/Usuario/Contraseña/Nick)
                    //El servidor devolverá 1 si se ha registrado con éxito y -1 si se ha producido algún error.
                    string consulta = ("4/" + textBox_usuario.Text + "/" + textBox_contraseña.Text + "/" + textBox_nick.Text);
                    socket.Send(Encoding.ASCII.GetBytes(consulta));

                    byte[] data = new byte[1024];
                    int dataSize = socket.Receive(data);
                    int respuesta = Convert.ToInt32(Encoding.ASCII.GetString(data, 0, dataSize));

                    if (respuesta == -1)
                    {
                        MessageBox.Show("Se ha producido un error al registrarse.");
                    }
                    else if (respuesta == 1)
                    {
                        MessageBox.Show("Registro completado con éxito");
                        button_registrarse.Visible = false;
                        label_nickRegistro.Visible = false;
                        textBox_nick.Visible = false;
                        button_login.Visible = true;
                        panel_estadoConexion.Invalidate();
                    }
                }
                catch (SocketException)
                {
                    MessageBox.Show("Error al realizar la consulta.");
                }
            }
        }

        ///////////////////////////////////// CONSULTAS /////////////////////////////////////
        private void button_consultas_Click(object sender, EventArgs e)
        {
            if (conectado == false)
            {
                MessageBox.Show("Primero deberá conectarse.");
            }

            else if (conectado == true)
            {
                MostrarSubMenu(subpanel_menuConsultas);
                label_títuloConsulta.Visible = false;
            }
        }

        //Seleccionando la consulta que queremos en el submenú asignamos
        //el valor correspondiente a consultaID y mostramos los botones 
        //para realizar la consulta
        private void button_mayor_puntuacion_Click(object sender, EventArgs e)
        {
            panel_consultas.Visible = true;
            consultaID = 1;
            label_títuloConsulta.Visible = true;
            label_títuloConsulta.Text = "MAYOR PUNTUACIÓN DE UN JUGADOR";
            label_IDpartidaConsulta.Visible = false;
            label_nickConsulta.Visible = true;
        }
        private void button_partidas_ganadas_Click(object sender, EventArgs e)
        {
            panel_consultas.Visible = true;
            consultaID = 2;
            label_títuloConsulta.Visible = true;
            label_títuloConsulta.Text = "Nº DE PARTIDAS GANADAS DE UN JUGADOR";
            label_IDpartidaConsulta.Visible = false;
            label_nickConsulta.Visible = true;
        }
        private void button_datos_partida_Click(object sender, EventArgs e)
        {
            panel_consultas.Visible = true;
            consultaID = 3;
            label_títuloConsulta.Visible = true;
            label_títuloConsulta.Text = "DATOS DE UNA PARTIDA";
            label_IDpartidaConsulta.Visible = true;
            label_nickConsulta.Visible = false;
        }

        //Según el valor de consultaID realizaremos una consulta u otra
        private void button_consultar_Click(object sender, EventArgs e)
        {
            if (consultaID == 1)
            {
                apodoConsulta= textBox_datosConsulta.Text;

                try
                {
                    //Construimos la consulta para nuestro servidor (1/Usuario)
                    //El servidor devolverá la mayor puntuación del jugador.
                    string consulta = ("1/" + apodoConsulta);
                    socket.Send(Encoding.ASCII.GetBytes(consulta));
                }
                catch (SocketException)
                {
                    MessageBox.Show("Error al realizar la consulta.");
                }
            }

            else if (consultaID == 2)
            {
                string nickUsuario = textBox_datosConsulta.Text;

                try
                {
                    //Construimos la consulta para nuestro servidor (3/Usuario)
                    //El servidor devolverá el # de partidas ganadas del usuario.
                    string consulta = ("2/" + nickUsuario);
                    socket.Send(Encoding.ASCII.GetBytes(consulta));
                }
                catch (SocketException)
                {
                    MessageBox.Show("Error al realizar la consulta.");
                }
            }
            else if (consultaID == 3)
            {
                IDcombateConsulta = textBox_datosConsulta.Text;

                try
                {
                    //Construimos la consulta para nuestro servidor (3/combateID)
                    //El servidor devolverá el ganador del combate.
                    string consulta = ("3/" + IDcombateConsulta);
                    socket.Send(Encoding.ASCII.GetBytes(consulta));
                }
                catch (SocketException)
                {
                    MessageBox.Show("Error al realizar la consulta.");
                }
            }
        }

        ///////////////////////////////////// EXIT /////////////////////////////////////
        //Botón para salir, si está desconectado cierra el programa, si se
        //está conectado se desconecta y cierra el programa.
        private void button_salir_Click(object sender, EventArgs e)
        {
            if (conectado == false)
            {
                this.Close();
            }

            else if (conectado == true)
            {
                try
                {
                    Desconexion();
                    panel_estadoConexion.Invalidate();
                    this.Close();
                }

                catch (SocketException)
                {
                    MessageBox.Show("Error al desconectar.");
                    this.Close();
                }
            }
        }

        private void dataGridView_listaConectados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string apodoRx = Convert.ToString(dataGridView_listaConectados.CurrentCell.Value);
            DialogResult dr = MessageBox.Show("Quieres invitar a jugar a: " + apodoRx, "ENVIAR INVITACIÓN", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    try
                    {
                        //Construimos la consulta para nuestro servidor (3/combateID)
                        //El servidor devolverá el ganador del combate.
                        string peticion = ("7/" + apodo +"/" + apodoRx);
                        socket.Send(Encoding.ASCII.GetBytes(peticion));
                    }
                    catch (SocketException)
                    {
                        MessageBox.Show("Error al enviar la petición.");
                    }
                    break;
            }
        }

        ///////////////////////////////////// ESTADO CONEXIÓN /////////////////////////////////////
        //En paint del panel_estadoConexion se muestra el estado de la conexión y 
        //el color correspondiente
        private void panel_estadoConexion_Paint(object sender, PaintEventArgs e)
        {
            if (conectado == false)
            {
                label_estadoConexion.Text = "DESCONECTADO";
                panel_estadoConexion.BackColor = Color.Red;

            }
            else if (conectado == true)
            {
                label_estadoConexion.Text = "CONECTADO";
                panel_estadoConexion.BackColor = Color.LightGreen;

            }
        }

        
    }
}
