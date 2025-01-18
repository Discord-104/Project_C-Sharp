namespace VisiteTTMediche
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.TXT_Nome = new System.Windows.Forms.TextBox();
            this.TXT_Cognome = new System.Windows.Forms.TextBox();
            this.ComboFigureAziendali = new System.Windows.Forms.ComboBox();
            this.Muletto = new System.Windows.Forms.CheckBox();
            this.ButtonAggiungi = new System.Windows.Forms.Button();
            this.TXT_Note = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TXT_Nome
            // 
            this.TXT_Nome.ForeColor = System.Drawing.Color.Gray;
            this.TXT_Nome.Location = new System.Drawing.Point(12, 12);
            this.TXT_Nome.Name = "TXT_Nome";
            this.TXT_Nome.Size = new System.Drawing.Size(446, 22);
            this.TXT_Nome.TabIndex = 0;
            this.TXT_Nome.Text = "Nome";
            this.TXT_Nome.Enter += new System.EventHandler(this.TXT_Nome_Enter);
            this.TXT_Nome.Leave += new System.EventHandler(this.TXT_Nome_Leave);
            // 
            // TXT_Cognome
            // 
            this.TXT_Cognome.ForeColor = System.Drawing.Color.Gray;
            this.TXT_Cognome.Location = new System.Drawing.Point(12, 40);
            this.TXT_Cognome.Name = "TXT_Cognome";
            this.TXT_Cognome.Size = new System.Drawing.Size(446, 22);
            this.TXT_Cognome.TabIndex = 1;
            this.TXT_Cognome.Text = "Cognome";
            this.TXT_Cognome.Enter += new System.EventHandler(this.TXT_Cognome_Enter);
            this.TXT_Cognome.Leave += new System.EventHandler(this.TXT_Cognome_Leave);
            // 
            // ComboFigureAziendali
            // 
            this.ComboFigureAziendali.ForeColor = System.Drawing.Color.Gray;
            this.ComboFigureAziendali.FormattingEnabled = true;
            this.ComboFigureAziendali.Location = new System.Drawing.Point(12, 96);
            this.ComboFigureAziendali.Name = "ComboFigureAziendali";
            this.ComboFigureAziendali.Size = new System.Drawing.Size(446, 24);
            this.ComboFigureAziendali.TabIndex = 3;
            this.ComboFigureAziendali.Text = "Figura Lavorativa";
            this.ComboFigureAziendali.SelectedIndexChanged += new System.EventHandler(this.ComboFigureAziendali_SelectedIndexChanged);
            // 
            // Muletto
            // 
            this.Muletto.AutoSize = true;
            this.Muletto.Location = new System.Drawing.Point(12, 158);
            this.Muletto.Name = "Muletto";
            this.Muletto.Size = new System.Drawing.Size(72, 20);
            this.Muletto.TabIndex = 4;
            this.Muletto.Text = "Muletto";
            this.Muletto.UseVisualStyleBackColor = true;
            // 
            // ButtonAggiungi
            // 
            this.ButtonAggiungi.Location = new System.Drawing.Point(12, 126);
            this.ButtonAggiungi.Name = "ButtonAggiungi";
            this.ButtonAggiungi.Size = new System.Drawing.Size(446, 26);
            this.ButtonAggiungi.TabIndex = 5;
            this.ButtonAggiungi.Text = "Aggiungi";
            this.ButtonAggiungi.UseVisualStyleBackColor = true;
            this.ButtonAggiungi.Click += new System.EventHandler(this.ButtonAggiungi_Click);
            // 
            // TXT_Note
            // 
            this.TXT_Note.ForeColor = System.Drawing.Color.Gray;
            this.TXT_Note.Location = new System.Drawing.Point(12, 68);
            this.TXT_Note.Name = "TXT_Note";
            this.TXT_Note.Size = new System.Drawing.Size(446, 22);
            this.TXT_Note.TabIndex = 6;
            this.TXT_Note.Text = "Note";
            this.TXT_Note.Enter += new System.EventHandler(this.TXT_Note_Enter);
            this.TXT_Note.Leave += new System.EventHandler(this.TXT_Note_Leave);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 185);
            this.Controls.Add(this.TXT_Note);
            this.Controls.Add(this.ButtonAggiungi);
            this.Controls.Add(this.Muletto);
            this.Controls.Add(this.ComboFigureAziendali);
            this.Controls.Add(this.TXT_Cognome);
            this.Controls.Add(this.TXT_Nome);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "Aggiungi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXT_Nome;
        private System.Windows.Forms.TextBox TXT_Cognome;
        private System.Windows.Forms.ComboBox ComboFigureAziendali;
        private System.Windows.Forms.CheckBox Muletto;
        private System.Windows.Forms.Button ButtonAggiungi;
        private System.Windows.Forms.TextBox TXT_Note;
    }
}