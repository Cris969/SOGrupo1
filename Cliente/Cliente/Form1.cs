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

namespace Cliente
{
    public partial class Form1 : Form
    {
        ///////////////////////////////////// VARIABLES Y OBJETOS COMUNES /////////////////////////////////////
        Socket socket;
        IPEndPoint remoteEP;

        bool conectado = false;
        string usuario;
        string apodo;

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
            remoteEP = new IPEndPoint(IPAddress.Parse("192.168.1.41"), 9210);
            try
            {
                socket.Connect(remoteEP);
                conectado = true;
                panel_estadoConexion.Invalidate();
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
                cerrar_lista_conectados();

                //Construimos la consulta para el servidor (5/usuario)
                //para informar dwe la desconexión al servidor.
                string consulta = ("5/" + apodo);
                socket.Send(Encoding.ASCII.GetBytes(consulta));

                //Cerramos la conexión
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                conectado = false;
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
        //Procedimiento para listar conectados y para cerrar la lista.
        private void listar_conectados()
        {
            //Construimos la consulta para el servidor (6/)
            //El servidor nos devolverá una lista de usuarios conectados (3/María/Juan/Pedro).
            string consulta = "6/";
            socket.Send(Encoding.ASCII.GetBytes(consulta));

            byte[] data = new byte[1024];
            int dataSize = socket.Receive(data);
            string conectados = Encoding.ASCII.GetString(data, 0, dataSize);

            char separador = '/';
            string[] trozos_conectados = conectados.Split(separador);

            panel_listaConectados.Visible = true;

            dataGridView_listaConectados.RowCount = trozos_conectados.Length-1;
            dataGridView_listaConectados.ColumnCount = 1;
            for (int i = 0; i < Convert.ToInt32(trozos_conectados[0]); i++)
            {
                dataGridView_listaConectados[0, i].Value = trozos_conectados[i+1];
            }
            dataGridView_listaConectados.ColumnHeadersVisible = false;
        }
        private void cerrar_lista_conectados()
        {
            panel_listaConectados.Visible = false;
        }

        //Consulta al servidor para obtener una lista de conectados.
        private void button_listar_conectados_Click(object sender, EventArgs e)
        {
            listar_conectados();
        }
        private void button_actualizarConectados_Click(object sender, EventArgs e)
        {
            listar_conectados();
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
                    //En caso de que exista y la contrseña sea correcta devolverá un 1
                    //En caso de algún fallo devolverá un -1.

                    string consulta = ("0/" + usuario + "/" + password);
                    socket.Send(Encoding.ASCII.GetBytes(consulta));

                    byte[] data = new byte[1024];
                    socket.Receive(data);
                    consulta = Encoding.ASCII.GetString(data).Split('\0')[0];
                    //El codigo esperado es 1/Apodo si esta todo ok
                    //-1 si falla 
                    string[] codigo = consulta.Split('/');
                    int resultado = Convert.ToInt32(codigo[0]);

                    //Comprobamos que el resultdo sea el correcto o no.
                    if (resultado == -1)
                    {
                        Desconexion();
                        MessageBox.Show("Contraseña o usuario incorrecto/s");
                    }
                    else
                    {
                        apodo = codigo[1];
                        MessageBox.Show("Inicio de sesión correcto.");
                    }
                }
            }
        }

        ///////////////////////////////////// RESGISTRO /////////////////////////////////////
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
                string nickUsuario = textBox_datosConsulta.Text;

                try
                {
                    //Construimos la consulta para nuestro servidor (1/Usuario)
                    //El servidor devolverá la mayor puntuación del jugador.
                    string consulta = ("1/" + nickUsuario);
                    socket.Send(Encoding.ASCII.GetBytes(consulta));

                    byte[] data = new byte[1024];
                    int dataSize = socket.Receive(data);
                    MessageBox.Show("La mayor puntuación de " + nickUsuario + " es: " + Encoding.ASCII.GetString(data, 0, dataSize));
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

                    byte[] data = new byte[1024];
                    int dataSize = socket.Receive(data);
                    MessageBox.Show("El # de partidas ganadas de " + nickUsuario + " es: " + Encoding.ASCII.GetString(data, 0, dataSize));
                }
                catch (SocketException)
                {
                    MessageBox.Show("Error al realizar la consulta.");
                }
            }
            else if (consultaID == 3)
            {
                string partida_ID = textBox_datosConsulta.Text;

                try
                {
                    //Construimos la consulta para nuestro servidor (2/Usuario)
                    //El servidor devolverá el ganador del combate.
                    string consulta = ("3/" + partida_ID);
                    socket.Send(Encoding.ASCII.GetBytes(consulta));

                    byte[] data = new byte[1024];
                    int dataSize = socket.Receive(data);
                    string respuesta = Encoding.ASCII.GetString(data, 0, dataSize);
                    //El codigo recibido es Resultado/Ganador/Jugador1/Puntuacion1/Jugador2/Puntuacion2
                    string[] codigo = respuesta.Split('/');
                    MessageBox.Show("Jugador1: " + codigo[2] + " Puntuacion1: " + codigo[3]);
                    MessageBox.Show("Jugador2: " + codigo[4] + " Puntuacion2: " + codigo[5]);
                    MessageBox.Show("Ganador: "+codigo[1]);
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
