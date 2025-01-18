using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisiteTTMediche
{
    public partial class Form2 : Form
    {
        private Dati dati; // Oggetto per gestire i dati dell'applicazione

        public Form2(Dati dati)
        {
            InitializeComponent();
            this.dati = dati;

            // Carica le figure aziendali all'avvio del form
            CaricaFigureAziendali("CSV\\Addetto.csv");
        }

        // Metodo per caricare le figure aziendali da un file CSV
        private void CaricaFigureAziendali(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string row = sr.ReadLine();
                        FiguraAziendale figura = FiguraAziendale.Parse(row); // Parsing della riga del file CSV in un oggetto FiguraAziendale
                        dati.FigureAziendali.Add(figura); // Aggiunge la figura aziendale alla collezione
                        ComboFigureAziendali.Items.Add(figura.Nome); // Aggiunge il nome della figura aziendale alla ComboBox
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante la lettura del file: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Gestisce il click sul pulsante di aggiunta di una persona
        private void ButtonAggiungi_Click(object sender, EventArgs e)
        {
            // Validazione dei campi obbligatori
            if (string.IsNullOrWhiteSpace(TXT_Nome.Text))
            {
                MessageBox.Show("Inserisci un nome valido.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(TXT_Cognome.Text))
            {
                MessageBox.Show("Inserisci un cognome valido.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ComboFigureAziendali.SelectedItem == null)
            {
                MessageBox.Show("Seleziona una figura aziendale.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Recupero dei dati dai controlli
            string nome = TXT_Nome.Text;
            string cognome = TXT_Cognome.Text;
            bool muletto = Muletto.Checked;
            string figuraAziendale = ComboFigureAziendali.SelectedItem as string;
            string note = TXT_Note.Text;

            // Creazione di un nuovo oggetto Persona
            Persona nuovaPersona = new Persona(cognome, nome, figuraAziendale, note);
            nuovaPersona.Id = GetNextPersonaleId(); // Assegnazione di un nuovo ID univoco
            nuovaPersona.muletto = muletto;
            nuovaPersona.Rumore = false;

            // Aggiunta della persona alla collezione
            dati.Persone.Add(nuovaPersona);

            // Salvataggio dei dati su file CSV
            SalvaSuFile("CSV\\Personale.csv");

            // Pulizia dei campi dopo l'aggiunta
            Pulisci();
        }

        // Pulisce i campi del form
        private void Pulisci()
        {
            TXT_Nome.Text = "";
            TXT_Cognome.Text = "";
            TXT_Note.Text = "";
            ComboFigureAziendali.SelectedItem = null;
            Muletto.Checked = false;
        }

        // Salva i dati delle persone su un file CSV
        private void SalvaSuFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var persona in dati.Persone)
                    {
                        writer.WriteLine(persona.TOCSV()); // Scrive la rappresentazione CSV di ogni persona nel file
                    }
                }
                MessageBox.Show("Dati salvati correttamente.", "Informazione", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio dei dati: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Gestisce l'evento di entrata nel campo di testo Nome
        private void TXT_Nome_Enter(object sender, EventArgs e)
        {
            if (TXT_Nome.Text == "Nome")
            {
                TXT_Nome.Text = "";
                TXT_Nome.ForeColor = System.Drawing.Color.Black;
            }
        }

        // Gestisce l'evento di uscita dal campo di testo Nome
        private void TXT_Nome_Leave(object sender, EventArgs e)
        {
            if (TXT_Nome.Text == "")
            {
                TXT_Nome.Text = "Nome";
                TXT_Nome.ForeColor = System.Drawing.Color.Gray;
            }
        }

        // Gestisce l'evento di entrata nel campo di testo Cognome
        private void TXT_Cognome_Enter(object sender, EventArgs e)
        {
            if (TXT_Cognome.Text == "Cognome")
            {
                TXT_Cognome.Text = "";
                TXT_Cognome.ForeColor = System.Drawing.Color.Black;
            }
        }

        // Gestisce l'evento di uscita dal campo di testo Cognome
        private void TXT_Cognome_Leave(object sender, EventArgs e)
        {
            if (TXT_Cognome.Text == "")
            {
                TXT_Cognome.Text = "Cognome";
                TXT_Cognome.ForeColor = System.Drawing.Color.Gray;
            }
        }

        // Gestisce la selezione di una figura aziendale dalla ComboBox
        private void ComboFigureAziendali_SelectedIndexChanged(object sender, EventArgs e)
        {
            string figuraSelezionata = ComboFigureAziendali.SelectedItem as string;
            FiguraAziendale figuraAziendale = null;

            // Cerca la figura aziendale selezionata tra quelle caricate
            foreach (var figura in dati.FigureAziendali)
            {
                if (figura.Nome == figuraSelezionata)
                {
                    figuraAziendale = figura;
                    break;
                }
            }

            // Se trova la figura aziendale, aggiunge le visite predefinite alla persona
            if (figuraAziendale != null)
            {
                foreach (var visita in figuraAziendale.GetVisitePredefinite())
                {
                    AggiungiVisitaPredefinita(visita);
                }
            }
        }

        // Aggiunge una visita predefinita alla persona
        private void AggiungiVisitaPredefinita(VisitaMedica visita)
        {
            string nome = TXT_Nome.Text;
            string cognome = TXT_Cognome.Text;
            bool muletto = Muletto.Checked;
            string figuraAziendale = ComboFigureAziendali.SelectedItem as string;
            string note = TXT_Note.Text;

            // Verifica se il dipendente è già presente nella collezione
            Persona dipendente = dati.Persone.FirstOrDefault(p => p.Nome == nome && p.Cognome == cognome && p.Ruolo == figuraAziendale);

            if (dipendente == null)
            {
                // Se il dipendente non è trovato, crea un nuovo dipendente
                dipendente = new Persona(cognome, nome, figuraAziendale, note);
                dipendente.muletto = muletto;
                dati.Persone.Add(dipendente);
            }

            // Aggiungi la visita alla persona solo se non è già presente
            if (!dipendente.Visite.Any(v => v.ID == visita.ID))
            {
                dipendente.AggiungiVisita(visita);

                // Salva l'aggiornamento su file
                SalvaSuFile("CSV\\Personale.csv");

                // Pulisci i campi di input
                Pulisci();
            }
            else
            {
                MessageBox.Show("Questa visita medica è già stata aggiunta per questo dipendente.", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Gestisce l'evento di entrata nel campo di testo Note
        private void TXT_Note_Enter(object sender, EventArgs e)
        {
            if (TXT_Note.Text == "Note")
            {
                TXT_Note.Text = "";
                TXT_Note.ForeColor = System.Drawing.Color.Black;
            }
        }

        // Gestisce l'evento di uscita dal campo di testo Note
        private void TXT_Note_Leave(object sender, EventArgs e)
        {
            if (TXT_Note.Text == "")
            {
                TXT_Note.Text = "Note";
                TXT_Note.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private int GetNextPersonaleId()
        {
            string storicoFilePath = "CSV\\Personale.csv";
            int nextId = 1; // ID di partenza

            try
            {
                if (File.Exists(storicoFilePath))
                {
                    // Legge tutte le righe attuali dal file storico per determinare l'ID successivo
                    string[] lines = File.ReadAllLines(storicoFilePath);

                    if (lines.Length > 0)
                    {
                        // Trova l'ID massimo attualmente utilizzato
                        foreach (string line in lines)
                        {
                            string[] columns = line.Split(';');
                            if (columns.Length > 0)
                            {
                                int id = int.Parse(columns[0]);
                                if (id >= nextId)
                                {
                                    nextId = id + 1; // Incrementa per ottenere un nuovo ID univoco
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il recupero dell'ID successivo per lo storico: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return nextId;
        }
    
    }

}
