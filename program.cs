using System;
using System.Collections.Generic;
using System.Linq;

namespace Opdracht
{
    class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            program.LogIn();
            program.ShowMenu();
        }

        public void LogIn()
        {
            Console.WriteLine("Wat is je naam?");
            string name = Console.ReadLine();

            Console.WriteLine("Wat is het password?");

            const int maxAttempt = 3;
            int attempt = 1;
            bool exit = false;

            while (attempt <= maxAttempt && exit == false)
            {
                string password = Console.ReadLine();
                int passReturn;

                if (password == "SHARPSOUND")
                {
                    passReturn = (int)PassReturnOptions.Ok;
                }
                else
                {
                    passReturn = (int)PassReturnOptions.NotOk;
                }

                switch (passReturn)
                {
                    case 0:
                        Console.WriteLine($"Welkom bij SoundSharp {name}");
                        exit = true;
                        break;
                    case 1:
                        attempt++;

                        if (attempt == 4)
                        {
                            Environment.Exit(0);
                        }

                        Console.WriteLine("Password is onjuist");
                        Console.WriteLine($"Poging {attempt} van 3");
                        if (attempt == 3)
                        {
                            Console.WriteLine("LET OP: Laatste poging!");
                        }
                        break;
                }
            }

        }

        private enum PassReturnOptions
        {
            Ok = 0,
            NotOk = 1
        }

        public void ShowMenu()
        {
            Console.WriteLine("\n\nMENU\n1. Overzicht mp3 spelers\n2. Overzicht voorraad\n3. Muteer voorraad\n4. Bereken statistieken\n8. Toon menu\n9. exit");

            bool exit = false;
            List<Product> productRange = ProductRange();
            var program = new Program();
            while (exit == false)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                var keyPressed = keyInfo.Key;

                switch (keyPressed)
                {
                    case ConsoleKey.D1:

                        foreach (var product in productRange)
                        {
                            Console.WriteLine($"\n\nmp3 speler {product.Id}:\n\nMerk:\t\t\t{product.Make}\nModel:\t\t\t{product.Model}\nOpslagcapaciteit:\t{product.MbSize}\nPrijs:\t\t\t{product.Price}");
                        }
                        break;
                    case ConsoleKey.D2:
                        foreach (var product in productRange)
                        {
                            Console.WriteLine($"\nmp3 speler {product.Id}:\nVoorraad:\t{product.Stock}");
                        }
                        break;
                    case ConsoleKey.D3:
                        program.UpdateStock(productRange);
                        break;
                    case ConsoleKey.D4:
                        program.StatisticalData(productRange);
                        break;
                    case ConsoleKey.D8:
                        program.ShowMenu();
                        break;
                    case ConsoleKey.D9:
                        Console.WriteLine("2");
                        exit = true;
                        break;
                }
            }
        }

        List<Product> ProductRange() 
        {
            List<Product> mp3Players = new List<Product>();
            mp3Players.Add(new Product(1, "GET technologies .inc", "HF 410", 4096, 129.95, 500));
            mp3Players.Add(new Product(2, "Far & Loud", "XM 600", 8192, 224.95, 500));
            mp3Players.Add(new Product(3, "Innotivative", "Z3", 512, 79.95, 500));
            mp3Players.Add(new Product(4, "Resistance S.A.", "3001", 4096, 124.95, 500));
            mp3Players.Add(new Product(5, "CBA", "NXT volume", 2048, 159.05, 500));

            return mp3Players;
        }

        public void UpdateStock(List<Product> products)
        {
            // vragen aan de gebruiker welke id we willen aanpassen
            Console.WriteLine("Van welk product wil je de voorraad aanpassen?");
            
            int id = 0;
            bool canAdd = false;
            
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Vul een nummer in");
                UpdateStock(products);
            }
            // controle of het id wel bestaat
            foreach (var product in products)
            {
                if (product.Id == id)
                {
                    canAdd = true;
                    
                    break;
                }
            }

            if (!canAdd)
            {
                Console.WriteLine("Dit product bestaat niet, probeer het opnieuw.");
                UpdateStock(products);
            }
            
            int mutation = 0;
            bool exit = false;
            while (!exit)
            {
                // vragen hoeveel de stock is veranderd
                Console.WriteLine("Wat is de mutatie op de voorraad?");
                
                try
                {
                    mutation = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Vul een nummer in");
                }
                
                // de stock veranderen
                foreach (var product in products)
                {
                    if (product.Id == id && product.Stock + mutation > 0)
                    {
                        product.Stock = product.Stock + mutation; // hier de nieuwe stock invullen
                        Console.WriteLine($"De voorraad van product {id} is nu {product.Stock}");
                        exit = true;
                    }
                    else if (product.Stock + mutation < 0)
                    {
                        Console.WriteLine("De voorraad van dit product kan niet lager dan 0 zijn.");
                        break;
                    }
                }
            }
        }

        public void StatisticalData(List<Product> products)
        {
            List<Product> productRange = ProductRange();
            
            // Bereken aantal mp3 spelers in voorraad
            int totalStock = 0;
            foreach (var product in productRange)
            {
                totalStock = totalStock + product.Stock;
            }
            Console.WriteLine($"Aantal mp3 spelers in voorraad: {totalStock}");
            
            // Bereken totaale waarde van voorraad
            double totalWorth = 0;
            foreach (var product in productRange)
            {
                totalWorth = totalWorth + product.Stock * product.Price;
            }
            Console.WriteLine($"Totale waarde van de voorraad: {totalWorth}");
            
            // Bereken gemiddelde prijs per mp3 speler
            double averagePrice = totalWorth / totalStock;
            Console.WriteLine($"Gemiddelde prijs per product: {averagePrice}");

            // Bereken beste prijs per mB mp3 speler
            int bestValueProduct = 0;
            double valueForMoney;
            double previousValueForMoney = 0;
            foreach (var product in productRange)
            {
                if (productRange.First() == product)
                {
                    previousValueForMoney = product.Price / product.MbSize;
                }
                valueForMoney = product.Price / product.MbSize;
                if (previousValueForMoney >= valueForMoney)
                {
                    previousValueForMoney = valueForMoney;
                    bestValueProduct = product.Id;
                }
            }
            Console.WriteLine($"Mp3 speler met beste prijs per mB: {bestValueProduct}");
        }
    }
} 