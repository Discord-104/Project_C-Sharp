using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisiteTTMediche
{
    public partial class Form3 : Form
    {
        // Variabili di classe
        private Persona personaSelezionata;  // Persona attualmente selezionata
        private List<Persona> persone;       // Elenco delle persone caricate da "Personale.csv"
        private ObservableCollection<VisitaProgrammata> visitaProgrammataList;  // Lista di visite programmate per la persona selezionata
        private ObservableCollection<VisitaProgrammata> salvataggio;           // Lista temporanea per salvare le visite
        private ObservableCollection<Storico> storicos;                       // Storico delle visite per la persona
        private List<VisitaProgrammata> visiteDaEliminare;                    // Visite da eliminare
        private ObservableCollection<VisitaMedica> visiteMedicaList;          // Elenco completo delle visite mediche
        private ObservableCollection <Storico> storicoListSalvataggio;

        // Costruttore della Form
        public Form3(Persona persona, ObservableCollection<VisitaMedica> visiteMedicaList)
        {
            // Inizializzazione delle variabili
            visitaProgrammataList = new ObservableCollection<VisitaProgrammata>();
            storicos = new ObservableCollection<Storico>();
            InitializeComponent();

            // Caricamento dei dati iniziali
            persone = carica("CSV\\Personale.csv");   // Carica le persone da "Personale.csv"
            personaSelezionata = persona;        // Imposta la persona selezionata
            PopolaComboBoxVisite();             // Popola la ComboBox con le visite mediche
            CaricaVisite();                     // Carica le visite programmate per la persona
            CaricaStorico();                    // Carica lo storico delle visite
            salvataggio = CaricaVisiteDaFile("CSV\\Prova.csv");  // Carica le visite da "Prova.csv"
            storicoListSalvataggio = CaricaVisiteDaFileStorico("CSV\\Storico.csv");

            // Impostazioni iniziali della DataGridView
            Tabella_Visite.DataSource = visitaProgrammataList;
            Tabella_Storico.DataSource = storicos;
            FormatDataGriew();  // Formatta le DataGridView

            visiteDaEliminare = new List<VisitaProgrammata>();  // Inizializza la lista delle visite da eliminare

            // Gestione dell'evento di formattazione delle celle nella DataGridView delle visite programmate
            Tabella_Visite.CellFormatting += Tabella_Visite_CellFormatting;

            // Assegna l'elenco completo delle visite mediche
            this.visiteMedicaList = visiteMedicaList;

            // Imposta il titolo del form con il nome e il cognome della persona
            this.Text = "Dipendente: " + personaSelezionata.Cognome + " " + personaSelezionata.Nome;
        }

        // Metodo per formattare le DataGridView
        private void FormatDataGriew()
        {
            // Nasconde alcune colonne e imposta il ridimensionamento automatico delle colonne
            Tabella_Visite.Columns["IDPersona"].Visible = false;
            Tabella_Visite.Columns["IdVisitaMedica"].Visible = false;
            Tabella_Storico.Columns["ID"].Visible = false;
            Tabella_Storico.Columns["PersonaID"].Visible = false;
            Tabella_Storico.Columns["VisitaID"].Visible = false;
            Tabella_Visite.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
            Tabella_Storico.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }

        // Popola la ComboBox con le visite mediche disponibili
        private void PopolaComboBoxVisite()
        {
            string visiteFilePath = "CSV\\VisiteMedicheFM.csv";
            if (File.Exists(visiteFilePath))
            {
                using (StreamReader sr = new StreamReader(visiteFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] columns = line.Split(';');
                        string nomeVisita = columns[1];
                        COMBO_Visite.Items.Add(nomeVisita);
                    }
                }
            }
        }

        // Carica l'elenco delle persone da un file CSV
        private List<Persona> carica(string path)
        {
            List<Persona> am = new List<Persona>();

            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                String row = sr.ReadLine();
                am.Add(Persona.parse(row));
            }
            sr.Close();
            sr.Dispose();

            return am;
        }

        // Carica le visite programmate da un file CSV
        private ObservableCollection<VisitaProgrammata> CaricaVisiteDaFile(string path)
        {
            ObservableCollection<VisitaProgrammata> am = new ObservableCollection<VisitaProgrammata>();

            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                String row = sr.ReadLine();
                am.Add(VisitaProgrammata.Parse(row));
            }
            sr.Close();
            sr.Dispose();

            return am;
        }

        private ObservableCollection<Storico> CaricaVisiteDaFileStorico(string path)
        {
            ObservableCollection<Storico> am = new ObservableCollection<Storico>();

            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                String row = sr.ReadLine();
                am.Add(Storico.Parse(row));
            }
            sr.Close();
            sr.Dispose();

            return am;
        }

        // Carica le visite programmate per la persona selezionata
        private void CaricaVisite()
        {
            try
            {
                Persona personaCorrispondente = persone.FirstOrDefault(p => p.Id == personaSelezionata.Id);
                if (personaCorrispondente != null)
                {
                    string visiteFilePath = "CSV\\Prova.csv";
                    string dettagliFilePath = "CSV\\VisiteMedicheFM.csv";

                    visitaProgrammataList.Clear(); // Pulisce la collezione esistente

                    using (StreamReader sr = new StreamReader(visiteFilePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] columns = line.Split(';');
                            if (columns.Length >= 3 && int.TryParse(columns[0], out int idPersona) && idPersona == personaSelezionata.Id)
                            {
                                int idVisitaMedica = int.Parse(columns[1]);
                                string dataVisita = columns[2];

                                // Stampa per debug
                                Console.WriteLine($"Caricamento visita: IDPersona={idPersona}, IDVisitaMedica={idVisitaMedica}, DataVisita={dataVisita}");

                                string nomeVisitaMedica = VisitaMedica.GetNomeVisita(idVisitaMedica, dettagliFilePath);
                                if (nomeVisitaMedica != null)
                                {
                                    visitaProgrammataList.Add(new VisitaProgrammata
                                    {
                                        IDPersona = idPersona,
                                        IDVisitaMedica = idVisitaMedica,
                                        NomeVisitaMedica = nomeVisitaMedica,
                                        DataVisita = dataVisita
                                    });
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Persona non trovata.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // Carica lo storico delle visite per la persona selezionata
        private void CaricaStorico()
        {
            try
            {
                Persona personaCorrispondente = persone.FirstOrDefault(p => p.Id == personaSelezionata.Id);
                if (personaCorrispondente != null)
                {
                    string storicoFilePath = "CSV\\Storico.csv";
                    string dettagliFilePath = "CSV\\VisiteMedicheFM.csv";

                    storicos.Clear();

                    using (StreamReader sr = new StreamReader(storicoFilePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] columns = line.Split(';');
                            if (columns.Length >= 4 && int.TryParse(columns[1], out int personaId) && personaId == personaSelezionata.Id)
                            {
                                int id = int.Parse(columns[0]);
                                int visitaId = int.Parse(columns[2]);
                                string data = columns[3];

                                // Ottiene il nome della visita medica usando VisitaMedica.GetNomeVisita
                                string nomeVisitaMedicaDettagliato = VisitaMedica.GetNomeVisita(visitaId, dettagliFilePath);
                                if (nomeVisitaMedicaDettagliato != null)
                                {
                                    storicos.Add(new Storico
                                    {
                                        ID = id,
                                        PersonaID = personaId,
                                        VisitaID = visitaId,
                                        NomeVisitaMedica = nomeVisitaMedicaDettagliato,
                                        Data = data
                                    });
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Persona non trovata.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento dello storico: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Gestisce l'evento di aggiunta di una nuova visita programmata
        private void Aggiungi_Click(object sender, EventArgs e)
        {
            // Controlla se è stata selezionata una visita dalla ComboBox
            if (COMBO_Visite.SelectedItem == null)
            {
                MessageBox.Show("Seleziona una visita dal menu a tendina.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nomeVisitaSelezionata = COMBO_Visite.SelectedItem.ToString();
            int idVisitaMedica = TrovaIdVisitaMedica(nomeVisitaSelezionata);
            int frequenza = TrovaFrequenzaVisitaMedica(idVisitaMedica);
            if (idVisitaMedica == -1)
            {
                MessageBox.Show("Visita non trovata.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Recupera la data selezionata dal DateTimePicker
            string dataVisita = Calendario.Value.ToString("dd/MM/yyyy");

            // Crea una nuova visita medica
            VisitaProgrammata nuovaVisita = new VisitaProgrammata
            {
                IDPersona = personaSelezionata.Id,
                IDVisitaMedica = idVisitaMedica,
                NomeVisitaMedica = nomeVisitaSelezionata,
                DataVisita = dataVisita
            };

            VisitaMedica NuovaVisitaMedica = new VisitaMedica
            {
                IDPersona = personaSelezionata.Id,
                ID = idVisitaMedica,
                Visita = nomeVisitaSelezionata,
                Data = DateTime.Parse(dataVisita),
                Frequenza_Mesi = frequenza
            };


            // Mostra una conferma prima di aggiungere la visita
            DialogResult conferma = MessageBox.Show("Sei sicuro di voler aggiungere questa visita?", "Conferma aggiunta visita", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (conferma == DialogResult.Yes)
            {
                // Aggiunge la nuova visita alla lista e aggiorna la DataGridView
                visitaProgrammataList.Add(nuovaVisita);
                visiteMedicaList.Add(NuovaVisitaMedica);
                Tabella_Visite.DataSource = null;
                Tabella_Visite.DataSource = visitaProgrammataList;

                // Salva la nuova visita nel file
                AggiungiVisitaAFile(nuovaVisita);

                // Aggiunge la nuova visita allo storico
                AggiungiVisitaAStorico(nuovaVisita);

                MessageBox.Show("Visita aggiunta con successo.", "Operazione completata", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Aggiorna la formattazione della DataGridView
                FormatDataGriew();
            }
        }

        private int TrovaFrequenzaVisitaMedica(int idVisitaMedica)
        {
            string visiteFilePath = "CSV\\VisiteMedicheFM.csv";
            if (File.Exists(visiteFilePath))
            {
                using (StreamReader sr = new StreamReader(visiteFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] columns = line.Split(';');
                        if (columns[0] == idVisitaMedica.ToString())
                        {
                            return int.Parse(columns[2]);
                        }
                    }
                }
            }
            return -1;
        }

        // Aggiunge una nuova visita medica allo storico
        private void AggiungiVisitaAStorico(VisitaProgrammata nuovaVisita)
        {
            // Crea una nuova voce nello storico
            Storico nuovaVoceStorico = new Storico
            {
                ID = GetNextStoricoId(),        // Ottiene il prossimo ID per lo storico
                PersonaID = nuovaVisita.IDPersona,
                VisitaID = nuovaVisita.IDVisitaMedica,
                NomeVisitaMedica = nuovaVisita.NomeVisitaMedica,
                Data = nuovaVisita.DataVisita
            };

            // Aggiunge la nuova voce allo storico
            storicos.Add(nuovaVoceStorico);

            // Aggiorna la DataGridView dello storico
            Tabella_Storico.DataSource = null;
            Tabella_Storico.DataSource = storicos;

            string filePath = "CSV\\Storico.csv";

            try
            {
                // Apre il file in modalità append
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    // Scrive la nuova visita medica nel file
                    sw.WriteLine(nuovaVoceStorico.TOCSV());
                }

                MessageBox.Show("Visita medica salvata nel file storico con successo.", "Salvataggio Completato", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio della visita medica nel file storico: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Aggiunge una nuova visita medica al file CSV delle visite programmate
        private void AggiungiVisitaAFile(VisitaProgrammata nuovaVisita)
        {
            string filePath = "CSV\\Prova.csv";

            try
            {
                // Apre il file in modalità append
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    // Scrive la nuova visita medica nel file
                    sw.WriteLine(nuovaVisita.ToCsvString());
                }

                MessageBox.Show("Visita medica salvata nel file con successo.", "Salvataggio Completato", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio della visita medica nel file: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Trova l'ID della visita medica corrispondente al nome specificato
        private int TrovaIdVisitaMedica(string nomeVisitaSelezionata)
        {
            string visiteFilePath = "CSV\\VisiteMedicheFM.csv";
            if (File.Exists(visiteFilePath))
            {
                using (StreamReader sr = new StreamReader(visiteFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] columns = line.Split(';');
                        if (columns[1] == nomeVisitaSelezionata)
                        {
                            return int.Parse(columns[0]);
                        }
                    }
                }
            }
            return -1;
        }

        // Gestisce l'evento di eliminazione di una visita programmata
        private void Elimina_Click(object sender, EventArgs e)
        {
            if (Tabella_Visite.SelectedRows.Count > 0)
            {
                int rowIndex = Tabella_Visite.SelectedRows[0].Index;
                VisitaProgrammata visitaDaEliminare = visitaProgrammataList[rowIndex];

                // Mostra una conferma prima di eliminare la visita
                DialogResult conferma = MessageBox.Show("Sei sicuro di voler eliminare questa visita?", "Conferma eliminazione visita", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (conferma == DialogResult.Yes)
                {
                    // Rimuove la visita dalla lista e aggiorna la DataGridView
                    visitaProgrammataList.RemoveAt(rowIndex);
                    Tabella_Visite.DataSource = null;
                    Tabella_Visite.DataSource = visitaProgrammataList;

                    // Rimuove la visita dal salvataggio in memoria
                    salvataggio.Remove(visitaDaEliminare);
                    EliminaVisitaDaFile(visitaDaEliminare);
                    MessageBox.Show("Visita eliminata con successo.", "Operazione completata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormatDataGriew();
                }
            }
            // Rimuove la visita dallo storico se è stata selezionata una riga nella Tabella_Storico
            else if (Tabella_Storico.SelectedRows.Count > 0)
            {
                int storicoRowIndex = Tabella_Storico.SelectedRows[0].Index;
                Storico storicoDaEliminare = storicos[storicoRowIndex];

                // Mostra una conferma prima di eliminare la visita dallo storico
                DialogResult confermaStorico = MessageBox.Show("Sei sicuro di voler eliminare questa visita dallo storico?", "Conferma eliminazione visita dallo storico", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confermaStorico == DialogResult.Yes)
                {
                    storicos.RemoveAt(storicoRowIndex);
                    // Aggiorna la DataGridView dello storico
                    Tabella_Storico.DataSource = null;
                    Tabella_Storico.DataSource = storicos;

                    storicoListSalvataggio.Remove(storicoDaEliminare);  
                    EliminaVisitaDaStorico(storicoDaEliminare);
                    MessageBox.Show("Visita eliminata con successo.", "Operazione completata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormatDataGriew();
                }
            }
            else
            {
                MessageBox.Show("Seleziona una visita da eliminare.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Elimina una visita dal file CSV delle visite programmate
        private void EliminaVisitaDaFile(VisitaProgrammata visitaDaEliminare)
        {
            string filePath = "CSV\\Prova.csv";
            try
            {
                // Crea una lista temporanea delle visite da mantenere
                List<VisitaProgrammata> visiteDaMantenere = new List<VisitaProgrammata>();
                foreach (var visita in salvataggio)
                {
                    if (!(visita.IDPersona == visitaDaEliminare.IDPersona && visita.IDVisitaMedica == visitaDaEliminare.IDVisitaMedica))
                    {
                        visiteDaMantenere.Add(visita);
                    }
                }

                // Riscrive il file solo con le visite da mantenere
                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    foreach (var visita in visiteDaMantenere)
                    {
                        sw.WriteLine(visita.ToCsvString());
                    }
                }

                // Aggiorna la lista salvataggio con le visite rimanenti
                salvataggio = new ObservableCollection<VisitaProgrammata>(visiteDaMantenere);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante l'eliminazione della visita nel file: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Elimina una visita dallo storico se è stata selezionata una riga nella Tabella_Storico
        private void EliminaVisitaDaStorico(Storico storicoDaEliminare)
        {
            if (storicoDaEliminare != null)
            {
                string filePath = "CSV\\Storico.csv";
                try
                {
                    // Crea una lista temporanea delle voci dello storico da mantenere
                    List<Storico> storicoDaMantenere = new List<Storico>();
                    foreach (var storico in storicoListSalvataggio)
                    {
                        // Rimuovi tutte le voci dello storico relative alla persona della visita da eliminare
                        if (!(storico.PersonaID == storicoDaEliminare.PersonaID && storico.VisitaID == storicoDaEliminare.VisitaID))
                        {
                            storicoDaMantenere.Add(storico);
                        }
                    }

                    // Riscrive il file solo con le voci dello storico da mantenere
                    using (StreamWriter sw = new StreamWriter(filePath, false))
                    {
                        foreach (var storico in storicoDaMantenere)
                        {
                            sw.WriteLine(storico.TOCSV());
                        }
                    }

                    // Aggiorna la lista storicos con le voci rimanenti
                    storicoListSalvataggio = new ObservableCollection<Storico>(storicoDaMantenere);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errore durante l'eliminazione della visita dallo storico: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Ottiene il prossimo ID disponibile per lo storico
        private int GetNextStoricoId()
        {
            string storicoFilePath = "CSV\\Storico.csv";
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

        // Gestisce l'evento di formattazione delle celle della DataGridView delle visite programmate
        private void Tabella_Visite_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && Tabella_Visite.Columns[e.ColumnIndex].Name == "DataVisita")
            {
                if (Tabella_Visite.Rows[e.RowIndex].DataBoundItem is VisitaProgrammata visita)
                {
                    string status = "N/A";

                    // Trova tutte le visite mediche della persona corrente
                 
                        status = visita.GetStatus(visita.DataVisita, visiteMedicaList, visita.IDPersona, personaSelezionata.Rumore);

                        // Assegna il colore in base allo stato della visita
                        switch (status)
                        {
                            case "Scaduta":
                                e.CellStyle.BackColor = Color.Red;
                                break;   
                            case "In scadenza":
                                e.CellStyle.BackColor = Color.Yellow;
                                break;
                                
                            case "Valida":
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                break;
                                
                        }

                 
                    // Mostra la data della visita anziché lo stato
                    e.Value = visita.DataVisita;
                }
            }
        }
    }
}
