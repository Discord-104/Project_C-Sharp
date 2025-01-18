using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisiteTTMediche
{

    // Classe Persona rappresenta una persona con le relative informazioni e visite mediche programmate
    public class Persona : INotifyPropertyChanged
    {
        // Evento per notificare cambiamenti nelle proprietà
        public event PropertyChangedEventHandler PropertyChanged;

        // Campi privati della classe Persona
        private int id;             // Identificativo univoco della persona
        private string cognome;     // Cognome della persona
        private string nome;        // Nome della persona
        private bool Muletto;       // Flag per indicare se la persona utilizza un muletto
        private bool rumore;        // Flag per indicare se la persona emette rumore
        private string figura;      // Figura professionale della persona
        private ObservableCollection<VisitaMedica> visiteProgrammate; // Lista delle visite mediche programmate per la persona
        private string note;        // Eventuali note aggiuntive sulla persona

        // Costruttore della classe Persona
        public Persona(string cognome, string nome, string figura, string note)
        {
            this.cognome = cognome;
            this.nome = nome;
            this.figura = figura;
            this.note = note;
            this.id = id + 1; // Attenzione: id non viene inizializzato correttamente
            visiteProgrammate = new ObservableCollection<VisitaMedica>(); // Inizializzazione della lista delle visite programmate
        }

        // Proprietà Id per l'accesso e la modifica dell'identificativo della persona
        public int Id { get => id; set => id = value; }

        // Proprietà Cognome per l'accesso e la modifica del cognome della persona
        public string Cognome
        {
            get { return cognome; }
            set
            {
                cognome = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("cognome")); // Notifica il cambiamento della proprietà
            }
        }

        // Proprietà Nome per l'accesso e la modifica del nome della persona
        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("nome")); // Notifica il cambiamento della proprietà
            }
        }

        // Proprietà muletto per l'accesso e la modifica del flag che indica se la persona utilizza un muletto
        public bool muletto
        {
            get { return Muletto; }
            set
            {
                Muletto = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Muletto")); // Notifica il cambiamento della proprietà
            }
        }

        // Proprietà Rumore per l'accesso e la modifica del flag che indica se la persona emette rumore
        public bool Rumore
        {
            get { return rumore; }
            set
            {
                rumore = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("rumore")); // Notifica il cambiamento della proprietà
            }
        }

        // Proprietà Ruolo per l'accesso e la modifica della figura professionale della persona
        public string Ruolo
        {
            get { return figura; }
            set
            {
                figura = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("figura")); // Notifica il cambiamento della proprietà
            }
        }

        // Proprietà Note per l'accesso e la modifica delle note aggiuntive sulla persona
        public string Note { get => note; set => note = value; }

        public ObservableCollection<VisitaMedica> Visite
        {
            get { return visiteProgrammate; }
            set
            {
                visiteProgrammate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("visiteProgrammate"));
            }
        }

        // Metodo che restituisce una stringa formattata CSV con le informazioni della persona
        public String TOCSV()
        {
            return id + ";" + Cognome + ";" + Nome + ";" + muletto + ";" + Ruolo + ";" + Rumore + ";" + Note;
        }

        // Metodo statico per il parsing di una stringa CSV e la creazione di un oggetto Persona
        public static Persona parse(string s)
        {
            string[] campi = s.Split(';');
            Persona visit = new Persona(campi[1], campi[2], campi[4], campi[6]);
            visit.id = int.Parse(campi[0]);
            visit.Muletto = bool.Parse(campi[3]);
            visit.Rumore = bool.Parse(campi[5]);
            return visit;
        }

        // Metodo per aggiungere una visita medica alla lista delle visite programmate per la persona
        public void AggiungiVisita(VisitaMedica visita)
        {
            visiteProgrammate.Add(visita);
        }

        // Override del metodo ToString per restituire una rappresentazione testuale della persona
        public override string ToString()
        {
            return Cognome + " " + Nome;
        }
    }
}
