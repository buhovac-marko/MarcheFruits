namespace MarcheFruits
{
    class Program
    {
        // Taille maximale du panier (exigée: 5)
        const int CAPACITY = 5;

        static void Main(string[] args)
        {
            string[] basket = new string[CAPACITY];
            int count = 0;

            while (true)
            {
                Console.WriteLine("\n=== Jeux: Le marché ===");
                Console.WriteLine("1) Ajouter un fruit");
                Console.WriteLine("2) Retirer un fruit");
                Console.WriteLine("3) Afficher le panier");
                Console.WriteLine("4) Rechercher un fruit");
                Console.WriteLine("5) Quitter");
                Console.Write("Votre choix: ");

                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        AddFruit(basket, ref count);
                        break;
                    case "2":
                        RemoveFruit(basket, ref count);
                        break;
                    case "3":
                        DisplayBasket(basket, count);
                        break;
                    case "4":
                        SearchFruit(basket, count);
                        break;
                    case "5":
                        Console.WriteLine("Au revoir!");
                        return;
                    default:
                        Console.WriteLine("Choix invalide. Réessayez.");
                        break;
                }
            }
        }

        // Lit une entrée non-vide de l'utilisateur
        static string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();
                Console.WriteLine("Entrée vide. Réessayez.");
            }
        }

        // Trouve l'index d'un fruit (recherche insensible à la casse); -1 si absent
        static int FindIndex(string[] basket, int count, string fruit)
        {
            for (int i = 0; i < count; i++)
            {
                if (string.Equals(basket[i], fruit, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

        // 1) Ajouter
        static void AddFruit(string[] basket, ref int count)
        {
            if (count >= CAPACITY)
            {
                Console.WriteLine("Panier plein (5 fruits max).");
                return;
            }

            string fruit = ReadNonEmpty("Nom du fruit à ajouter: ");

            // Gérer les doublons: ne pas autoriser
            if (FindIndex(basket, count, fruit) != -1)
            {
                Console.WriteLine($"\"{fruit}\" est déjà dans le panier. (Doublons non autorisés.)");
                return;
            }

            basket[count] = fruit;
            count++;
            Console.WriteLine($"Ajouté: {fruit}. ({count}/{CAPACITY})");
        }

        // 2) Retirer
        static void RemoveFruit(string[] basket, ref int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Le panier est vide.");
                return;
            }

            string fruit = ReadNonEmpty("Nom du fruit à retirer: ");
            int idx = FindIndex(basket, count, fruit);
            if (idx == -1)
            {
                Console.WriteLine($"\"{fruit}\" n'est pas dans le panier.");
                return;
            }

            // Décaler à gauche pour “compacter”
            for (int i = idx; i < count - 1; i++)
            {
                basket[i] = basket[i + 1];
            }
            basket[count - 1] = null;
            count--;

            Console.WriteLine($"Retiré: {fruit}. ({count}/{CAPACITY})");
        }

        // 3) Afficher
        static void DisplayBasket(string[] basket, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Panier vide.");
                return;
            }

            Console.WriteLine("\nContenu du panier:");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"- {basket[i]}");
            }
        }

        // 4) Rechercher
        static void SearchFruit(string[] basket, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Panier vide.");
                return;
            }

            string fruit = ReadNonEmpty("Nom du fruit à rechercher: ");
            int idx = FindIndex(basket, count, fruit);

            if (idx == -1)
                Console.WriteLine($"\"{fruit}\" n'a pas été trouvé.");
            else
                Console.WriteLine($"Trouvé: \"{basket[idx]}\" à la position {idx + 1}.");
        }
    }
}
