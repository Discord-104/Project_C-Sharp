namespace VisiteTTMediche
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.Tabella_Visite = new System.Windows.Forms.DataGridView();
            this.Tabella_Storico = new System.Windows.Forms.DataGridView();
            this.COMBO_Visite = new System.Windows.Forms.ComboBox();
            this.Elimina = new System.Windows.Forms.Button();
            this.Calendario = new System.Windows.Forms.DateTimePicker();
            this.Aggiungi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Tabella_Visite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tabella_Storico)).BeginInit();
            this.SuspendLayout();
            // 
            // Tabella_Visite
            // 
            this.Tabella_Visite.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tabella_Visite.Location = new System.Drawing.Point(12, 12);
            this.Tabella_Visite.Name = "Tabella_Visite";
            this.Tabella_Visite.RowHeadersWidth = 51;
            this.Tabella_Visite.RowTemplate.Height = 24;
            this.Tabella_Visite.Size = new System.Drawing.Size(795, 363);
            this.Tabella_Visite.TabIndex = 0;
            this.Tabella_Visite.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tabella_Visite_CellFormatting);
            // 
            // Tabella_Storico
            // 
            this.Tabella_Storico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tabella_Storico.Location = new System.Drawing.Point(12, 381);
            this.Tabella_Storico.Name = "Tabella_Storico";
            this.Tabella_Storico.RowHeadersWidth = 51;
            this.Tabella_Storico.RowTemplate.Height = 24;
            this.Tabella_Storico.Size = new System.Drawing.Size(795, 363);
            this.Tabella_Storico.TabIndex = 2;
            // 
            // COMBO_Visite
            // 
            this.COMBO_Visite.ForeColor = System.Drawing.Color.Gray;
            this.COMBO_Visite.FormattingEnabled = true;
            this.COMBO_Visite.Location = new System.Drawing.Point(813, 53);
            this.COMBO_Visite.Name = "COMBO_Visite";
            this.COMBO_Visite.Size = new System.Drawing.Size(289, 24);
            this.COMBO_Visite.TabIndex = 3;
            this.COMBO_Visite.Text = "Visita Medica";
            // 
            // Elimina
            // 
            this.Elimina.Location = new System.Drawing.Point(820, 364);
            this.Elimina.Name = "Elimina";
            this.Elimina.Size = new System.Drawing.Size(282, 35);
            this.Elimina.TabIndex = 5;
            this.Elimina.Text = "Elimina";
            this.Elimina.UseVisualStyleBackColor = true;
            this.Elimina.Click += new System.EventHandler(this.Elimina_Click);
            // 
            // Calendario
            // 
            this.Calendario.Location = new System.Drawing.Point(813, 83);
            this.Calendario.Name = "Calendario";
            this.Calendario.Size = new System.Drawing.Size(289, 22);
            this.Calendario.TabIndex = 6;
            // 
            // Aggiungi
            // 
            this.Aggiungi.Location = new System.Drawing.Point(813, 12);
            this.Aggiungi.Name = "Aggiungi";
            this.Aggiungi.Size = new System.Drawing.Size(289, 35);
            this.Aggiungi.TabIndex = 7;
            this.Aggiungi.Text = "Aggiungi";
            this.Aggiungi.UseVisualStyleBackColor = true;
            this.Aggiungi.Click += new System.EventHandler(this.Aggiungi_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 745);
            this.Controls.Add(this.Aggiungi);
            this.Controls.Add(this.Calendario);
            this.Controls.Add(this.Elimina);
            this.Controls.Add(this.COMBO_Visite);
            this.Controls.Add(this.Tabella_Storico);
            this.Controls.Add(this.Tabella_Visite);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form3";
            ((System.ComponentModel.ISupportInitialize)(this.Tabella_Visite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tabella_Storico)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Tabella_Visite;
        private System.Windows.Forms.DataGridView Tabella_Storico;
        private System.Windows.Forms.ComboBox COMBO_Visite;
        private System.Windows.Forms.Button Elimina;
        private System.Windows.Forms.DateTimePicker Calendario;
        private System.Windows.Forms.Button Aggiungi;
    }
}