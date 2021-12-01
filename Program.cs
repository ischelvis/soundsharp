using System;
using System.Collections.Generic;

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
            Console.WriteLine("\n\nMENU\n1. Overzicht mp3 spelers\n2. Overzicht voorraad\n3. Muteer voorraad\n8. Toon menu\n9. exit");
            
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
                        program.UpdateStock();
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

        public void UpdateStock()
        {
        }
    }
}