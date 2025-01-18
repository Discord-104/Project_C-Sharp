using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using VisiteTTMediche;

namespace VisiteTTMediche
{
    public partial class Form1 : Form
    {
        private Dati dati; // Dati del programma
        private ObservableCollection<VisitaMedica> visita; // Elenco delle visite mediche
        private VisitaMedica visitaMedica; // Singola visita medica

        public Form1()
        {
            // Inizializzazione delle variabili
            visitaMedica = new VisitaMedica();
            visita = new ObservableCollection<VisitaMedica>();
            InitializeComponent();

            // Inizializzazione dei dati principali
            dati = new Dati();
            dati.Persone = carica("CSV\\Personale.csv"); // Carica le persone da un file CSV
            Tabella.DataSource = dati.Persone; // Associa l'elenco delle persone alla DataGridView
            Tabella.CellDoubleClick += Tabella_CellDoubleClick; // Gestisce il doppio click su una cella della tabella
            Tabella.CellValueChanged += Tabella_CellValueChanged; // Gestisce la modifica dei valori delle celle
            Tabella.CellContentClick += Tabella_CellContentClick; // Gestisce il click sulle celle della DataGridView

            CaricaImmagineTT(); // Carica l'immagine del logo

            // Impostazioni iniziali della DataGridView
            FormatDataGridView(); // Formatta le colonne della DataGridView
            PopolaComboBoxVisite(); // Popola la ComboBox delle visite disponibili
            CaricaVisite(); // Carica le visite mediche
            AggiungiColonnaScadenza(); // Aggiunge una colonna "Stato" alla DataGridView
        }

        // Aggiunge una colonna "Stato" alla DataGridView per visualizzare lo stato delle visite
        private void AggiungiColonnaScadenza()
        {
            DataGridViewTextBoxColumn colScadenza = new DataGridViewTextBoxColumn();
            colScadenza.Name = "Stato";
            colScadenza.HeaderText = "Stato";
            Tabella.Columns.Add(colScadenza);
            Tabella.CellFormatting += Tabella_CellFormatting; // Gestisce il formato delle celle
        }

        // Popola la ComboBox delle visite con i dati da un file CSV
        private void PopolaComboBoxVisite()
        {
            string visiteFilePath = "CSV\\Addetto.csv";
            if (File.Exists(visiteFilePath))
            {
                using (StreamReader sr = new StreamReader(visiteFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        COMBO_Figura.Items.Add(line); // Aggiunge ogni linea come elemento della ComboBox
                    }
                }
            }
        }

        // Carica le persone da un file CSV e restituisce una ObservableCollection
        private ObservableCollection<Persona> carica(string path)
        {
            ObservableCollection<Persona> am = new ObservableCollection<Persona>();
            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                String row = sr.ReadLine();
                am.Add(Persona.parse(row)); // Aggiunge ogni riga convertita in oggetto Persona
            }
            sr.Close();
            sr.Dispose();
            return am;
        }

