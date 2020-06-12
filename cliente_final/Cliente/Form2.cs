using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Cliente
{
    public partial class Form2 : Form
    {
        Tablero tablero;
        int dado;
        Random rnd;
        List<Jugador> jugadores;
        //Indica que jugador puede realizar su jugada
        int turno;
        string tu;
        int id_partida;
        Socket server;
        int color;
        int nueva_posicion;
        int ficha_movida;

        delegate void delegado_conMensaje(string[] mensaje);

        public Form2(string apodo, int id, Socket socket, int color)
        {
            InitializeComponent();
            this.tu = apodo;
            this.id_partida = id;
            this.server = socket;
            this.color = color;
            label_info_apodo.Text = "Sesion iniciada como:\n"+this.tu;
            label_info_id.Text = "#partida: " + this.id_partida;
        }

        private void identificar_jugador(string apodo)
        {
            int j = 0;
            bool encontrado = false;
            while (j<jugadores.Count && (encontrado == false))
            {
                if (jugadores[j].GetNombre() == apodo)
                    color = jugadores[j].GetColor();
                else
                    j++;
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            turno = 0;
            rnd = new Random();
            jugadores = new List<Jugador>();
            //Inicializar el tablero
            tablero = new Tablero();
            InicializarTablero(tablero);
            jugadores.Add(new Jugador("Cris969"));
            jugadores.ElementAt<Jugador>(0).SetColor(0);
            label1.Text = jugadores.ElementAt<Jugador>(0).GetNombre();
        }

        public void InicializarTablero(Tablero tablero)
        {
            tablero.AñadirCasilla(new Casilla(1,null,360,572,360-22,572,360+22,572));
            tablero.AñadirCasilla(new Casilla(2,null,360,547,360-22,547,360+22,547));
            tablero.AñadirCasilla(new Casilla(3,null,360,519,360-22,519,360+22,519));
            tablero.AñadirCasilla(new Casilla(4,null,360,493,360-22,493,360+22,493));
            tablero.AñadirCasilla(new Casilla(5,"Base",360,465,360-22,460,360+22,460));
            tablero.AñadirCasilla(new Casilla(6,null,360,438,360-22,438,360+22,438));
            tablero.AñadirCasilla(new Casilla(7,null,360,411,360-22,411,360+22,411));
            tablero.AñadirCasilla(new Casilla(8,null,360,385,360-22,385,360+22,385));
            tablero.AñadirCasilla(new Casilla(9,null,383,361,383,361-22,383,361+22));
            tablero.AñadirCasilla(new Casilla(10,null,411,361,411,361-22,411,361+22));
            tablero.AñadirCasilla(new Casilla(11,null,438,361,438,361-22,438,361+22));
            tablero.AñadirCasilla(new Casilla(12,"Seguro",464,361,464,361-22,464,361+22));
            tablero.AñadirCasilla(new Casilla(13,null,492,361,492,361-22,492,361+22));
            tablero.AñadirCasilla(new Casilla(14,null,519,361,519,361-22,519,361+22));
            tablero.AñadirCasilla(new Casilla(15,null,546,361,546,361-22,546,361+22));
            tablero.AñadirCasilla(new Casilla(16,null,572,361,572,361-22,572,361+22));
            tablero.AñadirCasilla(new Casilla(17,"Seguro",572,287,572,287-22,572,287+22));
            tablero.AñadirCasilla(new Casilla(18,null,572,215,572,215-22,572,215+22));
            tablero.AñadirCasilla(new Casilla(19,null,545,215,545,215-22,545,215+22));
            tablero.AñadirCasilla(new Casilla(20,null,519,215,519,215-22,519,215+22));
            tablero.AñadirCasilla(new Casilla(21,null,492,215,492,215-22,492,215+22));
            tablero.AñadirCasilla(new Casilla(22,"Base",465,215,465,215-22,465,215+22));
            tablero.AñadirCasilla(new Casilla(23,null,438,215,438,215-22,438,215+22));
            tablero.AñadirCasilla(new Casilla(24,null,411,215,411,215-22,411,215+22));
            tablero.AñadirCasilla(new Casilla(25,null,383,215,383,215-22,383,215+22));
            tablero.AñadirCasilla(new Casilla(26,null,357,190,357-22,190,357+22,190));
            tablero.AñadirCasilla(new Casilla(27,null,357,163,357-22,163,357+22,163));
            tablero.AñadirCasilla(new Casilla(28,null,357,136,357-22,136,357+22,136));
            tablero.AñadirCasilla(new Casilla(29,"Seguro",357,109,357-22,109,357+22,109));
            tablero.AñadirCasilla(new Casilla(30,null,357,82,357-22,82,357+22,82));
            tablero.AñadirCasilla(new Casilla(31,null,357,56,357-22,56,357+22,56));
            tablero.AñadirCasilla(new Casilla(32,null,357,28,357-22,28,357+22,28));
            tablero.AñadirCasilla(new Casilla(33,null,357,1,357-22,1,357+22,1));
            tablero.AñadirCasilla(new Casilla(34,"Seguro",286,1,286-22,1,286+22,1));
            tablero.AñadirCasilla(new Casilla(35,null,215,1,215-22,1,215+22,1));
            tablero.AñadirCasilla(new Casilla(36,null,215,28,215-22,28,215+22,28));
            tablero.AñadirCasilla(new Casilla(37,null,215,56,215-22,56,215+22,56));
            tablero.AñadirCasilla(new Casilla(38,null,215,82,215-22,82,215+22,82));
            tablero.AñadirCasilla(new Casilla(39,"Base",215,108,215-22,108,215+22,108));
            tablero.AñadirCasilla(new Casilla(40,null,215,136,215-22,136,215+22,136));
            tablero.AñadirCasilla(new Casilla(41,null,215,163,215-22,163,215+22,163));
            tablero.AñadirCasilla(new Casilla(42,null,215,190,215-22,190,215+22,190));
            tablero.AñadirCasilla(new Casilla(43,null,189,218,189,218-22,189,218+22));
            tablero.AñadirCasilla(new Casilla(44,null,162,218,162,218-22,162,218+22));
            tablero.AñadirCasilla(new Casilla(45,null,136,218,136,218-22,136,218+22));
            tablero.AñadirCasilla(new Casilla(46,"Seguro",107,218,107,218-22,107,218+22));
            tablero.AñadirCasilla(new Casilla(47,null,82,218,82,218-22,82,218+22));
            tablero.AñadirCasilla(new Casilla(48,null,54,218,54,218-22,54,218+22));
            tablero.AñadirCasilla(new Casilla(49,null,27,218,27,218-22,27,218+22));
            tablero.AñadirCasilla(new Casilla(50,null,0,218,0,218-22,0,218+22));
            tablero.AñadirCasilla(new Casilla(51,"Seguro",0,287,0,287-22,0,287+22));
            tablero.AñadirCasilla(new Casilla(52,null,0,359,0,359-22,0,359+22));
            tablero.AñadirCasilla(new Casilla(53,null,27,359,27,359-22,27,359+22));
            tablero.AñadirCasilla(new Casilla(54,null,55,359,55,359-22,55,359+22));
            tablero.AñadirCasilla(new Casilla(55,null,81,359,81,359-22,81,359+22));
            tablero.AñadirCasilla(new Casilla(56,"Base",109,359,109,359-22,109,359+22));
            tablero.AñadirCasilla(new Casilla(57,null,135,359,135,359-22,135,359+22));
            tablero.AñadirCasilla(new Casilla(58,null,163,359,163,359-22,163,359+22));
            tablero.AñadirCasilla(new Casilla(59,null,189,359,189,359-22,189,359+22));
            tablero.AñadirCasilla(new Casilla(60,null,218,385,218-22,385,218+22,385));
            tablero.AñadirCasilla(new Casilla(61,null,218,412,218-22,412,218+22,412));
            tablero.AñadirCasilla(new Casilla(62,null,218,440,218-22,400,218+22,400));
            tablero.AñadirCasilla(new Casilla(63,"Seguro",218,465,218-22,465,218+22,465));
            tablero.AñadirCasilla(new Casilla(64,null,218,493,218-22,493,218+22,493));
            tablero.AñadirCasilla(new Casilla(65,null,218,520,218-22,520,218+22,520));
            tablero.AñadirCasilla(new Casilla(66,null,218,546,218-22,546,218+22,546));
            tablero.AñadirCasilla(new Casilla(67,null,218,574,218-22,574,218+22,574));
            tablero.AñadirCasilla(new Casilla(68,"Seguro",286,574,286-22,574,286+22,574));
        }

        private void buttonDado_Click(object sender, EventArgs e)
        {
            if (this.turno == this.color)
            {
                dado = rnd.Next(1, 7);
                labelDado.Text = dado.ToString();
            }
            else
            {
                MessageBox.Show("No es tu turno");
            }
        }

        public void SetJugador(Jugador jugador,List<Jugador> lista)
        {
            lista.Add(jugador);
        }

        private void buttonTurno_Click(object sender, EventArgs e)
        {
            if (this.turno == this.color)
            {
                //Construimos mensaje con los cambios del tablero 4/5/id_partida/color/ficha/tirada
                string mensaje = "9/5/" + id_partida + "/" + color + "/" + ficha_movida + "/" + nueva_posicion + "\0";
                server.Send(Encoding.ASCII.GetBytes(mensaje));
            }
            else
            {
                MessageBox.Show("No es tu turno");
            }
        }

        #region //CLICKS FICHAS
        private void botonAzul1_Click(object sender, EventArgs e)
        {
            if(dado == 5)
            {
                botonAzul1.Location = new Point(tablero.GetCasilla(22).GetX(),tablero.GetCasilla(22).GetY());
            }
            else
            {
                
            }
        }

        private void botonAzul2_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonAzul2.Location = new Point(tablero.GetCasilla(22).GetX(), tablero.GetCasilla(22).GetY());
            }
        }

        private void botonAzul3_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonAzul3.Location = new Point(tablero.GetCasilla(22).GetX(), tablero.GetCasilla(22).GetY());
            }
        }

        private void botonAzul4_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonAzul4.Location = new Point(tablero.GetCasilla(22).GetX(), tablero.GetCasilla(22).GetY());
            }
        }

        private void botonRojo1_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonRojo1.Location = new Point(tablero.GetCasilla(39).GetX(), tablero.GetCasilla(39).GetY());
            }
        }

        private void botonRojo2_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonRojo2.Location = new Point(tablero.GetCasilla(39).GetX(), tablero.GetCasilla(39).GetY());
            }
        }

        private void botonRojo3_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonRojo3.Location = new Point(tablero.GetCasilla(39).GetX(), tablero.GetCasilla(39).GetY());
            }
        }

        private void botonRojo4_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonRojo4.Location = new Point(tablero.GetCasilla(39).GetX(), tablero.GetCasilla(39).GetY());
            }
        }

        private void botonVerde1_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonVerde1.Location = new Point(tablero.GetCasilla(56).GetX(), tablero.GetCasilla(56).GetY());
            }
        }

        private void botonVerde2_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonVerde2.Location = new Point(tablero.GetCasilla(56).GetX(), tablero.GetCasilla(56).GetY());
            }
        }

        private void botonVerde3_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonVerde3.Location = new Point(tablero.GetCasilla(56).GetX(), tablero.GetCasilla(56).GetY());
            }
        }

        private void botonVerde4_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonVerde4.Location = new Point(tablero.GetCasilla(56).GetX(), tablero.GetCasilla(56).GetY());
            }
        }

        private void botonAmarillo1_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonAmarillo1.Location = new Point(tablero.GetCasilla(5).GetX(), tablero.GetCasilla(5).GetY());
            }
        }

        private void botonAmarillo2_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonAmarillo2.Location = new Point(tablero.GetCasilla(5).GetX(), tablero.GetCasilla(5).GetY());
            }
        }

        private void botonAmarillo3_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonAmarillo3.Location = new Point(tablero.GetCasilla(5).GetX(), tablero.GetCasilla(5).GetY());
            }
        }

        private void botonAmarillo4_Click(object sender, EventArgs e)
        {
            if (dado == 5)
            {
                botonAmarillo4.Location = new Point(tablero.GetCasilla(5).GetX(), tablero.GetCasilla(5).GetY());
            }
        }
        #endregion

        #region //FUNCIONES PARA CHAT
        private void button_send_msg_Click(object sender, EventArgs e)
        {
            //Construimos mensaje para el servidor 9/6/id_partida/mensaje
            string mensaje = "9/6/"+ id_partida+"/"+textBox_msg.Text;
            server.Send(Encoding.ASCII.GetBytes(mensaje));
        }

        public void mensaje_chat(string [] mensaje)
        {
            delegado_conMensaje mensaje_chat = new delegado_conMensaje(poner_mensaje);
            listBox1_msg.Invoke(mensaje_chat, new object[] { mensaje });
        }

        private void poner_mensaje(string [] mensaje)
        {
            // 9/6/id_partida/host/mensaje

            if (mensaje[3] == tu)
            {
                listBox1_msg.Items.Add("Yo: " + mensaje[4]);
            }
            else
                listBox1_msg.Items.Add(mensaje[3] + ": " + mensaje[4]);
        }

        #endregion

        public int get_id_partida()
        {
            return this.id_partida;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Construimos el mensaje pare el servidor para que nos quite de la partida
            string mensaje = "9/0/"+id_partida;
            server.Send(Encoding.ASCII.GetBytes(mensaje));
        }

        public void dar_tirada(string [] mensaje)
        {
            delegado_conMensaje tirada = new delegado_conMensaje(nueva_tirada);
            this.Invoke(tirada, new object[] { mensaje});
        }

        private void nueva_tirada (string [] mensaje)
        {
            //9/5/id_partida/color/ficha/nueva_pos

            int color_tirada = Convert.ToInt32(mensaje[3]);
            int ficha_tirada = Convert.ToInt32(mensaje[4]);
            int nueva_pos_tirada = Convert.ToInt32(mensaje[5]);

            int j = 0;
            bool encontrado = false;

            while ((j<jugadores.Count) && (encontrado==false)){
                if (jugadores[j].GetColor() == color_tirada)
                    encontrado = true;
                else
                    j++;
            }

            if (encontrado == true)
            {
                //jugadores[j].GetFichas()[ficha_tirada].SetPos(nueva_pos_tirada);
            }


        }

    }
}
