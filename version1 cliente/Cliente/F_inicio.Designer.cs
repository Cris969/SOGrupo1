namespace Cliente
{
    partial class F_inicio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_consulta1 = new System.Windows.Forms.Button();
            this.button_consulta3 = new System.Windows.Forms.Button();
            this.button_consulta2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_consulta1
            // 
            this.button_consulta1.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_consulta1.Location = new System.Drawing.Point(76, 137);
            this.button_consulta1.Name = "button_consulta1";
            this.button_consulta1.Size = new System.Drawing.Size(220, 89);
            this.button_consulta1.TabIndex = 3;
            this.button_consulta1.Text = "Mayor puntuación de un jugador.";
            this.button_consulta1.UseVisualStyleBackColor = true;
            this.button_consulta1.Click += new System.EventHandler(this.button_consulta1_Click);
            // 
            // button_consulta3
            // 
            this.button_consulta3.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_consulta3.Location = new System.Drawing.Point(520, 137);
            this.button_consulta3.Name = "button_consulta3";
            this.button_consulta3.Size = new System.Drawing.Size(220, 89);
            this.button_consulta3.TabIndex = 4;
            this.button_consulta3.Text = "Número de partidas ganadas de un jugador.";
            this.button_consulta3.UseVisualStyleBackColor = true;
            this.button_consulta3.Click += new System.EventHandler(this.button_consulta3_Click);
            // 
            // button_consulta2
            // 
            this.button_consulta2.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_consulta2.Location = new System.Drawing.Point(298, 263);
            this.button_consulta2.Name = "button_consulta2";
            this.button_consulta2.Size = new System.Drawing.Size(220, 89);
            this.button_consulta2.TabIndex = 5;
            this.button_consulta2.Text = "Datos de una partida.";
            this.button_consulta2.UseVisualStyleBackColor = true;
            this.button_consulta2.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(254, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Seleccione la consulta a realizar:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // F_inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_consulta2);
            this.Controls.Add(this.button_consulta3);
            this.Controls.Add(this.button_consulta1);
            this.Name = "F_inicio";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.F_inicio_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_consulta1;
        private System.Windows.Forms.Button button_consulta3;
        private System.Windows.Forms.Button button_consulta2;
        private System.Windows.Forms.Label label1;
    }
}