        // Carica le visite mediche da file CSV
        private void CaricaVisite()
        {
            try
            {
                string visiteFilePath = "CSV\\Prova.csv";
                string dettagliFilePath = "CSV\\VisiteMedicheFM.csv";

                visita.Clear(); // Pulisce la lista delle visite

                using (StreamReader srVisite = new StreamReader(visiteFilePath))
                {
                    string line;
                    while ((line = srVisite.ReadLine()) != null)
                    {
                        string[] columns = line.Split(';');
                        if (columns.Length >= 3 && int.TryParse(columns[0], out int idPersona) && int.TryParse(columns[1], out int idVisitaMedica))
                        {
                            string dataVisita = columns[2];

                            // Ottiene il nome della visita medica e la frequenza associata
                            string nomeVisitaMedica = VisitaMedica.GetFrequenza(idVisitaMedica, dettagliFilePath, out int frequenza);
                            if (nomeVisitaMedica != null)
                            {
                                visita.Add(new VisitaMedica
                                {
                                    ID = idVisitaMedica,
                                    Visita = nomeVisitaMedica,
                                    Data = DateTime.Parse(dataVisita),
                                    Frequenza_Mesi = frequenza,
                                    IDPersona = idPersona
                                });
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento delle visite: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Carica e imposta l'immagine del logo dell'applicazione
        private void CaricaImmagineTT()
        {
            // Ottiene il percorso del direttorio dell'eseguibile
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            // Costruisce il percorso relativo all'immagine
            string relativePath = @"IMG\TTMedic.png";
            string imagePath = Path.Combine(basePath, relativePath);

            // Controlla se il file esiste prima di caricarlo
            if (File.Exists(imagePath))
            {
                // Imposta il percorso dell'immagine nella PictureBox
                BOX_LATT.ImageLocation = imagePath;
            }
            else
            {
                MessageBox.Show("Immagine non trovata: " + imagePath);
            }
        }

        // Formatta le colonne della DataGridView
        private void FormatDataGridView()
        {
            // Modifica l'intestazione delle colonne desiderate
            Tabella.Columns["Nome"].HeaderText = "Nome";
            Tabella.Columns["Cognome"].HeaderText = "Cognome";
            Tabella.Columns["Ruolo"].HeaderText = "Ruolo";
            Tabella.Columns["Muletto"].HeaderText = "Muletto";

            // Nasconde la colonna "Id"
            Tabella.Columns["Id"].Visible = false;

            // Imposta il riempimento automatico delle colonne
            Tabella.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }

        // Gestisce il cambiamento del testo nel campo di ricerca
        private void TXT_Ricerca_TextChanged(object sender, EventArgs e)
        {
            string testoRicerca = TXT_Ricerca.Text.ToLower();
            if (testoRicerca == "ricerca")
            {
                Tabella.DataSource = dati.Persone;
            }
            else
            {
                ObservableCollection<Persona> personeFiltrate = FiltraPersone(testoRicerca); // Filtra le persone in base al testo di ricerca
                Tabella.DataSource = personeFiltrate; // Aggiorna i dati visualizzati nella DataGridView
            }

        }

        // Filtra le persone in base al testo di ricerca
        private ObservableCollection<Persona> FiltraPersone(string testoRicerca)
        {
            ObservableCollection<Persona> personeFiltrate = new ObservableCollection<Persona>();
            foreach (var persona in dati.Persone.Where(v => v.Cognome.ToLower().Contains(testoRicerca) || v.Nome.ToLower().Contains(testoRicerca) || v.Ruolo.ToLower().Contains(testoRicerca)))
            {
               
                    personeFiltrate.Add(persona); // Aggiunge la persona filtrata alla lista
              
            }
            return personeFiltrate;
        }

        // Gestisce il click sul pulsante per aprire Form2
        private void ButtonForm2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(dati);
            form2.Show(); // Apre il Form2
        }

        // Gestisce il click sul pulsante per eliminare una persona
        private void ButtonElimina_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Tabella.SelectedRows)
            {
                if (row.DataBoundItem is Persona persona)
                {
                    dati.Persone.Remove(persona); // Rimuove la persona dalla lista
                    dati.AggiungiAlCestino(persona); // Aggiunge la persona al cestino
                }
            }

            SalvaSuFile("CSV\\Personale.csv"); // Salva le modifiche su file

            Tabella.DataSource = null;
            Tabella.DataSource = dati.Persone; // Aggiorna i dati visualizzati nella DataGridView
            FormatDataGridView(); // Riformatta le colonne della DataGridView
        }

        // Salva i dati correnti su un file CSV
        private void SalvaSuFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var persona in dati.Persone)
                    {
                        writer.WriteLine(persona.TOCSV()); // Scrive ogni persona nel file CSV
                    }
                }
                MessageBox.Show("Dati salvati correttamente.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio dei dati: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Gestisce il formato delle celle della DataGridView
        private void Tabella_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verifica se la cella è della colonna "Stato"
            if (Tabella.Columns[e.ColumnIndex].Name == "Stato")
            {
                // Ottiene la persona associata alla riga
                if (Tabella.Rows[e.RowIndex].DataBoundItem is Persona persona)
                {
                    // Filtra le visite relative alla persona corrente
                    var visitePersona = visita.Where(v => v.IDPersona == persona.Id);

                    string status = "N/A";
                    bool isInScadenza = false;

                    foreach (var visitaMedica in visitePersona)
                    {
                        string currentStatus = visitaMedica.GetStatus(visitaMedica.Data, visita, persona.Id, persona.Rumore); // Ottiene lo stato della visita

                        if (currentStatus == "Scaduta")
                        {
                            status = "Scaduta";
                            e.CellStyle.BackColor = Color.Red; // Imposta il colore rosso per le visite scadute
                            break;
                        }
                        else if (currentStatus == "In scadenza")
                        {
                            status = "In scadenza";
                            e.CellStyle.BackColor = Color.Yellow; // Imposta il colore giallo per le visite in scadenza
                            isInScadenza = true;
                        }
                        else if (currentStatus == "Valida" && !isInScadenza)
                        {
                            status = "Valida";
                            e.CellStyle.BackColor = Color.LightSkyBlue; // Imposta il colore azzurro chiaro per le visite valide
                        }
                    }

                    e.Value = status; // Imposta il valore della cella
                }
            }
        }

        // Apre il Form3 per visualizzare i dettagli delle visite di una persona
        private void ApriDettagliVisiteForm(Persona personaSelezionata)
        {
            Form3 form3 = new Form3(personaSelezionata, visita);
            form3.Show(); // Apre il Form3
        }

        // Gestisce il doppio click su una cella della DataGridView
        private void Tabella_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Persona personaSelezionata = (Persona)Tabella.Rows[e.RowIndex].DataBoundItem;

                // Evita di aprire più volte la stessa finestra
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm is Form3)
                    {
                        openForm.Focus();
                        return;
                    }
                }

                ApriDettagliVisiteForm(personaSelezionata); // Apre il Form3 con i dettagli delle visite
            }
        }

        // Gestisce il click sul pulsante per aprire Form4 (Cestino)
        private void ButtonCestino_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(dati);
            form4.Show(); // Apre il Form4
        }

        // Gestisce l'evento Enter nel campo di ricerca
        private void TXT_Ricerca_Enter(object sender, EventArgs e)
        {
            if (TXT_Ricerca.Text == "Ricerca")
            {
                TXT_Ricerca.Text = "";
                TXT_Ricerca.ForeColor = Color.Black;
            }
        }

        // Gestisce l'evento Leave nel campo di ricerca
        private void TXT_Ricerca_Leave(object sender, EventArgs e)
        {
            if (TXT_Ricerca.Text == "")
            {
                TXT_Ricerca.Text = "Ricerca";
                TXT_Ricerca.ForeColor = Color.Gray;
            }
        }

        // Gestisce il cambiamento della selezione nella DataGridView
        private void Tabella_SelectionChanged(object sender, EventArgs e)
        {
            if (Tabella.SelectedRows.Count > 0)
            {
                var personaSelezionata = (Persona)Tabella.SelectedRows[0].DataBoundItem;
                COMBO_Figura.SelectedItem = personaSelezionata.Ruolo; // Imposta il valore della ComboBox in base al ruolo della persona selezionata
            }
        }

        // Gestisce il cambiamento della selezione nella ComboBox
        private void COMBO_Figura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Tabella.SelectedRows.Count > 0)
            {
                var personaSelezionata = (Persona)Tabella.SelectedRows[0].DataBoundItem;
                personaSelezionata.Ruolo = COMBO_Figura.SelectedItem.ToString(); // Aggiorna il ruolo della persona selezionata
                SalvaSuFile("CSV\\Personale.csv"); // Salva i dati su file
                Tabella.Refresh(); // Aggiorna la DataGridView
            }
        }

        private void Tabella_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se è stata cliccata una cella di tipo DataGridViewCheckBoxColumn e se l'indice della riga è valido
            if (Tabella.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                Tabella.CommitEdit(DataGridViewDataErrorContexts.Commit); // Conferma l'editazione della cella
            }
        }

        private void Tabella_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se l'indice della riga è valido
            if (e.RowIndex >= 0)
            {
                // Ottiene l'oggetto persona associato alla riga modificata
                var persona = (Persona)Tabella.Rows[e.RowIndex].DataBoundItem;

                // Verifica quale colonna è stata modificata tramite l'indice della colonna
                if (Tabella.Columns[e.ColumnIndex].Name == "Muletto")
                {
                    // Aggiorna la proprietà 'muletto' dell'oggetto persona con il valore della cella modificata
                    persona.muletto = (bool)Tabella.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                }
                else if (Tabella.Columns[e.ColumnIndex].Name == "Rumore")
                {
                    // Aggiorna la proprietà 'Rumore' dell'oggetto persona con il valore della cella modificata
                    persona.Rumore = (bool)Tabella.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                }

                SalvaSuFile("CSV\\Personale.csv"); // Salva le modifiche su file CSV
            }
        }
    }
}
