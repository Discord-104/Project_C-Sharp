namespace VisiteTTMediche
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.listBoxCestino = new System.Windows.Forms.ListBox();
            this.ButtonRipristina = new System.Windows.Forms.Button();
            this.ButtonPulisciCestino = new System.Windows.Forms.Button();
            this.EliminaDefinitivamente = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxCestino
            // 
            this.listBoxCestino.FormattingEnabled = true;
            this.listBoxCestino.ItemHeight = 16;
            this.listBoxCestino.Location = new System.Drawing.Point(12, 12);
            this.listBoxCestino.Name = "listBoxCestino";
            this.listBoxCestino.Size = new System.Drawing.Size(207, 420);
            this.listBoxCestino.TabIndex = 0;
            // 
            // ButtonRipristina
            // 
            this.ButtonRipristina.Location = new System.Drawing.Point(225, 12);
            this.ButtonRipristina.Name = "ButtonRipristina";
            this.ButtonRipristina.Size = new System.Drawing.Size(90, 29);
            this.ButtonRipristina.TabIndex = 1;
            this.ButtonRipristina.Text = "Ripristina";
            this.ButtonRipristina.UseVisualStyleBackColor = true;
            this.ButtonRipristina.Click += new System.EventHandler(this.ButtonRipristina_Click);
            // 
            // ButtonPulisciCestino
            // 
            this.ButtonPulisciCestino.Location = new System.Drawing.Point(225, 47);
            this.ButtonPulisciCestino.Name = "ButtonPulisciCestino";
            this.ButtonPulisciCestino.Size = new System.Drawing.Size(90, 28);
            this.ButtonPulisciCestino.TabIndex = 2;
            this.ButtonPulisciCestino.Text = "Pulisci";
            this.ButtonPulisciCestino.UseVisualStyleBackColor = true;
            this.ButtonPulisciCestino.Click += new System.EventHandler(this.ButtonPulisciCestino_Click);
            // 
            // EliminaDefinitivamente
            // 
            this.EliminaDefinitivamente.Location = new System.Drawing.Point(225, 81);
            this.EliminaDefinitivamente.Name = "EliminaDefinitivamente";
            this.EliminaDefinitivamente.Size = new System.Drawing.Size(90, 28);
            this.EliminaDefinitivamente.TabIndex = 3;
            this.EliminaDefinitivamente.Text = "Elimina";
            this.EliminaDefinitivamente.UseVisualStyleBackColor = true;
            this.EliminaDefinitivamente.Click += new System.EventHandler(this.EliminaDefinitivamente_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.EliminaDefinitivamente);
            this.Controls.Add(this.ButtonPulisciCestino);
            this.Controls.Add(this.ButtonRipristina);
            this.Controls.Add(this.listBoxCestino);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form4";
            this.Text = "Cestino";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form4_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxCestino;
        private System.Windows.Forms.Button ButtonRipristina;
        private System.Windows.Forms.Button ButtonPulisciCestino;
        private System.Windows.Forms.Button EliminaDefinitivamente;
    }
}