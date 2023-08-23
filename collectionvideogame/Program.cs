using System;
using System.Data.SqlClient;

namespace collectionvideogame
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CollectionGame;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                while (true)
                {
                    Console.WriteLine("1. Liste des jeux (Tous)");
                    Console.WriteLine("2. Liste des jeux (Par catégorie)");
                    Console.WriteLine("3. Ajouter un jeu");
                    Console.WriteLine("4. Afficher détail d'un jeu");
                    Console.WriteLine("5. Ajouter catégorie"); 
                    Console.WriteLine("6. Lister les catégories"); 
                    Console.WriteLine("7. Modifier"); 
                    Console.WriteLine("8. Quitter");
                    Console.Write("Choisissez une option : ");
                    string choice = Console.ReadLine();
                    Console.WriteLine("\n");
                    Console.Clear();

                    if (choice == "1")
                    {
                        ListJeux(connection);
                    }
                    else if (choice == "2")
                    {
                        Console.Write("Entrez le nom de la catégorie (Action, Aventure, Stratégie) : ");
                        string categorie = Console.ReadLine();
                        ListJeuxParCategorie(connection, categorie);
                    }
                    else if (choice == "3")
                    {
                        AjouterJeu(connection);
                    }
                    else if (choice == "4")
                    {
                        Console.Write("Entrez le nom du jeu : ");
                        string nomJeu = Console.ReadLine();
                        AfficherDetailJeu(connection, nomJeu);
                    }
                    else if (choice == "5")
                    {
                        AjouterCategorie(connection);
                    }
                    else if (choice == "6")
                    {
                        ListCategories(connection);
                    }
                    else if (choice == "7")
                    {
                        Console.Write("Entrez le nom du jeu à modifier : ");
                        string nomJeu = Console.ReadLine();
                        ModifierJeu(connection, nomJeu);
                    }
                    else if (choice == "8")
                    {
                        break;
                    }
                }
            }
        }
        static void ListJeux(SqlConnection connection)
        {
            string selectQuery = "SELECT Nom FROM JeuxVideo";
            using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                Console.WriteLine("Liste des jeux vidéo :");
                while (reader.Read())
                {
                    string nom = reader["Nom"].ToString();
                    Console.WriteLine(nom+"\n");
                  
                }
            }
        }

        static void ListJeuxParCategorie(SqlConnection connection, string categorie)
        {
            string selectQuery = "SELECT j.Nom FROM JeuxVideo j " +
                                 "INNER JOIN Categories c ON j.CategorieId = c.Id " +
                                 "WHERE c.Nom = @Categorie";
            using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
            {
                selectCommand.Parameters.AddWithValue("@Categorie", categorie);
                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    Console.WriteLine($"Liste des jeux vidéo de la catégorie '{categorie}':");
                    while (reader.Read())
                    {
                        string nom = reader["Nom"].ToString();
                        Console.WriteLine(nom+"\n");
                    }
                }
            }
        }

        static void AjouterJeu(SqlConnection connection)
        {
            Console.Write("Nom du jeu : ");
            string nomJeu = Console.ReadLine();

            Console.Write("Catégories (séparées par des virgules) : ");
            string categoriesInput = Console.ReadLine();
            string[] categories = categoriesInput.Split(',');

            // Insertion du jeu
            string insertQuery = "INSERT INTO JeuxVideo (Nom) VALUES (@Nom); SELECT SCOPE_IDENTITY();";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@Nom", nomJeu);
                int newGameId = Convert.ToInt32(insertCommand.ExecuteScalar());

                // Ajout des catégories 
                string insertCategoriesQuery = "INSERT INTO CategoriesJeux (JeuxVideoId, CategorieId) VALUES (@JeuxVideoId, @CategorieId)";
                foreach (string categorie in categories)
                {
                    string trimmedCategorie = categorie.Trim();
                    int categorieId = GetCategorieId(connection, trimmedCategorie);
                    using (SqlCommand insertCategoriesCommand = new SqlCommand(insertCategoriesQuery, connection))
                    {
                        insertCategoriesCommand.Parameters.AddWithValue("@JeuxVideoId", newGameId);
                        insertCategoriesCommand.Parameters.AddWithValue("@CategorieId", categorieId);
                        insertCategoriesCommand.ExecuteNonQuery();
                    }
                }
            }

            Console.WriteLine("Jeu ajouté avec succès !");
        }




        static int GetCategorieId(SqlConnection connection, string categorie)
        {
            string selectQuery = "SELECT Id FROM Categories WHERE Nom = @Categorie";
            using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
            {
                selectCommand.Parameters.AddWithValue("@Categorie", categorie);
                object result = selectCommand.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    // Si la catégorie n'existe pas
                    string insertQuery = "INSERT INTO Categories (Nom) VALUES (@Categorie); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Categorie", categorie);
                        return Convert.ToInt32(insertCommand.ExecuteScalar());
                    }
                }
            }
        }

        static void AfficherDetailJeu(SqlConnection connection, string nomJeu)
        {
            string selectQuery = "SELECT j.Nom, c.Nom AS Categorie, c.Id AS CategorieId " +
                                 "FROM JeuxVideo j " +
                                 "INNER JOIN CategoriesJeux cj ON j.Id = cj.JeuxVideoId " +
                                 "INNER JOIN Categories c ON cj.CategorieId = c.Id " +
                                 "WHERE j.Nom = @NomJeu";
            using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
            {

                selectCommand.Parameters.AddWithValue("@NomJeu", nomJeu);
                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nom = reader["Nom"].ToString();
                        string categorie = reader["Categorie"].ToString();
                        int categorieId = Convert.ToInt32(reader["CategorieId"]);
                        Console.WriteLine($"Nom du jeu : {nom}");
                        Console.WriteLine($"Catégorie : {categorie} (ID : {categorieId})");
                    }
                }
            }
        }

        static void AjouterCategorie(SqlConnection connection)
        {
            Console.Write("Nom de la catégorie : ");
            string nomCategorie = Console.ReadLine();

            string insertQuery = "INSERT INTO Categories (Nom) VALUES (@Nom)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@Nom", nomCategorie);
                insertCommand.ExecuteNonQuery();
            }

            Console.WriteLine("Catégorie ajoutée avec succès !");
        }

        static void ListCategories(SqlConnection connection)
        {
            string selectQuery = "SELECT Nom FROM Categories";
            using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                Console.WriteLine("Liste des catégories :");
                while (reader.Read())
                {
                    string nom = reader["Nom"].ToString();
                    Console.WriteLine(nom + "\n");
                }
            }
        }

        static void ModifierJeu(SqlConnection connection, string nomJeu)
        {
            Console.Write("Nouveau nom du jeu : ");
            string nouveauNom = Console.ReadLine();

            string updateQuery = "UPDATE JeuxVideo SET Nom = @NouveauNom WHERE Nom = @NomJeu";
            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {
                updateCommand.Parameters.AddWithValue("@NouveauNom", nouveauNom);
                updateCommand.Parameters.AddWithValue("@NomJeu", nomJeu);
                int rowsAffected = updateCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Jeu modifié avec succès !");
                }
                else
                {
                    Console.WriteLine("Aucun jeu trouvé avec le nom spécifié.");
                }
            }
        }
    }
}
