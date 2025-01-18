using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiteTTMediche
{
    // Classe VisitaProgrammata rappresenta una visita medica programmata per una persona
    public class VisitaProgrammata : INotifyPropertyChanged
    {
        private int _idPersona;
        private int _idVisitaMedica;
        private string _nomeVisitaMedica;
        private string _dataVisita;

        public int IDPersona
        {
            get { return _idPersona; }
            set
            {
                _idPersona = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("_idPersona")); // Notifica il cambiamento della proprietà
            }
        }

        public int IDVisitaMedica
        {
            get { return _idVisitaMedica; }
            set
            {
                _idVisitaMedica = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("_idVisitaMedica")); // Notifica il cambiamento della proprietà
            }
        }

        public string NomeVisitaMedica
        {
            get { return _nomeVisitaMedica; }
            set
            {
                _nomeVisitaMedica = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("_nomeVisitaMedica")); // Notifica il cambiamento della proprietà
            }
        }

        public string DataVisita
        {
            get { return _dataVisita; }
            set
            {
                _dataVisita = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("_dataVisita")); // Notifica il cambiamento della proprietà
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Metodo per convertire l'oggetto VisitaProgrammata in una stringa CSV
        public string ToCsvString()
        {
            return IDPersona + ";" + IDVisitaMedica + ";" + DataVisita;
        }

        // Metodo statico per convertire una riga CSV in un oggetto VisitaProgrammata
        public static VisitaProgrammata Parse(string csvLine)
        {
            string[] values = csvLine.Split(';');
            VisitaProgrammata visita = new VisitaProgrammata();
            visita.IDPersona = int.Parse(values[0]);
            visita.IDVisitaMedica = int.Parse(values[1]);
            visita.DataVisita = values[2];

            return visita;
        }

        // Metodo per ottenere lo stato corrente di una visita programmata
        public string GetStatus(string dataVisita, ObservableCollection<VisitaMedica> visiteMediche, int idPersona, bool rumore)
        {
            DateTime oggi = DateTime.Today;
            string lower = NomeVisitaMedica.ToLower();

            if (!rumore && lower == "audiometria")
            {
                return "Valida";
            }

            DateTime dataVisitaDateTime = DateTime.Parse(DataVisita);

            // Stampa di debug per IDVisitaMedica e idPersona
            Console.WriteLine($"Cercando visita medica con IDVisitaMedica={IDVisitaMedica} e IDPersona={idPersona}");

            var visitaMedica = visiteMediche.FirstOrDefault(v => v.ID == IDVisitaMedica && v.IDPersona == idPersona);
            if (visitaMedica == null)
            {
                // Stampa di debug per il contenuto di visiteMediche
                foreach (var visita in visiteMediche)
                {
                    Console.WriteLine($"Visita medica: ID={visita.ID}, IDPersona={visita.IDPersona}");
                }
                Console.WriteLine($"IDVisitaMedica: {IDVisitaMedica}");
                // Stampa per debug
                Console.WriteLine("Visita medica non trovata.");
                throw new Exception("Visita medica non trovata.");
            }

            DateTime prossimaVisita = dataVisitaDateTime.AddMonths(visitaMedica.Frequenza_Mesi);

            if (oggi > prossimaVisita)
            {
                return "Scaduta";
            }
            else if ((prossimaVisita - oggi).TotalDays <= 30)
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
