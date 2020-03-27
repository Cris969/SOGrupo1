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
using System.IO;

namespace Cliente
{
    public partial class F_login : Form
    {
        public F_login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = textBox_usuario.Text;
            string password = textBox_password.Text;

            //Control de errores.
            if ((usuario == "") || (password == ""))
            {
                MessageBox.Show("Por favor, rellene los campos.");
            }

            else
            {
                //Creamos el IPEndPoint con el ip y el puerto del servidor
                IPAddress direc = IPAddress.Parse("192.168.56.104");
                IPEndPoint ipep = new IPEndPoint(direc, 9050);
                //Creamos el socket
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep);
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }
                //Construimos el mensaje para nuestro servidor (0/Usuario/Password)
                //El servidor comprobará si nuestro usuario existe o no
                //En caso de que exista y la contrseña sea correcta devolverá un 1
                //En caso de algún fallo devolverá un -1.

                string mensaje = ("0/" + usuario + "/" + password);
                //Enviamos el mensaje al servidor
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string[] codigo = mensaje.Split('/');
                //Comprobamos que el resultdo sea el correcto o no.
                if (Convert.ToInt32(codigo[0]) == -1)
                {
                    MessageBox.Show("Contraseña o usuario incorrecto/s");
                }
                else
                {
                    MessageBox.Show("Bienvenido " + codigo[1]);
                    F_inicio f2 = new F_inicio();
                    f2.Show();
                }
                server.Shutdown(SocketShutdown.Both);
                server.Close();

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_registrarse_Click(object sender, EventArgs e)
        {
            string usuario = textBox_usuario.Text;
            string password = textBox_password.Text;
            string apodo = textBox_registrarse.Text;

            //Control de errores.
            if ((usuario == "") || (password == "") || (apodo == ""))
            {
                MessageBox.Show("Por favor, rellene los campos.");
            }

            else
            {
                //Creamos el IPEndPoint con el ip y el puerto del servidor
                IPAddress direc = IPAddress.Parse("192.168.56.104");
                IPEndPoint ipep = new IPEndPoint(direc, 9050);
                //Creamos el socket
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep);
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }
                //Construimos el mensaje para nuestro servidor (4/Usuario/Password/Apodo)
                //El servidor comprobará si nuestro usuario existe o no
                //En caso de que exista -1
                //En caso de algún fallo devolverá un -1.

                string mensaje = ("4/" + usuario + "/" + password+"/"+apodo);
                //Enviamos el mensaje al servidor
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //Comprobamos que el resultdo sea el correcto o no.
                if (Convert.ToInt32(mensaje) == -1)
                {
                    MessageBox.Show("Ya existe el usuario");
                }
                else
                {
                    MessageBox.Show("Registrado correctamente");
                    F_inicio f2 = new F_inicio();
                    f2.Show();
                }
                server.Shutdown(SocketShutdown.Both);
                server.Close();

            }
        }
    }
}
