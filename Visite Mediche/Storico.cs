using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiteTTMediche
{
    // Classe Storico rappresenta un record storico di una visita medica associata a una persona
    public class Storico
    {
        // Proprietà dell'ID univoco del record storico
        public int ID { get; set; }

        // Proprietà dell'ID della persona associata al record storico
        public int PersonaID { get; set; }

        // Proprietà dell'ID della visita medica associata al record storico
        public int VisitaID { get; set; }

        // Proprietà del nome della visita medica associata al record storico
        public string NomeVisitaMedica { get; set; }

        // Proprietà della data della visita medica associata al record storico
        public string Data { get; set; }

        // Metodo per convertire il record storico in una stringa formattata CSV
        public String TOCSV()
        {
            return ID + ";" + PersonaID + ";" + VisitaID + ";" + Data;
        }

        public static Storico Parse(string csvLine)
        {
            string[] values = csvLine.Split(';');
            Storico visita = new Storico();
            visita.ID = int.Parse(values[0]);
            visita.PersonaID = int.Parse(values[1]);
            visita.VisitaID = int.Parse(values[2]);
            visita.Data = values[3];
            return visita;
        }
    }
}
