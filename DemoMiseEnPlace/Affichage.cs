using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoDataAccessLayer;

namespace DemoMiseEnPlace
{
    internal class Affichage
    {
        public void CreerNouvelleTache()
        {
            Todo nouveau = new Todo();

            Console.Write("Nom de votre tâche : ");
            nouveau.Nom = Console.ReadLine();

            Console.Write("Description : ");
            nouveau.Description = Console.ReadLine();

            Console.Write("Priorite (0 à 5) :");
            nouveau.Priorite = int.Parse(Console.ReadLine());

            nouveau.DateLimite = DateTime.Now;

            TodoService service = new TodoService();
            service.Create(nouveau);

            Console.WriteLine("Tâche créée avec succès!");

            Console.WriteLine("Liste des tâches :");
            foreach (Todo tache in service.GetAll())
            {
                Console.WriteLine($"Nom : {tache.Nom}");
                Console.WriteLine($"Description : {tache.Description}");
                Console.WriteLine($"Priorité : {tache.Priorite}");
                Console.WriteLine($"Date limite : {tache.DateLimite}");
                Console.WriteLine("--------------");
            }
        }


        public void AfficherListe()
        {
            TodoService service = new TodoService();
            foreach (Todo t in service.GetAll())
            {
                Console.WriteLine($"{t.Id} - {t.Nom} - {t.IsFinished}");
            }
        }
    }
}
