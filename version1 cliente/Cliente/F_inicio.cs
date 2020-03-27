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
    public partial class F_inicio : Form
    {
        public F_inicio()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            F_datosPartida f5 = new F_datosPartida();
            f5.Show();
        }

        private void label_consulta2_Click(object sender, EventArgs e)
        {

        }

        private void label_consulta1_Click(object sender, EventArgs e)
        {

        }

        private void F_inicio_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button_consulta1_Click(object sender, EventArgs e)
        {
            F_mayorPuntuacion f3 = new F_mayorPuntuacion();
            f3.Show();
        }

        private void button_consulta3_Click(object sender, EventArgs e)
        {
            F_partidasGanadas f4 = new F_partidasGanadas();
            f4.Show();
        }
    }
}
