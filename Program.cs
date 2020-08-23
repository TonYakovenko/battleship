using System;
using Battleship.model;
using System.Threading.Tasks;


namespace Battleship
{
    class Program
    {
        static Board OpponentMap = new Board();
        static Board PlayerMap = new Board();
        static int PlayerShips = 10;
        static int OpponentShips = 10;

        static Opponent OpponentPlayer;

        static bool IsUserTurn = true;


        static void Main()
        {
            //Console.Clear();
            PlayerMap.createMap(Maps.Fleet2);
            OpponentMap.createMap(Maps.Fleet);
            OpponentPlayer = new Opponent();
            UpdateBoards();
            
            NextTurn();
        }

        static void NextTurn()
        {
            if(PlayerShips <= 0 || OpponentShips <= 0)
            {
                System.Console.WriteLine("Game Over");
                Environment.Exit(0);
            }

            if(IsUserTurn)
            {
                UserTurn();
            }
            else
            {
                Task.Delay(1000).Wait();
                ComputerTurn();
            }
        }

        static void UserTurn()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("Please write coordinates:");

            Console.ForegroundColor = ConsoleColor.Green;
            string input = Console.ReadLine();
            string[] inpArr = input.Split(",");
            int inp1 = Int32.Parse(inpArr[0]);
            int inp2 = Int32.Parse(inpArr[1]);
            string shotResult = OpponentMap.MakeShot(inp1, inp2);
            UpdateBoards();

            if(shotResult == "Hit" || shotResult == "Destroyed")
            {
                if(shotResult == "Destroyed") 
                {
                    OpponentShips--;
                }
                IsUserTurn = true;
            }
            else
            {
                IsUserTurn = false;
            }
            NextTurn();
        }

        static void ComputerTurn()
        {
            System.Console.WriteLine("Computer turn!");

            int guessIndex = OpponentPlayer.MakeGuess();
            decimal dec = guessIndex / Board.Col;
            int inp1 = (int)Math.Floor(dec);
            int inp2 = guessIndex - inp1*Board.Col;
            string shotResult = PlayerMap.MakeShot(inp1, inp2);
            UpdateBoards();
            OpponentPlayer.ShotResult(shotResult);

            if(shotResult == "Hit" || shotResult == "Destroyed")
            {
                IsUserTurn = false;
                if(shotResult == "Destroyed") 
                {
                    PlayerShips--;
                    PlayerMap.RevealMultipleSpots(OpponentPlayer.GetClearedIndexesAroundTheShip());
                }
            }
            else
            {
                IsUserTurn = true;
            }
            NextTurn();
        }

        private static void UpdateBoards()
        {
            //Console.Clear();
            OpponentMap.showBoard();
            PlayerMap.showBoard();
        }
    }
}
