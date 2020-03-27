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
    public partial class F_partidasGanadas : Form
    {
        public F_partidasGanadas()
        {
            InitializeComponent();
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

            //Construimos la consulta para nuestro servidor (2/Usuario)
            //El servidor devolverá el # de partidas ganadas del usuario.

            string mensaje = ("2/" + usuario);
            //Enviamos al servidor la peticion del usuario
            //Enviamos al servidor un vector de bytes
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            if (Convert.ToInt32(mensaje) == -1)
            {
                MessageBox.Show("No existe el jugador");
            }
            else
            {
                MessageBox.Show("El número de partidas ganadas por " + usuario + " es " + mensaje);
            }
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
    }
}
