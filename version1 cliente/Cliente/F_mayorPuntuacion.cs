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
    public partial class F_mayorPuntuacion : Form
    {
        public F_mayorPuntuacion()
        {
            InitializeComponent();
        }

        private void F_mayorPuntuacion_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
            string usuario = textBox1.Text;
            //Construimos el mensaje para nuestro servidor (1/Apodo del jugador)
            //El servidor devolverá la mayor puntuación del jugador.
            string mensaje = ("1/" + usuario);
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            string[] codigo = mensaje.Split('/');
            if (Convert.ToInt32(codigo[0]) == -1)
            {
                MessageBox.Show("El jugador introducido no existe o no ha participado en ningún combate");
            }
            else
            {
                MessageBox.Show("La mayor puntuación de " + usuario + " es " + mensaje);
            }
            server.Shutdown(SocketShutdown.Both);
            server.Close();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        
    }
}
