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
        #region //VARIABLES Y OBJETOS GLOBALES
        Socket socket;
        IPEndPoint remoteEP;
        Thread atender;

        int id_partida;
        int color;


        delegate void delegado_vacio();
        delegate void delegado_conMensaje(string[] mensaaje);

        List<Form2> formularios_partida = new List<Form2>();

        bool conectado = false;
        string usuario;
        string apodo;
        #endregion

        public Form1()
        {
            InitializeComponent();
            estado_conexion();
            panel_inicioSesion.Visible = false;
            panel_listaConectados.Visible = false;
            label_nickRegistro.Visible = false;
            textBox_nick.Visible = false;
            button_registrarse.Visible = false;
        }

        #region //FUNCION DE ATENCION AL SERVIDOR
        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] data = new byte[80];
                socket.Receive(data);
                string[] mensaje = Encoding.ASCII.GetString(data).Split('\0');
                string [] trozoMensaje=mensaje[0].Split('/');
                int codigo = Convert.ToInt32(trozoMensaje[0]);

                switch (codigo)
                {
                    case 0: //Respuesta al login

                        delegado_vacio logeo;
                        // 0/1/apodo -> login correcto
                        // 0/-1 -> login erróneo
                        if (Convert.ToInt32(trozoMensaje[1]) == -1)
                        {
                            logeo = new delegado_vacio(logeo_incorrecto);
                            this.Invoke(logeo);

                        }
                        else
                        {
                            apodo = Convert.ToString(trozoMensaje[2]);
                            logeo = new delegado_vacio(logeo_correcto);
                            label_info_apodo.Invoke(logeo);
                        }
                        break;

                    case 4: //Respuesta al registro

                        delegado_vacio registro;
                        // 4/1 -> registro correcto
                        // 4/0 -> error en el registro
                        if (Convert.ToInt32(trozoMensaje[1]) == 1)
                        {
                            registro = new delegado_vacio(registro_correcto);
                            Invoke(registro);
                        }
                        else
                        {
                            registro = new delegado_vacio(registro_incorrecto);
                            this.Invoke(registro);
                        }
                        break;

                    case 6: //Notificación de cambio en lista de conectados

                        delegado_conMensaje notificacion = new delegado_conMensaje(ListarConectados);
                        this.Invoke(notificacion, new object[] { trozoMensaje });
                        break;

                    case 8: //Respuesta a la peticion de eliminacion de usuario

                        delegado_vacio elminiar_usuario;
                        //8/1 -> eliminacion correcta
                        //8/0 -> eliminacion incorrecta
                        if (Convert.ToInt32(trozoMensaje[1]) == 1)
                        {
                            elminiar_usuario = new delegado_vacio(eliminacion_correcta);
                            this.Invoke(elminiar_usuario);
                        }
                        else if (Convert.ToInt32(trozoMensaje[1]) == 0)
                            MessageBox.Show("Error al eliminar la cuenta");
                        break;

                    case 9: //Mensaje relacionado con partida

                        // 9/tipo de mensaje/...
                        //Volvemos a analizar la subpeticion
                        int subcodigo = Convert.ToInt32(trozoMensaje[1]);
                        string respuesta;

                        switch (subcodigo)
                        {
                            case 1: //Inicio de partida

                                // 9/1/idChat/1/color -> el chat se inicia
                                // 9/1/idChat/0 -> el chat no se iniciara
                                id_partida = Convert.ToInt32(trozoMensaje[2]);
                                color = Convert.ToInt32(trozoMensaje[4]);
                                if (Convert.ToInt32(trozoMensaje[3]) == 1)
                                {
                                    //Ponemos en marcha el thread que ejecutara el formulario
                                    ThreadStart chat = delegate { crear_formulario(id_partida, color); };
                                    Thread t = new Thread(chat);
                                    t.Start();

                                }


                                break;
                            case 2: //Invitacion a partida

                                //9/2/host/idPartida
                                string host = trozoMensaje[2];
                                id_partida = Convert.ToInt32(trozoMensaje[3]);

                                DialogResult dr1 = MessageBox.Show(host + " te invita a jugar. ¿Aceptar invitación?", "ACEPTAR INVITACIÓN", MessageBoxButtons.YesNo);
                                switch (dr1)
                                {
                                    case DialogResult.No:
                                        //Construimos la respuesta para el servidor: 7/2/idChat/0
                                        respuesta = ("9/2/" + id_partida + "/0\0");
                                        socket.Send(Encoding.ASCII.GetBytes(respuesta));
                                        break;
                                    case DialogResult.Yes:
                                        //Construimos la respuesta para el servidor: 7/2/idChat/1
                                        respuesta = ("9/2/" + id_partida + "/1\0");
                                        socket.Send(Encoding.ASCII.GetBytes(respuesta));
                                        break;
                                }
                                break;

                            case 5: //Notificacion con movimiento

                                // 9/5/id_partida/color/ficha/nueva_pos
                                int pos = posicion_formulario(formularios_partida, Convert.ToInt32(trozoMensaje[2]));
                                formularios_partida[pos].dar_tirada(trozoMensaje); //Le damos el mensaje al formulario 2
                                break;

                            case 6: //Mensaje para chat 9/6/id_partida/mensaje
                                int posicion = posicion_formulario(formularios_partida, Convert.ToInt32(trozoMensaje[2]));
                                formularios_partida[posicion].mensaje_chat(trozoMensaje); //A traves de la funcion mensaje le pasamos la info al formulario del chat
                                break;

                        }

                        break;

                    default:
                        MessageBox.Show("Mensaje recibido erróneo.");
                        break;
                        

                }
            }
        }
        #endregion

        #region //CONEXION Y DESCONEXION
        private void Conexion()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            remoteEP = new IPEndPoint(IPAddress.Parse("147.83.117.22"), 50052);
            try
            {
                socket.Connect(remoteEP);
                conectado = true;
                estado_conexion();

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
                //Construimos la consulta para el servidor (5/apodo)
                //para informar de la desconexión al servidor.
                string consulta = "5/";
                socket.Send(Encoding.ASCII.GetBytes(consulta));

                //Cerramos la conexión
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

                //Cerramos el thread que atiende los mensajes del servidor
                atender.Abort();

                conectado = false;
                EsconderSubMenu();
                estado_conexion();
                label_info_apodo.Visible = false;
            }

            catch (SocketException)
            {
                MessageBox.Show("Unable to disconnect to server.");
            }

        }
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
                    estado_conexion();
                }

                catch (SocketException)
                {
                    MessageBox.Show("Error al deconectar.");
                    estado_conexion();
                }
            }
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
                    string consulta = ("0/" + usuario + "/" + password);
                    socket.Send(Encoding.ASCII.GetBytes(consulta));
                    panel_inicioSesion.Visible = false;
                    vaciar_textBox();
                }
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
                apodo = textBox_nick.Text;
                Conexion();
                //Construimos la consulta para nuestro servidor (4/Usuario/Contraseña/Nick)
                //El servidor devolverá 1 si se ha registrado con éxito y -1 si se ha producido algún error.
                string consulta = ("4/" + textBox_usuario.Text + "/" + textBox_contraseña.Text + "/" + textBox_nick.Text);
                socket.Send(Encoding.ASCII.GetBytes(consulta));
                vaciar_textBox();
            }
        }
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
                    estado_conexion();
                    this.Close();
                }

                catch (SocketException)
                {
                    MessageBox.Show("Error al desconectar.");
                    this.Close();
                }
            }
        }
        private void button_eliminar_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region //FUNCIONES PARA DELEGADOS
        private void logeo_correcto()
        {
            label_info_apodo.Text = "Sesión iniciada como:\n"+apodo;
            label_info_apodo.Visible = true;
        }
        private void logeo_incorrecto()
        {
            Desconexion();
            MessageBox.Show("Credenciales incorrectas");

        }
        private void registro_incorrecto()
        {
            MessageBox.Show("Error al realizar el registro");
            Desconexion();
        }
        private void registro_correcto()
        {
            label_nickRegistro.Visible = false;
            textBox_nick.Visible = false;
            button_registrarse.Visible = false;
            button_login.Visible = true;
            panel_inicioSesion.Visible = false;
            estado_conexion();
            MessageBox.Show("Registro realizado");

        }
        private void ListarConectados(string[] trozoMensaje)
        {
            panel_listaConectados.Visible = true;

            dataGridView_listaConectados.RowCount = Convert.ToInt32(trozoMensaje[1]);
            dataGridView_listaConectados.ColumnCount = 1;
            for (int i = 0; i < Convert.ToInt32(trozoMensaje[1]); i++)
            {
                dataGridView_listaConectados[0, i].Value = trozoMensaje[i + 2];
            }
        }
        private void eliminacion_correcta()
        {
            MessageBox.Show("Eliminacion correcta.");
            Desconexion();
        }
        #endregion

        #region //MENUS Y BOTONES SUPERFICIALES
        private void EsconderSubMenu()
        {
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
        private void ocultar_alInicio()
        {
            panel_inicioSesion.Visible = false;
            panel_estadoConexion.Visible = false;
            panel_listaConectados.Visible = false;
            label_nickRegistro.Visible = false;
            textBox_nick.Visible = false;
            button_registrarse.Visible = false;
            button_eliminar.Visible = false;
        }
        private void button_inicio_sesion_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panel_inicioSesion);
            label_nickRegistro.Visible = false;
            textBox_nick.Visible = false;
            button_registrarse.Visible = false;
            button_eliminar.Visible = false;
            button_login.Visible = true;
        }
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
        private void vaciar_textBox()
        {
            textBox_contraseña.Text = "";
            textBox_nick.Text = "";
            textBox_usuario.Text = "";
        }
        private void button_eliminar_Click_1(object sender, EventArgs e)
        {
            DialogResult dr2 = MessageBox.Show("Va a eliminar su cunta y todos su datos ¿Desea continuar?", "ELIMINAR CUENTA", MessageBoxButtons.YesNo);

            if (dr2 == DialogResult.Yes)
            {
                string consulta = ("8/" + textBox_usuario.Text + "/" + textBox_nick.Text + "/" + textBox_contraseña.Text + "\0");
                socket.Send(Encoding.ASCII.GetBytes(consulta));
                vaciar_textBox();
            }
            else if (dr2 == DialogResult.No)
            {
                button_eliminar.Visible = false;
                textBox_nick.Visible = false;
                label_nickRegistro.Visible = false;
                vaciar_textBox();
            }
        }
        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (conectado == true)
            {
                button_eliminar.Visible = true;
                textBox_nick.Visible = true;
                label_nickRegistro.Visible = true;
                button_login.Visible = false;
                MessageBox.Show("Para darse de baja del sistema introduzca de nuevo sus credenciales.");
            }

            else
                MessageBox.Show("Para darse de baja del sistema, primero debe iniciar sesión");
        }
        #endregion

        private void button_crearPartida_Click(object sender, EventArgs e) //Se encia peticion al servidor para crear una partida con los seleccionados
        {
            int j = 0;
            string[] seleccionados = new string[100];
            foreach (DataGridViewCell item in dataGridView_listaConectados.SelectedCells)
            {
                seleccionados[j] = Convert.ToString(item.Value);
                j++;
            }

            try
            {
                //Construimos la consulta para nuestro servidor 
                string peticion = ("9/1/" + j);
                for (int k = 0; k < j; k++)
                {
                    peticion = (peticion + "/" + seleccionados[k]);
                }
                socket.Send(Encoding.ASCII.GetBytes(peticion));
            }
            catch (SocketException)
            {
                MessageBox.Show("Error al enviar la petición.");
            }
        }

        private void estado_conexion() //Procedimiento para informar sobre el estado de la conexion
        {
            if (conectado == false)
            {
                label_estadoConexion.Text = "DESCONECTADO";
                pictureBox_estado.Image = Image.FromFile("rojo.png");
                pictureBox_estado.SizeMode = PictureBoxSizeMode.StretchImage;
                button_desconexion.Visible = false;
                panel_listaConectados.Visible = false;
            }
            else if (conectado == true)
            {
                label_estadoConexion.Text = "CONECTADO";
                pictureBox_estado.Image = Image.FromFile("verde.png");
                pictureBox_estado.SizeMode = PictureBoxSizeMode.StretchImage;
                button_desconexion.Visible = true;
            }
        }

        private void crear_formulario(int id, int color) //Se crea un nuevo formulario para chat
        {
            Form2 f = new Form2(apodo, id, socket, color);
            formularios_partida.Add(f);
            f.ShowDialog();
        }

        private int posicion_formulario(List<Form2> formularios, int id) //Se devuelve la posicion del formulario con el id proporcionado
        {
            int j = 0;
            bool encontrado = false;

            while ((j < formularios.Count) && (encontrado == false))
            {
                if (formularios[j].get_id_partida() == id)
                    return j;
                else
                    j++;
            }
            return -1;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (conectado == true)
            {
                try
                {
                    Desconexion();
                    estado_conexion();
                }

                catch (SocketException)
                {
                    MessageBox.Show("Error al desconectar.");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
