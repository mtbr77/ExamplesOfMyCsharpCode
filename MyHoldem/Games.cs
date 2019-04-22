using System;


namespace Holdem
{
    class Games
    {
        public PoketHand[] BestPoketHands
        {
            get;
            set;
        }

        public Games(int NumberOfGames)
            
        {
            BestPoketHands = new PoketHand[NumberOfGames];
            for (int i = 0; i < NumberOfGames; ++i)
            {
                byte NumberOfPlayers = 2;
                //Game game = new Game(NumberOfPlayers);
                //BestPoketHands[i] = game.GetBestPoketHandOnFlop();
            }
            Array.Sort(BestPoketHands);
        }

        public static void generateCasinoHoldemGames(int numberOfGames)
        {
            int profit = 0;
            for (int i = 0; i < numberOfGames; ++i)
            {
                byte NumberOfPlayers = 2;
                Game game = new Game(NumberOfPlayers, 1);
                profit += game.getProfit();
            }

            Console.Write("Profit = " + profit);
        }

        public void PrintBestPoketHandsOnFlop()
        {
            foreach(PoketHand x in BestPoketHands)
            {
                x.PrintBest("");
            }
            
        }
    }
}
