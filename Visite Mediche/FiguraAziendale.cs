using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiteTTMediche
{
    // Classe che rappresenta una figura aziendale associata a visite mediche
    public class FiguraAziendale
    {
        // Proprietà per il nome della figura aziendale
        public string Nome { get; set; }

        // Lista delle visite mediche associate alla figura aziendale
        public List<VisitaMedica> Visite { get; set; }

        // Costruttore che inizializza una nuova istanza della classe FiguraAziendale
        public FiguraAziendale(string nome)
        {
            Nome = nome; // Assegna il nome passato come parametro
            Visite = new List<VisitaMedica>(); // Inizializza la lista delle visite mediche
        }

        // Metodo statico per creare una nuova istanza di FiguraAziendale da una stringa
        public static FiguraAziendale Parse(string row)
        {
            string nome = row; // Assume che il nome sia passato direttamente come stringa
            return new FiguraAziendale(nome); // Restituisce una nuova istanza di FiguraAziendale con il nome specificato
        }

        // Metodo per ottenere la lista delle visite predefinite associate alla figura aziendale
        public List<VisitaMedica> GetVisitePredefinite()
        {
            return Visite; // Restituisce l'intera lista delle visite associate alla figura aziendale
        }
    }
}
