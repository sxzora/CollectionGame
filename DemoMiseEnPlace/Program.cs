using DemoMiseEnPlace;
using TodoDataAccessLayer;

Affichage affichage = new Affichage();

Console.WriteLine("Que voulez vous faire ? ");
Console.WriteLine("1 - Afficher les taches");
Console.WriteLine("2 - Creer une tache");

int choix = int.Parse(Console.ReadLine());

switch(choix)
{
    case 1: affichage.AfficherListe();
        break;
    case 2: affichage.CreerNouvelleTache();
        break;

    default: Console.WriteLine("C'est un ou deux gros noob !!!");
        break;
}

