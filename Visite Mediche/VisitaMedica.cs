using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiteTTMediche
{
    // Classe VisitaMedica rappresenta una visita medica associata a una persona
    public class VisitaMedica : INotifyPropertyChanged
    {
        // Proprietà dell'ID univoco della visita medica
        public int ID { get; set; }

        // Proprietà del nome della visita medica
        public string Visita { get; set; }

        // Proprietà della frequenza in mesi della visita medica
        public int Frequenza_Mesi { get; set; }

        // Proprietà della data della visita medica
        public DateTime Data { get; set; }

        // Proprietà dell'ID della persona associata alla visita medica
        public int IDPersona { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        // Metodo statico per ottenere la frequenza in mesi di una visita medica dato il suo ID
        public static string GetFrequenza(int idVisitaMedica, string visiteMedicheFilePath, out int frequenza)
        {
            string nomeVisita = null;
            frequenza = 0; // Inizializziamo la frequenza a zero

            try
            {
                using (StreamReader sr = new StreamReader(visiteMedicheFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] columns = line.Split(';');
                        if (columns.Length >= 3 && int.TryParse(columns[0], out int id) && id == idVisitaMedica)
                        {
                            nomeVisita = columns[1];
                            frequenza = int.Parse(columns[2]); // Recuperiamo la frequenza dalla colonna 3
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il caricamento della visita medica: " + ex.Message);
            }

            return nomeVisita;
        }

        // Metodo statico per ottenere il nome di una visita medica dato il suo ID
        public static string GetNomeVisita(int idVisitaMedica, string visiteMedicheFilePath)
        {
            string nomeVisita = null;
            try
            {
                using (StreamReader sr = new StreamReader(visiteMedicheFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] columns = line.Split(';');
                        if (columns.Length >= 2 && int.TryParse(columns[0], out int id) && id == idVisitaMedica)
                        {
                            nomeVisita = columns[1];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il caricamento della visita medica: " + ex.Message);
            }
            return nomeVisita;
        }

        // Metodo per ottenere lo stato corrente di una visita medica
        public string GetStatus(DateTime dataVisita, ObservableCollection<VisitaMedica> visiteMediche, int idPersona, bool rumore)
        {
            DateTime oggi = DateTime.Today;
            string lower = Visita.ToLower();

            // Se la visita è di tipo "audiometria" e c'è rumore, ritorna "Valida"
            if (!rumore && lower == "audiometria")
            {
                return "Valida";
            }

            DateTime dataVisitaDateTime = dataVisita;

            // Cerca la visita medica corrente nella collezione visiteMediche
            var visitaMedica = visiteMediche.FirstOrDefault(v => v.ID == this.ID && v.IDPersona == idPersona);
            if (visitaMedica == null)
            {
                throw new Exception("Visita medica non trovata.");
            }

            // Calcola la data della prossima visita aggiungendo la frequenza in mesi
            DateTime nextVisit = dataVisitaDateTime.AddMonths(visitaMedica.Frequenza_Mesi);
            int giorniDifferenza = (nextVisit - oggi).Days;

            // Determina lo stato in base alla differenza di giorni tra oggi e la prossima visita
            if (giorniDifferenza < 0)
            {
                return "Scaduta";
            }
            else if (giorniDifferenza <= 30)
            {
                return "In scadenza";
            }
            else
            {
                return "Valida";
            }
        }
    }
}
