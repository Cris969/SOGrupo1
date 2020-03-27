namespace Cliente
{
    partial class F_login
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
            this.label_titulo = new System.Windows.Forms.Label();
            this.button_inicioSesion = new System.Windows.Forms.Button();
            this.label_usuario = new System.Windows.Forms.Label();
            this.textBox_usuario = new System.Windows.Forms.TextBox();
            this.label_pasword = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.button_registrarse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_registrarse = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_titulo
            // 
            this.label_titulo.AutoSize = true;
            this.label_titulo.Font = new System.Drawing.Font("Microsoft YaHei", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_titulo.Location = new System.Drawing.Point(206, 42);
            this.label_titulo.Name = "label_titulo";
            this.label_titulo.Size = new System.Drawing.Size(335, 39);
            this.label_titulo.TabIndex = 0;
            this.label_titulo.Text = "BIENVENIDO A LA V1";
            // 
            // button_inicioSesion
            // 
            this.button_inicioSesion.Location = new System.Drawing.Point(255, 311);
            this.button_inicioSesion.Name = "button_inicioSesion";
            this.button_inicioSesion.Size = new System.Drawing.Size(97, 25);
            this.button_inicioSesion.TabIndex = 1;
            this.button_inicioSesion.Text = "Iniciar Sesión";
            this.button_inicioSesion.UseVisualStyleBackColor = true;
            this.button_inicioSesion.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_usuario
            // 
            this.label_usuario.AutoSize = true;
            this.label_usuario.Location = new System.Drawing.Point(265, 184);
            this.label_usuario.Name = "label_usuario";
            this.label_usuario.Size = new System.Drawing.Size(99, 13);
            this.label_usuario.TabIndex = 2;
            this.label_usuario.Text = "Nombre de usuario:";
            // 
            // textBox_usuario
            // 
            this.textBox_usuario.Location = new System.Drawing.Point(392, 177);
            this.textBox_usuario.Name = "textBox_usuario";
            this.textBox_usuario.Size = new System.Drawing.Size(100, 20);
            this.textBox_usuario.TabIndex = 3;
            this.textBox_usuario.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label_pasword
            // 
            this.label_pasword.AutoSize = true;
            this.label_pasword.Location = new System.Drawing.Point(265, 229);
            this.label_pasword.Name = "label_pasword";
            this.label_pasword.Size = new System.Drawing.Size(56, 13);
            this.label_pasword.TabIndex = 4;
            this.label_pasword.Text = "Password:";
            this.label_pasword.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(392, 222);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(100, 20);
            this.textBox_password.TabIndex = 5;
            // 
            // button_registrarse
            // 
            this.button_registrarse.Location = new System.Drawing.Point(392, 311);
            this.button_registrarse.Name = "button_registrarse";
            this.button_registrarse.Size = new System.Drawing.Size(96, 25);
            this.button_registrarse.TabIndex = 6;
            this.button_registrarse.Text = "Registrarse";
            this.button_registrarse.UseVisualStyleBackColor = true;
            this.button_registrarse.Click += new System.EventHandler(this.button_registrarse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Apodo:";
            // 
            // textBox_registrarse
            // 
            this.textBox_registrarse.Location = new System.Drawing.Point(392, 267);
            this.textBox_registrarse.Name = "textBox_registrarse";
            this.textBox_registrarse.Size = new System.Drawing.Size(100, 20);
            this.textBox_registrarse.TabIndex = 8;
            // 
            // F_login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.textBox_registrarse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_registrarse);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.label_pasword);
            this.Controls.Add(this.textBox_usuario);
            this.Controls.Add(this.label_usuario);
            this.Controls.Add(this.button_inicioSesion);
            this.Controls.Add(this.label_titulo);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Location = new System.Drawing.Point(20, 20);
            this.Name = "F_login";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_titulo;
        private System.Windows.Forms.Button button_inicioSesion;
        private System.Windows.Forms.Label label_usuario;
        private System.Windows.Forms.TextBox textBox_usuario;
        private System.Windows.Forms.Label label_pasword;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button button_registrarse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_registrarse;
    }
}

