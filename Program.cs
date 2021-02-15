using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {


            InputGameElements(args);
            string secretKeyfromPC = CreateSecretKey();
            Dictionary<int, string> movesDictionary = new Dictionary<int, string>();
            for (int i = 0; i < args.Length; i++)
                {
                    movesDictionary.Add(i + 1, args[i]);
                }
            ShowMenu(args);
            int PCmove = new Random().Next(1, args.Length);
            Console.WriteLine($"HMAC: {HMACGenerate(PCmove, secretKeyfromPC)}");
            int playerMove = GetMove(args);
            Console.WriteLine($"Player move: {movesDictionary.FirstOrDefault(m=>m.Key==playerMove).Value}");
            Console.WriteLine($"PC move: {movesDictionary.FirstOrDefault(m => m.Key == PCmove).Value}");
            GetWinner(args, playerMove, PCmove);
            Console.WriteLine($"HMAC key: {secretKeyfromPC}");

        }

        private static void InputGameElements(string[] arguments)
        {

            var inputSet = new HashSet<string>(arguments);
            if (arguments.Length != inputSet.Count)
            {
                Console.WriteLine("Elements must be unique. Please try again.");
                Environment.Exit(1);
            }

            if (arguments.Length < 3)
            {
                Console.WriteLine("Number of input elements must be >=3. Please try again.");
                Environment.Exit(1);
            }
            if (arguments.Length % 2 == 0)
            {
                Console.WriteLine("Input must be odd. Please try again.");
                Environment.Exit(1);
            }
        }
        private static string CreateSecretKey()
        {
            var random = RandomNumberGenerator.Create();
            var bytes = new byte[16];
            random.GetBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }
        private static string HMACGenerate(int PCmove, string secretKey)
        {


            byte[] choiseToByte = BitConverter.GetBytes(PCmove);
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            return Convert.ToBase64String(hmac.ComputeHash(choiseToByte));

        }
        private static void ShowMenu(string[] arguments)
        {
            Console.WriteLine("Available moves: ");
            for (var i = 0; i < arguments.Length; i++)
            {
                Console.WriteLine($"{i + 1} ---- {arguments[i]}");
            }
            Console.WriteLine("0 ---- Exit");
        }
        

        private static int GetMove(string[] arguments)
        {

            int playerMove;
            while (true)
            {
                string inputFromPlayer = Console.ReadLine();
                if (inputFromPlayer == "0")
                {
                    Console.WriteLine("Good bye sweet prince");
                    Environment.Exit(1);
                }
                else if (Convert.ToInt32(inputFromPlayer) < 0 || Convert.ToInt32(inputFromPlayer) > arguments.Length)
                {
                    
                    Console.WriteLine("Please choose available number from menu: ");
                    ShowMenu(arguments);
                    continue;

                }
                else
                {
                    return playerMove = Convert.ToInt32(inputFromPlayer);
                }
                
            }
           
        }



        private static void GetWinner(string[] arguments, int playerMove, int PCmove)
            {
                    if (PCmove == playerMove)
                        {
                            Console.WriteLine("Draw!");
                        }
                    else
                    {
                        int halfOfRestElements = (arguments.Length - 1) / 2;
                        int minimumValue = (PCmove < playerMove) ? PCmove : playerMove;
                        int maximumValue = (PCmove > playerMove) ? PCmove : playerMove;
                        var movesDictionary = new Dictionary<int, string>
                    {
                        { minimumValue, ((minimumValue == PCmove) ? "PC" : "Player") },
                        { maximumValue, ((maximumValue == PCmove) ? "PC" : "Player") }
                    };


                    if (minimumValue + halfOfRestElements >= maximumValue)
                        {
                            Console.WriteLine($"{movesDictionary[maximumValue]} - win!");
                        }
                    else
                        {
                            Console.WriteLine($"{movesDictionary[minimumValue]} - win!");
                        }
                    }
            }
       

        
       


        

           

        


        
            


            

            


            
            

       


    }
}
