using Console_Quizspel.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Quizspel
{
    internal class Program
    {
        //Ruben: mapje maken voor verschillende soorten classes om makkelijker te maken om code te vinden. // dit wou ik proberen en toen brak mijn programma, dus voor de zekerheid doe ik dit niet, Anders had ik het opgesteld door verschillende soorten class types zoals constructors
        //Ruben: als er geen vragen zijn in je programma dan stopt je programma misschien zorgen dat die dan weer terug gaat naar het menu om een andere optie te vinden // Check
        //Ruben: ik krijg wel te horen dat mijn score in de database is gezet maar ik krijg nergens mijn score te zien? // Check
        //Ruben: als ik vragen wilt aanpassen krijg ik wel de vraag welke vraag wil je aanpassen maar ik zie niet de vragen die er zijn? // Dit doet raar door de connectie en durf ik nu op dit moment niet aan te passen
        //ik krijg ze we te zien als ik klik op laat alle vragen zien   

        // bij de update en vraag functie is een cascade gebruikt
        // een cascade is een soort trigger event wat bij relaties wordt toegevoegd
        // in dit geval doet hij dit op basis van een update en delete zodat de vraag en answer table samen wordt verwijderd en verwijderd

        // Marijn: Na het reviewen van de code heb ik zelf meer geleerd over classes, database connecties en switch cases (hiervoor wil ik ruben erg bedanken)
        // Marijn: Ik vind het zelf wel jammer dat ik niet goed de connectie class heb kunnen gebruiken, maar in de toekomst kan ik dit nu wel veel efficiënter gebruiken.

        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.menu();
        }
        
    }
}
