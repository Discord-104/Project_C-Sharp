using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisiteTTMediche
{
    public partial class Form4 : Form
    {
        private Dati dati; // Oggetto che gestisce i dati dell'applicazione

        // Costruttore della classe Form4 che accetta un oggetto di tipo Dati
        public Form4(Dati dati)
        {
            InitializeComponent();
            this.dati = dati; // Inizializza l'oggetto dati con quello passato dal chiamante
            dati.Cestino = dati.CaricaCestinoDaCSV("CSV\\Cestino.csv"); // Carica il cestino da un file CSV

            AggiornaCestino(); // Aggiorna la visualizzazione del cestino nella ListBox
        }

        // Metodo per aggiornare la visualizzazione del cestino nella ListBox
        private void AggiornaCestino()
        {
            listBoxCestino.Items.Clear(); // Cancella tutti gli elementi nella ListBox
            foreach (var persona in dati.Cestino)
            {
                listBoxCestino.Items.Add(persona); // Aggiunge ogni persona nel cestino alla ListBox
            }
        }

        // Gestore dell'evento click sul pulsante "Ripristina"
        private void ButtonRipristina_Click(object sender, EventArgs e)
        {
            if (listBoxCestino.SelectedItem != null)
            {
                Persona persona = (Persona)listBoxCestino.SelectedItem; // Ottiene la persona selezionata nella ListBox
                dati.RipristinaDalCestino(persona); // Ripristina la persona selezionata dal cestino utilizzando il metodo dati
                AggiornaCestino(); // Aggiorna la visualizzazione del cestino
            }
            else
            {
                MessageBox.Show("Seleziona una persona dal cestino.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Gestore dell'evento click sul pulsante "Pulisci Cestino"
        private void ButtonPulisciCestino_Click(object sender, EventArgs e)
        {
            dati.Cestino.Clear(); // Pulisce il cestino utilizzando il metodo dati
            AggiornaCestino(); // Aggiorna la visualizzazione del cestino
        }

        // Gestore dell'evento click sul pulsante "Elimina Definitivamente"
        private void EliminaDefinitivamente_Click(object sender, EventArgs e)
        {
            if (listBoxCestino.SelectedItem != null)
            {
                Persona persona = (Persona)listBoxCestino.SelectedItem; // Ottiene la persona selezionata nella ListBox
                dati.RimuoviDalCestino(persona); // Rimuove la persona selezionata dal cestino utilizzando il metodo dati
                AggiornaCestino(); // Aggiorna la visualizzazione del cestino
            }
            else
            {
                MessageBox.Show("Seleziona una persona dal cestino.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Gestore dell'evento di chiusura del form
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            dati.SalvaCestinoCSV("CSV\\Cestino.csv"); // Salva il cestino nel file CSV prima della chiusura del form
        }
    }

}
