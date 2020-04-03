namespace Cliente
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_Menu = new System.Windows.Forms.Panel();
            this.button_inicio_sesion = new System.Windows.Forms.Button();
            this.button_consultas = new System.Windows.Forms.Button();
            this.subpanel_menuConsultas = new System.Windows.Forms.Panel();
            this.button_mayor_puntuacion = new System.Windows.Forms.Button();
            this.button_partidas_ganadas = new System.Windows.Forms.Button();
            this.button_datos_partida = new System.Windows.Forms.Button();
            this.button_listar_conectados = new System.Windows.Forms.Button();
            this.button_salir = new System.Windows.Forms.Button();
            this.panel_inicioSesion = new System.Windows.Forms.Panel();
            this.textBox_usuario = new System.Windows.Forms.TextBox();
            this.textBox_contraseña = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel_registrarse = new System.Windows.Forms.LinkLabel();
            this.button_login = new System.Windows.Forms.Button();
            this.panel_estadoConexion = new System.Windows.Forms.Panel();
            this.label_estadoConexion = new System.Windows.Forms.Label();
            this.button_desconexion = new System.Windows.Forms.Button();
            this.panel_consultas = new System.Windows.Forms.Panel();
            this.textBox_datosConsulta = new System.Windows.Forms.TextBox();
            this.button_consultar = new System.Windows.Forms.Button();
            this.label_nickConsulta = new System.Windows.Forms.Label();
            this.label_IDpartidaConsulta = new System.Windows.Forms.Label();
            this.panel_listaConectados = new System.Windows.Forms.Panel();
            this.dataGridView_listaConectados = new System.Windows.Forms.DataGridView();
            this.button_actualizarConectados = new System.Windows.Forms.Button();
            this.button_cerrarLista = new System.Windows.Forms.Button();
            this.label_nickRegistro = new System.Windows.Forms.Label();
            this.textBox_nick = new System.Windows.Forms.TextBox();
            this.button_registrarse = new System.Windows.Forms.Button();
            this.label_títuloConsulta = new System.Windows.Forms.Label();
            this.panel_Menu.SuspendLayout();
            this.subpanel_menuConsultas.SuspendLayout();
            this.panel_inicioSesion.SuspendLayout();
            this.panel_estadoConexion.SuspendLayout();
            this.panel_consultas.SuspendLayout();
            this.panel_listaConectados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_listaConectados)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Menu
            // 
            this.panel_Menu.Controls.Add(this.button_salir);
            this.panel_Menu.Controls.Add(this.subpanel_menuConsultas);
            this.panel_Menu.Controls.Add(this.button_consultas);
            this.panel_Menu.Controls.Add(this.button_inicio_sesion);
            this.panel_Menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Menu.Location = new System.Drawing.Point(0, 0);
            this.panel_Menu.Name = "panel_Menu";
            this.panel_Menu.Size = new System.Drawing.Size(200, 544);
            this.panel_Menu.TabIndex = 0;
            // 
            // button_inicio_sesion
            // 
            this.button_inicio_sesion.BackColor = System.Drawing.Color.Gray;
            this.button_inicio_sesion.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_inicio_sesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_inicio_sesion.Location = new System.Drawing.Point(0, 0);
            this.button_inicio_sesion.Name = "button_inicio_sesion";
            this.button_inicio_sesion.Size = new System.Drawing.Size(200, 80);
            this.button_inicio_sesion.TabIndex = 1;
            this.button_inicio_sesion.Text = "Iniciar Sesión";
            this.button_inicio_sesion.UseVisualStyleBackColor = false;
            this.button_inicio_sesion.Click += new System.EventHandler(this.button_inicio_sesion_Click);
            // 
            // button_consultas
            // 
            this.button_consultas.BackColor = System.Drawing.Color.Silver;
            this.button_consultas.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_consultas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_consultas.Location = new System.Drawing.Point(0, 80);
            this.button_consultas.Name = "button_consultas";
            this.button_consultas.Size = new System.Drawing.Size(200, 80);
            this.button_consultas.TabIndex = 2;
            this.button_consultas.Text = "Consultas";
            this.button_consultas.UseVisualStyleBackColor = false;
            this.button_consultas.Click += new System.EventHandler(this.button_consultas_Click);
            // 
            // subpanel_menuConsultas
            // 
            this.subpanel_menuConsultas.Controls.Add(this.button_listar_conectados);
            this.subpanel_menuConsultas.Controls.Add(this.button_datos_partida);
            this.subpanel_menuConsultas.Controls.Add(this.button_partidas_ganadas);
            this.subpanel_menuConsultas.Controls.Add(this.button_mayor_puntuacion);
            this.subpanel_menuConsultas.Dock = System.Windows.Forms.DockStyle.Top;
            this.subpanel_menuConsultas.Location = new System.Drawing.Point(0, 160);
            this.subpanel_menuConsultas.Name = "subpanel_menuConsultas";
            this.subpanel_menuConsultas.Size = new System.Drawing.Size(200, 220);
            this.subpanel_menuConsultas.TabIndex = 1;
            // 
            // button_mayor_puntuacion
            // 
            this.button_mayor_puntuacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button_mayor_puntuacion.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_mayor_puntuacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_mayor_puntuacion.Location = new System.Drawing.Point(0, 0);
            this.button_mayor_puntuacion.Name = "button_mayor_puntuacion";
            this.button_mayor_puntuacion.Size = new System.Drawing.Size(200, 55);
            this.button_mayor_puntuacion.TabIndex = 2;
            this.button_mayor_puntuacion.Text = "Mayor puntuación de un jugador";
            this.button_mayor_puntuacion.UseVisualStyleBackColor = false;
            this.button_mayor_puntuacion.Click += new System.EventHandler(this.button_mayor_puntuacion_Click);
            // 
            // button_partidas_ganadas
            // 
            this.button_partidas_ganadas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button_partidas_ganadas.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_partidas_ganadas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_partidas_ganadas.Location = new System.Drawing.Point(0, 55);
            this.button_partidas_ganadas.Name = "button_partidas_ganadas";
            this.button_partidas_ganadas.Size = new System.Drawing.Size(200, 55);
            this.button_partidas_ganadas.TabIndex = 3;
            this.button_partidas_ganadas.Text = "Nº de partidas ganadas de un jugador";
            this.button_partidas_ganadas.UseVisualStyleBackColor = false;
            this.button_partidas_ganadas.Click += new System.EventHandler(this.button_partidas_ganadas_Click);
            // 
            // button_datos_partida
            // 
            this.button_datos_partida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button_datos_partida.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_datos_partida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_datos_partida.Location = new System.Drawing.Point(0, 110);
            this.button_datos_partida.Name = "button_datos_partida";
            this.button_datos_partida.Size = new System.Drawing.Size(200, 55);
            this.button_datos_partida.TabIndex = 4;
            this.button_datos_partida.Text = "Datos de una partida";
            this.button_datos_partida.UseVisualStyleBackColor = false;
            this.button_datos_partida.Click += new System.EventHandler(this.button_datos_partida_Click);
            // 
            // button_listar_conectados
            // 
            this.button_listar_conectados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button_listar_conectados.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_listar_conectados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_listar_conectados.Location = new System.Drawing.Point(0, 165);
            this.button_listar_conectados.Name = "button_listar_conectados";
            this.button_listar_conectados.Size = new System.Drawing.Size(200, 55);
            this.button_listar_conectados.TabIndex = 5;
            this.button_listar_conectados.Text = "Listar conectados";
            this.button_listar_conectados.UseVisualStyleBackColor = false;
            this.button_listar_conectados.Click += new System.EventHandler(this.button_listar_conectados_Click);
            // 
            // button_salir
            // 
            this.button_salir.BackColor = System.Drawing.Color.Gray;
            this.button_salir.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_salir.Location = new System.Drawing.Point(0, 380);
            this.button_salir.Name = "button_salir";
            this.button_salir.Size = new System.Drawing.Size(200, 80);
            this.button_salir.TabIndex = 3;
            this.button_salir.Text = "Salir";
            this.button_salir.UseVisualStyleBackColor = false;
            this.button_salir.Click += new System.EventHandler(this.button_salir_Click);
            // 
            // panel_inicioSesion
            // 
            this.panel_inicioSesion.Controls.Add(this.button_registrarse);
            this.panel_inicioSesion.Controls.Add(this.textBox_nick);
            this.panel_inicioSesion.Controls.Add(this.label_nickRegistro);
            this.panel_inicioSesion.Controls.Add(this.button_login);
            this.panel_inicioSesion.Controls.Add(this.linkLabel_registrarse);
            this.panel_inicioSesion.Controls.Add(this.label2);
            this.panel_inicioSesion.Controls.Add(this.label1);
            this.panel_inicioSesion.Controls.Add(this.textBox_contraseña);
            this.panel_inicioSesion.Controls.Add(this.textBox_usuario);
            this.panel_inicioSesion.Location = new System.Drawing.Point(200, 0);
            this.panel_inicioSesion.Name = "panel_inicioSesion";
            this.panel_inicioSesion.Size = new System.Drawing.Size(600, 215);
            this.panel_inicioSesion.TabIndex = 1;
            // 
            // textBox_usuario
            // 
            this.textBox_usuario.Location = new System.Drawing.Point(192, 30);
            this.textBox_usuario.Name = "textBox_usuario";
            this.textBox_usuario.Size = new System.Drawing.Size(100, 26);
            this.textBox_usuario.TabIndex = 0;
            // 
            // textBox_contraseña
            // 
            this.textBox_contraseña.Location = new System.Drawing.Point(192, 77);
            this.textBox_contraseña.Name = "textBox_contraseña";
            this.textBox_contraseña.Size = new System.Drawing.Size(100, 26);
            this.textBox_contraseña.TabIndex = 1;
            this.textBox_contraseña.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Usuario:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Contraseña:";
            // 
            // linkLabel_registrarse
            // 
            this.linkLabel_registrarse.AutoSize = true;
            this.linkLabel_registrarse.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.linkLabel_registrarse.Location = new System.Drawing.Point(129, 177);
            this.linkLabel_registrarse.Name = "linkLabel_registrarse";
            this.linkLabel_registrarse.Size = new System.Drawing.Size(294, 20);
            this.linkLabel_registrarse.TabIndex = 4;
            this.linkLabel_registrarse.TabStop = true;
            this.linkLabel_registrarse.Text = "¿Aún no tienes cuenta? Registrate aquí.";
            this.linkLabel_registrarse.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.linkLabel_registrarse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_registrarse_LinkClicked);
            // 
            // button_login
            // 
            this.button_login.Location = new System.Drawing.Point(321, 73);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(102, 34);
            this.button_login.TabIndex = 5;
            this.button_login.Text = "Login";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // panel_estadoConexion
            // 
            this.panel_estadoConexion.Controls.Add(this.button_desconexion);
            this.panel_estadoConexion.Controls.Add(this.label_estadoConexion);
            this.panel_estadoConexion.Location = new System.Drawing.Point(800, 0);
            this.panel_estadoConexion.Name = "panel_estadoConexion";
            this.panel_estadoConexion.Size = new System.Drawing.Size(378, 160);
            this.panel_estadoConexion.TabIndex = 2;
            this.panel_estadoConexion.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_estadoConexion_Paint);
            // 
            // label_estadoConexion
            // 
            this.label_estadoConexion.AutoSize = true;
            this.label_estadoConexion.Location = new System.Drawing.Point(89, 36);
            this.label_estadoConexion.Name = "label_estadoConexion";
            this.label_estadoConexion.Size = new System.Drawing.Size(51, 20);
            this.label_estadoConexion.TabIndex = 1;
            this.label_estadoConexion.Text = "label3";
            // 
            // button_desconexion
            // 
            this.button_desconexion.Location = new System.Drawing.Point(234, 98);
            this.button_desconexion.Name = "button_desconexion";
            this.button_desconexion.Size = new System.Drawing.Size(132, 43);
            this.button_desconexion.TabIndex = 2;
            this.button_desconexion.Text = "Desconectarse";
            this.button_desconexion.UseVisualStyleBackColor = true;
            this.button_desconexion.Click += new System.EventHandler(this.button_desconexion_Click);
            // 
            // panel_consultas
            // 
            this.panel_consultas.Controls.Add(this.label_títuloConsulta);
            this.panel_consultas.Controls.Add(this.label_IDpartidaConsulta);
            this.panel_consultas.Controls.Add(this.label_nickConsulta);
            this.panel_consultas.Controls.Add(this.button_consultar);
            this.panel_consultas.Controls.Add(this.textBox_datosConsulta);
            this.panel_consultas.Location = new System.Drawing.Point(200, 215);
            this.panel_consultas.Name = "panel_consultas";
            this.panel_consultas.Size = new System.Drawing.Size(600, 329);
            this.panel_consultas.TabIndex = 3;
            // 
            // textBox_datosConsulta
            // 
            this.textBox_datosConsulta.Location = new System.Drawing.Point(218, 63);
            this.textBox_datosConsulta.Name = "textBox_datosConsulta";
            this.textBox_datosConsulta.Size = new System.Drawing.Size(100, 26);
            this.textBox_datosConsulta.TabIndex = 0;
            // 
            // button_consultar
            // 
            this.button_consultar.Location = new System.Drawing.Point(385, 55);
            this.button_consultar.Name = "button_consultar";
            this.button_consultar.Size = new System.Drawing.Size(132, 43);
            this.button_consultar.TabIndex = 1;
            this.button_consultar.Text = "Consultar";
            this.button_consultar.UseVisualStyleBackColor = true;
            this.button_consultar.Click += new System.EventHandler(this.button_consultar_Click);
            // 
            // label_nickConsulta
            // 
            this.label_nickConsulta.AutoSize = true;
            this.label_nickConsulta.Location = new System.Drawing.Point(95, 66);
            this.label_nickConsulta.Name = "label_nickConsulta";
            this.label_nickConsulta.Size = new System.Drawing.Size(99, 20);
            this.label_nickConsulta.TabIndex = 2;
            this.label_nickConsulta.Text = "Nick usuario:";
            // 
            // label_IDpartidaConsulta
            // 
            this.label_IDpartidaConsulta.AutoSize = true;
            this.label_IDpartidaConsulta.Location = new System.Drawing.Point(95, 66);
            this.label_IDpartidaConsulta.Name = "label_IDpartidaConsulta";
            this.label_IDpartidaConsulta.Size = new System.Drawing.Size(105, 20);
            this.label_IDpartidaConsulta.TabIndex = 3;
            this.label_IDpartidaConsulta.Text = "ID de partida:";
            // 
            // panel_listaConectados
            // 
            this.panel_listaConectados.Controls.Add(this.button_cerrarLista);
            this.panel_listaConectados.Controls.Add(this.button_actualizarConectados);
            this.panel_listaConectados.Controls.Add(this.dataGridView_listaConectados);
            this.panel_listaConectados.Location = new System.Drawing.Point(800, 160);
            this.panel_listaConectados.Name = "panel_listaConectados";
            this.panel_listaConectados.Size = new System.Drawing.Size(378, 384);
            this.panel_listaConectados.TabIndex = 4;
            // 
            // dataGridView_listaConectados
            // 
            this.dataGridView_listaConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_listaConectados.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_listaConectados.Name = "dataGridView_listaConectados";
            this.dataGridView_listaConectados.RowHeadersWidth = 62;
            this.dataGridView_listaConectados.RowTemplate.Height = 28;
            this.dataGridView_listaConectados.Size = new System.Drawing.Size(378, 265);
            this.dataGridView_listaConectados.TabIndex = 0;
            // 
            // button_actualizarConectados
            // 
            this.button_actualizarConectados.Location = new System.Drawing.Point(24, 281);
            this.button_actualizarConectados.Name = "button_actualizarConectados";
            this.button_actualizarConectados.Size = new System.Drawing.Size(116, 38);
            this.button_actualizarConectados.TabIndex = 1;
            this.button_actualizarConectados.Text = "Actualizar";
            this.button_actualizarConectados.UseVisualStyleBackColor = true;
            this.button_actualizarConectados.Click += new System.EventHandler(this.button_actualizarConectados_Click);
            // 
            // button_cerrarLista
            // 
            this.button_cerrarLista.Location = new System.Drawing.Point(234, 281);
            this.button_cerrarLista.Name = "button_cerrarLista";
            this.button_cerrarLista.Size = new System.Drawing.Size(116, 38);
            this.button_cerrarLista.TabIndex = 2;
            this.button_cerrarLista.Text = "Cerrar";
            this.button_cerrarLista.UseVisualStyleBackColor = true;
            this.button_cerrarLista.Click += new System.EventHandler(this.button_cerrarLista_Click);
            // 
            // label_nickRegistro
            // 
            this.label_nickRegistro.AutoSize = true;
            this.label_nickRegistro.Location = new System.Drawing.Point(40, 121);
            this.label_nickRegistro.Name = "label_nickRegistro";
            this.label_nickRegistro.Size = new System.Drawing.Size(79, 20);
            this.label_nickRegistro.TabIndex = 6;
            this.label_nickRegistro.Text = "Nickname";
            // 
            // textBox_nick
            // 
            this.textBox_nick.Location = new System.Drawing.Point(192, 121);
            this.textBox_nick.Name = "textBox_nick";
            this.textBox_nick.Size = new System.Drawing.Size(100, 26);
            this.textBox_nick.TabIndex = 7;
            // 
            // button_registrarse
            // 
            this.button_registrarse.Location = new System.Drawing.Point(321, 117);
            this.button_registrarse.Name = "button_registrarse";
            this.button_registrarse.Size = new System.Drawing.Size(102, 34);
            this.button_registrarse.TabIndex = 8;
            this.button_registrarse.Text = "Registrarse";
            this.button_registrarse.UseVisualStyleBackColor = true;
            this.button_registrarse.Click += new System.EventHandler(this.button_registrarse_Click);
            // 
            // label_títuloConsulta
            // 
            this.label_títuloConsulta.AutoSize = true;
            this.label_títuloConsulta.Location = new System.Drawing.Point(129, 17);
            this.label_títuloConsulta.Name = "label_títuloConsulta";
            this.label_títuloConsulta.Size = new System.Drawing.Size(51, 20);
            this.label_títuloConsulta.TabIndex = 4;
            this.label_títuloConsulta.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 544);
            this.Controls.Add(this.panel_listaConectados);
            this.Controls.Add(this.panel_consultas);
            this.Controls.Add(this.panel_estadoConexion);
            this.Controls.Add(this.panel_inicioSesion);
            this.Controls.Add(this.panel_Menu);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel_Menu.ResumeLayout(false);
            this.subpanel_menuConsultas.ResumeLayout(false);
            this.panel_inicioSesion.ResumeLayout(false);
            this.panel_inicioSesion.PerformLayout();
            this.panel_estadoConexion.ResumeLayout(false);
            this.panel_estadoConexion.PerformLayout();
            this.panel_consultas.ResumeLayout(false);
            this.panel_consultas.PerformLayout();
            this.panel_listaConectados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_listaConectados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Menu;
        private System.Windows.Forms.Button button_salir;
        private System.Windows.Forms.Panel subpanel_menuConsultas;
        private System.Windows.Forms.Button button_listar_conectados;
        private System.Windows.Forms.Button button_datos_partida;
        private System.Windows.Forms.Button button_partidas_ganadas;
        private System.Windows.Forms.Button button_mayor_puntuacion;
        private System.Windows.Forms.Button button_consultas;
        private System.Windows.Forms.Button button_inicio_sesion;
        private System.Windows.Forms.Panel panel_inicioSesion;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.LinkLabel linkLabel_registrarse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_contraseña;
        private System.Windows.Forms.TextBox textBox_usuario;
        private System.Windows.Forms.Panel panel_estadoConexion;
        private System.Windows.Forms.Label label_estadoConexion;
        private System.Windows.Forms.Button button_desconexion;
        private System.Windows.Forms.Panel panel_consultas;
        private System.Windows.Forms.Label label_IDpartidaConsulta;
        private System.Windows.Forms.Label label_nickConsulta;
        private System.Windows.Forms.Button button_consultar;
        private System.Windows.Forms.TextBox textBox_datosConsulta;
        private System.Windows.Forms.Panel panel_listaConectados;
        private System.Windows.Forms.DataGridView dataGridView_listaConectados;
        private System.Windows.Forms.Button button_cerrarLista;
        private System.Windows.Forms.Button button_actualizarConectados;
        private System.Windows.Forms.TextBox textBox_nick;
        private System.Windows.Forms.Label label_nickRegistro;
        private System.Windows.Forms.Button button_registrarse;
        private System.Windows.Forms.Label label_títuloConsulta;
    }
}

