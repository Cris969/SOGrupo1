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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel_Menu = new System.Windows.Forms.Panel();
            this.label_info_apodo = new System.Windows.Forms.Label();
            this.button_salir = new System.Windows.Forms.Button();
            this.button_consultas = new System.Windows.Forms.Button();
            this.button_inicio_sesion = new System.Windows.Forms.Button();
            this.panel_estadoConexion = new System.Windows.Forms.Panel();
            this.pictureBox_estado = new System.Windows.Forms.PictureBox();
            this.label_estadoConexion = new System.Windows.Forms.Label();
            this.button_desconexion = new System.Windows.Forms.Button();
            this.panel_inicioSesion = new System.Windows.Forms.Panel();
            this.button_eliminar = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button_registrarse = new System.Windows.Forms.Button();
            this.textBox_nick = new System.Windows.Forms.TextBox();
            this.label_nickRegistro = new System.Windows.Forms.Label();
            this.button_login = new System.Windows.Forms.Button();
            this.linkLabel_registrarse = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_contraseña = new System.Windows.Forms.TextBox();
            this.textBox_usuario = new System.Windows.Forms.TextBox();
            this.panel_listaConectados = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.button_crearPartida = new System.Windows.Forms.Button();
            this.dataGridView_listaConectados = new System.Windows.Forms.DataGridView();
            this.panel_Menu.SuspendLayout();
            this.panel_estadoConexion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_estado)).BeginInit();
            this.panel_inicioSesion.SuspendLayout();
            this.panel_listaConectados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_listaConectados)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Menu
            // 
            this.panel_Menu.Controls.Add(this.label_info_apodo);
            this.panel_Menu.Controls.Add(this.button_salir);
            this.panel_Menu.Controls.Add(this.button_consultas);
            this.panel_Menu.Controls.Add(this.button_inicio_sesion);
            this.panel_Menu.Controls.Add(this.panel_estadoConexion);
            this.panel_Menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Menu.Location = new System.Drawing.Point(0, 0);
            this.panel_Menu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel_Menu.Name = "panel_Menu";
            this.panel_Menu.Size = new System.Drawing.Size(133, 354);
            this.panel_Menu.TabIndex = 0;
            // 
            // label_info_apodo
            // 
            this.label_info_apodo.AutoSize = true;
            this.label_info_apodo.Location = new System.Drawing.Point(2, 158);
            this.label_info_apodo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_info_apodo.Name = "label_info_apodo";
            this.label_info_apodo.Size = new System.Drawing.Size(0, 13);
            this.label_info_apodo.TabIndex = 5;
            // 
            // button_salir
            // 
            this.button_salir.BackColor = System.Drawing.Color.Gray;
            this.button_salir.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_salir.Location = new System.Drawing.Point(0, 104);
            this.button_salir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_salir.Name = "button_salir";
            this.button_salir.Size = new System.Drawing.Size(133, 52);
            this.button_salir.TabIndex = 3;
            this.button_salir.Text = "Salir";
            this.button_salir.UseVisualStyleBackColor = false;
            this.button_salir.Click += new System.EventHandler(this.button_salir_Click);
            // 
            // button_consultas
            // 
            this.button_consultas.BackColor = System.Drawing.Color.Silver;
            this.button_consultas.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_consultas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_consultas.Location = new System.Drawing.Point(0, 52);
            this.button_consultas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_consultas.Name = "button_consultas";
            this.button_consultas.Size = new System.Drawing.Size(133, 52);
            this.button_consultas.TabIndex = 2;
            this.button_consultas.Text = "Consultas";
            this.button_consultas.UseVisualStyleBackColor = false;
            // 
            // button_inicio_sesion
            // 
            this.button_inicio_sesion.BackColor = System.Drawing.Color.Gray;
            this.button_inicio_sesion.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_inicio_sesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_inicio_sesion.Location = new System.Drawing.Point(0, 0);
            this.button_inicio_sesion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_inicio_sesion.Name = "button_inicio_sesion";
            this.button_inicio_sesion.Size = new System.Drawing.Size(133, 52);
            this.button_inicio_sesion.TabIndex = 1;
            this.button_inicio_sesion.Text = "Iniciar Sesión";
            this.button_inicio_sesion.UseVisualStyleBackColor = false;
            this.button_inicio_sesion.Click += new System.EventHandler(this.button_inicio_sesion_Click);
            // 
            // panel_estadoConexion
            // 
            this.panel_estadoConexion.Controls.Add(this.pictureBox_estado);
            this.panel_estadoConexion.Controls.Add(this.label_estadoConexion);
            this.panel_estadoConexion.Controls.Add(this.button_desconexion);
            this.panel_estadoConexion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_estadoConexion.Location = new System.Drawing.Point(0, 199);
            this.panel_estadoConexion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel_estadoConexion.Name = "panel_estadoConexion";
            this.panel_estadoConexion.Size = new System.Drawing.Size(133, 155);
            this.panel_estadoConexion.TabIndex = 2;
            // 
            // pictureBox_estado
            // 
            this.pictureBox_estado.Location = new System.Drawing.Point(14, 21);
            this.pictureBox_estado.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox_estado.Name = "pictureBox_estado";
            this.pictureBox_estado.Size = new System.Drawing.Size(109, 95);
            this.pictureBox_estado.TabIndex = 5;
            this.pictureBox_estado.TabStop = false;
            // 
            // label_estadoConexion
            // 
            this.label_estadoConexion.AutoSize = true;
            this.label_estadoConexion.Location = new System.Drawing.Point(20, 6);
            this.label_estadoConexion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_estadoConexion.Name = "label_estadoConexion";
            this.label_estadoConexion.Size = new System.Drawing.Size(35, 13);
            this.label_estadoConexion.TabIndex = 1;
            this.label_estadoConexion.Text = "label3";
            // 
            // button_desconexion
            // 
            this.button_desconexion.Location = new System.Drawing.Point(43, 120);
            this.button_desconexion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_desconexion.Name = "button_desconexion";
            this.button_desconexion.Size = new System.Drawing.Size(88, 21);
            this.button_desconexion.TabIndex = 2;
            this.button_desconexion.Text = "Desconectarse";
            this.button_desconexion.UseVisualStyleBackColor = true;
            this.button_desconexion.Click += new System.EventHandler(this.button_desconexion_Click);
            // 
            // panel_inicioSesion
            // 
            this.panel_inicioSesion.Controls.Add(this.button_eliminar);
            this.panel_inicioSesion.Controls.Add(this.linkLabel1);
            this.panel_inicioSesion.Controls.Add(this.button_registrarse);
            this.panel_inicioSesion.Controls.Add(this.textBox_nick);
            this.panel_inicioSesion.Controls.Add(this.label_nickRegistro);
            this.panel_inicioSesion.Controls.Add(this.button_login);
            this.panel_inicioSesion.Controls.Add(this.linkLabel_registrarse);
            this.panel_inicioSesion.Controls.Add(this.label2);
            this.panel_inicioSesion.Controls.Add(this.label1);
            this.panel_inicioSesion.Controls.Add(this.textBox_contraseña);
            this.panel_inicioSesion.Controls.Add(this.textBox_usuario);
            this.panel_inicioSesion.Location = new System.Drawing.Point(133, 0);
            this.panel_inicioSesion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel_inicioSesion.Name = "panel_inicioSesion";
            this.panel_inicioSesion.Size = new System.Drawing.Size(422, 156);
            this.panel_inicioSesion.TabIndex = 1;
            // 
            // button_eliminar
            // 
            this.button_eliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_eliminar.Location = new System.Drawing.Point(195, 131);
            this.button_eliminar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_eliminar.Name = "button_eliminar";
            this.button_eliminar.Size = new System.Drawing.Size(68, 17);
            this.button_eliminar.TabIndex = 10;
            this.button_eliminar.Text = "Eliminar";
            this.button_eliminar.UseVisualStyleBackColor = true;
            this.button_eliminar.Click += new System.EventHandler(this.button_eliminar_Click_1);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.linkLabel1.Location = new System.Drawing.Point(126, 131);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(73, 13);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Darse de baja";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked_1);
            // 
            // button_registrarse
            // 
            this.button_registrarse.Location = new System.Drawing.Point(214, 76);
            this.button_registrarse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_registrarse.Name = "button_registrarse";
            this.button_registrarse.Size = new System.Drawing.Size(68, 22);
            this.button_registrarse.TabIndex = 8;
            this.button_registrarse.Text = "Registrarse";
            this.button_registrarse.UseVisualStyleBackColor = true;
            this.button_registrarse.Click += new System.EventHandler(this.button_registrarse_Click);
            // 
            // textBox_nick
            // 
            this.textBox_nick.Location = new System.Drawing.Point(128, 79);
            this.textBox_nick.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_nick.Name = "textBox_nick";
            this.textBox_nick.Size = new System.Drawing.Size(68, 20);
            this.textBox_nick.TabIndex = 7;
            // 
            // label_nickRegistro
            // 
            this.label_nickRegistro.AutoSize = true;
            this.label_nickRegistro.Location = new System.Drawing.Point(27, 79);
            this.label_nickRegistro.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_nickRegistro.Name = "label_nickRegistro";
            this.label_nickRegistro.Size = new System.Drawing.Size(55, 13);
            this.label_nickRegistro.TabIndex = 6;
            this.label_nickRegistro.Text = "Nickname";
            // 
            // button_login
            // 
            this.button_login.Location = new System.Drawing.Point(214, 47);
            this.button_login.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(68, 22);
            this.button_login.TabIndex = 5;
            this.button_login.Text = "Login";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // linkLabel_registrarse
            // 
            this.linkLabel_registrarse.AutoSize = true;
            this.linkLabel_registrarse.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.linkLabel_registrarse.Location = new System.Drawing.Point(73, 104);
            this.linkLabel_registrarse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel_registrarse.Name = "linkLabel_registrarse";
            this.linkLabel_registrarse.Size = new System.Drawing.Size(199, 13);
            this.linkLabel_registrarse.TabIndex = 4;
            this.linkLabel_registrarse.TabStop = true;
            this.linkLabel_registrarse.Text = "¿Aún no tienes cuenta? Registrate aquí.";
            this.linkLabel_registrarse.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.linkLabel_registrarse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_registrarse_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Contraseña:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Usuario:";
            // 
            // textBox_contraseña
            // 
            this.textBox_contraseña.Location = new System.Drawing.Point(128, 50);
            this.textBox_contraseña.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_contraseña.Name = "textBox_contraseña";
            this.textBox_contraseña.Size = new System.Drawing.Size(68, 20);
            this.textBox_contraseña.TabIndex = 1;
            this.textBox_contraseña.UseSystemPasswordChar = true;
            // 
            // textBox_usuario
            // 
            this.textBox_usuario.Location = new System.Drawing.Point(128, 19);
            this.textBox_usuario.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_usuario.Name = "textBox_usuario";
            this.textBox_usuario.Size = new System.Drawing.Size(68, 20);
            this.textBox_usuario.TabIndex = 0;
            // 
            // panel_listaConectados
            // 
            this.panel_listaConectados.Controls.Add(this.label3);
            this.panel_listaConectados.Controls.Add(this.button_crearPartida);
            this.panel_listaConectados.Controls.Add(this.dataGridView_listaConectados);
            this.panel_listaConectados.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_listaConectados.Location = new System.Drawing.Point(607, 0);
            this.panel_listaConectados.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel_listaConectados.Name = "panel_listaConectados";
            this.panel_listaConectados.Size = new System.Drawing.Size(178, 354);
            this.panel_listaConectados.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 298);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "Para invitar a varios usuarios, \r\nseleccionalos mientras pulsas CTRL\r\n";
            // 
            // button_crearPartida
            // 
            this.button_crearPartida.Location = new System.Drawing.Point(40, 255);
            this.button_crearPartida.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_crearPartida.Name = "button_crearPartida";
            this.button_crearPartida.Size = new System.Drawing.Size(115, 40);
            this.button_crearPartida.TabIndex = 1;
            this.button_crearPartida.Text = "CREAR PARTIDA!";
            this.button_crearPartida.UseVisualStyleBackColor = true;
            this.button_crearPartida.Click += new System.EventHandler(this.button_crearPartida_Click);
            // 
            // dataGridView_listaConectados
            // 
            this.dataGridView_listaConectados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_listaConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_listaConectados.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_listaConectados.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_listaConectados.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView_listaConectados.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_listaConectados.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView_listaConectados.Name = "dataGridView_listaConectados";
            this.dataGridView_listaConectados.RowHeadersVisible = false;
            this.dataGridView_listaConectados.RowHeadersWidth = 62;
            this.dataGridView_listaConectados.RowTemplate.Height = 28;
            this.dataGridView_listaConectados.Size = new System.Drawing.Size(178, 252);
            this.dataGridView_listaConectados.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 354);
            this.Controls.Add(this.panel_listaConectados);
            this.Controls.Add(this.panel_inicioSesion);
            this.Controls.Add(this.panel_Menu);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel_Menu.ResumeLayout(false);
            this.panel_Menu.PerformLayout();
            this.panel_estadoConexion.ResumeLayout(false);
            this.panel_estadoConexion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_estado)).EndInit();
            this.panel_inicioSesion.ResumeLayout(false);
            this.panel_inicioSesion.PerformLayout();
            this.panel_listaConectados.ResumeLayout(false);
            this.panel_listaConectados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_listaConectados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Menu;
        private System.Windows.Forms.Button button_salir;
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
        private System.Windows.Forms.Panel panel_listaConectados;
        private System.Windows.Forms.DataGridView dataGridView_listaConectados;
        private System.Windows.Forms.TextBox textBox_nick;
        private System.Windows.Forms.Label label_nickRegistro;
        private System.Windows.Forms.Button button_registrarse;
        private System.Windows.Forms.Button button_eliminar;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_crearPartida;
        private System.Windows.Forms.PictureBox pictureBox_estado;
        private System.Windows.Forms.Label label_info_apodo;
    }
}

