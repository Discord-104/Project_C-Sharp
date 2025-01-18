using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace VisiteTTMediche
{
   
    public class Dati
    {
        // Campi privati che rappresentano le collezioni di oggetti
        private ObservableCollection<Persona> persone;
        private ObservableCollection<FiguraAziendale> figureAziendali;
        private ObservableCollection<Storico> storico;
        private ObservableCollection<Persona> cestino;
        private ObservableCollection<VisitaMedica> visitaMedica;

        // Proprietà pubbliche per accedere alle collezioni
        public ObservableCollection<Persona> Persone { get => persone; set => persone = value; }
        public ObservableCollection<FiguraAziendale> FigureAziendali { get => figureAziendali; set => figureAziendali = value; }
        public ObservableCollection<Storico> Storico { get => storico; set => storico = value; }
        public ObservableCollection<Persona> Cestino { get => cestino; set => cestino = value; }
        public ObservableCollection<VisitaMedica> VisitaMedica { get => visitaMedica; set => visitaMedica = value; }

        // Costruttore della classe Dati
        public Dati()
        {
            // Inizializza tutte le collezioni nel costruttore
            Persone = new ObservableCollection<Persona>();
            FigureAziendali = new ObservableCollection<FiguraAziendale>();
            Storico = new ObservableCollection<Storico>();
            Cestino = new ObservableCollection<Persona>();
            VisitaMedica = new ObservableCollection<VisitaMedica>();
        }

        // Metodo per salvare la lista delle persone nel cestino in formato CSV
        public void SalvaCestinoCSV(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var persona in Cestino)
                    {
                        writer.WriteLine(persona.TOCSV());
                    }
                }
                Console.WriteLine("Cestino salvato correttamente: " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante il salvataggio del cestino: " + ex.Message);
            }
        }

        // Metodo statico per caricare la lista delle persone da un file CSV
        public static ObservableCollection<Persona> CaricaPersoneDaCSV(string filePath)
        {
            var persone = new ObservableCollection<Persona>();

            try
            {
                if (!File.Exists(filePath))
                {
                    return persone; // Se il file non esiste, ritorna una collezione vuota
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var persona = Persona.parse(line);
                        persone.Add(persona);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante il caricamento delle persone: " + ex.Message);
            }

            return persone;
        }

        // Metodo per caricare la lista delle persone dal cestino da un file CSV
        public ObservableCollection<Persona> CaricaCestinoDaCSV(string filePath)
        {
            var cestino = new ObservableCollection<Persona>();

            try
            {
                if (!File.Exists(filePath))
                {
                    return cestino; // Se il file non esiste, ritorna una collezione vuota
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var persona = Persona.parse(line);
                        cestino.Add(persona);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante il caricamento del cestino: " + ex.Message);
            }

            return cestino;
        }

        // Metodo per aggiungere una persona al cestino
        public void AggiungiAlCestino(Persona persona)
        {
            Cestino.Add(persona); // Aggiunge la persona alla collezione Cestino
            SalvaCestinoCSV("CSV\\Cestino.csv"); // Salva la collezione aggiornata nel file CSV del cestino
        }

        // Metodo per rimuovere una persona dal cestino
        public void RimuoviDalCestino(Persona persona)
        {
            Cestino.Remove(persona); // Rimuove la persona dalla collezione Cestino
            SalvaCestinoCSV("CSV\\Cestino.csv"); // Salva la collezione aggiornata nel file CSV del cestino
        }

        // Metodo per ripristinare una persona dal cestino alla lista principale delle persone
        public void RipristinaDalCestino(Persona persona)
        {
            Persone.Add(persona); // Aggiunge la persona alla collezione Persone
            Cestino.Remove(persona); // Rimuove la persona dalla collezione Cestino
            SalvaCestinoCSV("CSV\\Cestino.csv"); // Salva la collezione aggiornata nel file CSV del cestino
        }
    }
}
