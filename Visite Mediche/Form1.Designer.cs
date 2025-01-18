namespace VisiteTTMediche
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Tabella = new System.Windows.Forms.DataGridView();
            this.TXT_Ricerca = new System.Windows.Forms.TextBox();
            this.ButtonForm2 = new System.Windows.Forms.Button();
            this.ButtonElimina = new System.Windows.Forms.Button();
            this.ButtonCestino = new System.Windows.Forms.Button();
            this.BOX_LATT = new System.Windows.Forms.PictureBox();
            this.COMBO_Figura = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.Tabella)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BOX_LATT)).BeginInit();
            this.SuspendLayout();
            // 
            // Tabella
            // 
            this.Tabella.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tabella.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tabella.Location = new System.Drawing.Point(12, 167);
            this.Tabella.Name = "Tabella";
            this.Tabella.RowHeadersWidth = 51;
            this.Tabella.RowTemplate.Height = 24;
            this.Tabella.Size = new System.Drawing.Size(1656, 680);
            this.Tabella.TabIndex = 0;
            this.Tabella.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tabella_CellContentClick);
            this.Tabella.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tabella_CellDoubleClick);
            this.Tabella.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tabella_CellFormatting);
            this.Tabella.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tabella_CellValueChanged);
            this.Tabella.SelectionChanged += new System.EventHandler(this.Tabella_SelectionChanged);
            // 
            // TXT_Ricerca
            // 
            this.TXT_Ricerca.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TXT_Ricerca.ForeColor = System.Drawing.Color.Gray;
            this.TXT_Ricerca.Location = new System.Drawing.Point(12, 853);
            this.TXT_Ricerca.Name = "TXT_Ricerca";
            this.TXT_Ricerca.Size = new System.Drawing.Size(248, 22);
            this.TXT_Ricerca.TabIndex = 1;
            this.TXT_Ricerca.Text = "Ricerca";
            this.TXT_Ricerca.TextChanged += new System.EventHandler(this.TXT_Ricerca_TextChanged);
            this.TXT_Ricerca.Enter += new System.EventHandler(this.TXT_Ricerca_Enter);
            this.TXT_Ricerca.Leave += new System.EventHandler(this.TXT_Ricerca_Leave);
            // 
            // ButtonForm2
            // 
            this.ButtonForm2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonForm2.Location = new System.Drawing.Point(1483, 853);
            this.ButtonForm2.Name = "ButtonForm2";
            this.ButtonForm2.Size = new System.Drawing.Size(185, 29);
            this.ButtonForm2.TabIndex = 2;
            this.ButtonForm2.Text = "Nuovo";
            this.ButtonForm2.UseVisualStyleBackColor = true;
            this.ButtonForm2.Click += new System.EventHandler(this.ButtonForm2_Click);
            // 
            // ButtonElimina
            // 
            this.ButtonElimina.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonElimina.Location = new System.Drawing.Point(12, 881);
            this.ButtonElimina.Name = "ButtonElimina";
            this.ButtonElimina.Size = new System.Drawing.Size(186, 29);
            this.ButtonElimina.TabIndex = 3;
            this.ButtonElimina.Text = "Elimina";
            this.ButtonElimina.UseVisualStyleBackColor = true;
            this.ButtonElimina.Click += new System.EventHandler(this.ButtonElimina_Click);
            // 
            // ButtonCestino
            // 
            this.ButtonCestino.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCestino.Location = new System.Drawing.Point(8, 916);
            this.ButtonCestino.Name = "ButtonCestino";
            this.ButtonCestino.Size = new System.Drawing.Size(189, 29);
            this.ButtonCestino.TabIndex = 4;
            this.ButtonCestino.Text = "Cestino";
            this.ButtonCestino.UseVisualStyleBackColor = true;
            this.ButtonCestino.Click += new System.EventHandler(this.ButtonCestino_Click);
            // 
            // BOX_LATT
            // 
            this.BOX_LATT.Location = new System.Drawing.Point(8, 12);
            this.BOX_LATT.Name = "BOX_LATT";
            this.BOX_LATT.Size = new System.Drawing.Size(186, 149);
            this.BOX_LATT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BOX_LATT.TabIndex = 5;
            this.BOX_LATT.TabStop = false;
            // 
            // COMBO_Figura
            // 
            this.COMBO_Figura.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.COMBO_Figura.ForeColor = System.Drawing.Color.Gray;
            this.COMBO_Figura.FormattingEnabled = true;
            this.COMBO_Figura.Location = new System.Drawing.Point(1483, 888);
            this.COMBO_Figura.Name = "COMBO_Figura";
            this.COMBO_Figura.Size = new System.Drawing.Size(185, 24);
            this.COMBO_Figura.TabIndex = 6;
            this.COMBO_Figura.Text = "Figura Lavorativa";
            this.COMBO_Figura.SelectedIndexChanged += new System.EventHandler(this.COMBO_Figura_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1680, 962);
            this.Controls.Add(this.COMBO_Figura);
            this.Controls.Add(this.BOX_LATT);
            this.Controls.Add(this.ButtonCestino);
            this.Controls.Add(this.ButtonElimina);
            this.Controls.Add(this.ButtonForm2);
            this.Controls.Add(this.TXT_Ricerca);
            this.Controls.Add(this.Tabella);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "TTMedic";
            ((System.ComponentModel.ISupportInitialize)(this.Tabella)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BOX_LATT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Tabella;
        private System.Windows.Forms.TextBox TXT_Ricerca;
        private System.Windows.Forms.Button ButtonForm2;
        private System.Windows.Forms.Button ButtonElimina;
        private System.Windows.Forms.Button ButtonCestino;
        private System.Windows.Forms.PictureBox BOX_LATT;
        private System.Windows.Forms.ComboBox COMBO_Figura;
    }
}

