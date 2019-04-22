using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holdem
{
    class Game
    {
        private int profit = 0;
        public PoketHand[] PoketHands
        {
            get; set;
        }

        public Hand[] Hands
        {
            get; set;
        }

        public Deck Deck
        {
            get; set;
        }

        public byte NumberOfPlayers
        {
            get; set;
        }

        public Hand Board
        {
            get;
            set;
        }

        public Flop Flop
        {
            get;
            set;
        }

        public Card Turn
        {
            get;
            set;
        }

        public Card River
        {
            get;
            set;
        }

        public Game(byte NumberOfPlayers, int ante)
            //: this()
        {
            this.NumberOfPlayers = NumberOfPlayers;
            PoketHands = new PoketHand[NumberOfPlayers];
            Hands = new Hand[NumberOfPlayers];
            Deck = new Deck();
            //Deck.Print();

            for (byte i = 0; i < NumberOfPlayers; ++i)
            {
                PoketHands[i] = new PoketHand(Deck.Cards[i], Deck.Cards[i + NumberOfPlayers]);
            }

            Flop = new Flop(Deck.Cards[2 * NumberOfPlayers], Deck.Cards[2 * NumberOfPlayers + 1], Deck.Cards[2 * NumberOfPlayers + 2]);

            //Flop.Print();

            for (byte i = 0; i < NumberOfPlayers; ++i)
            {
                Hands[i] = new Hand(PoketHands[i],Flop);
            }

            /*Turn = Deck.Cards[2 * NumberOfPlayers + 3];
            Turn.Print("   Turn :   ");
            River = Deck.Cards[2 * NumberOfPlayers + 4];
            River.Print("   River :   ");*/
            Console.WriteLine();
            //PrintPoketHands();
            //PrintBestPoketHandOnFlop();
            
        }

        public int getProfit()
        {
            return profit;
        }

        private int GetBestHandIndex()
        {
            int best = 0;
            for (byte i = 0; i < NumberOfPlayers - 1; ++i)
            {
                if (Hands[best] < Hands[i+1])
                {
                    best = i + 1; 
                }
            }
            return best;
        }

        public PoketHand GetBestPoketHandOnFlop()
        {
            return PoketHands[GetBestHandIndex()];
        }

        public void PrintBestPoketHandOnFlop()
        {
            int i = GetBestHandIndex();
            PoketHands[i].PrintBest(Hand.ArrayOfCombi[(int)Hands[i].Combi] + " player " + (i+1) + " win ");
            //Hands[i].Print();
        }

        public void PrintHands()
        {
            for (byte i = 0; i < NumberOfPlayers; ++i)
            {
                Console.Write("Player ", i , " = ");
                Hands[i].Print();
            }
            //Console.WriteLine();
            //Console.ReadLine();
        }

        public void PrintPoketHands()
        {
            for (byte i = 0; i < NumberOfPlayers; ++i)
            {
                Console.Write("Player " +(i+1)+ " = ");
                PoketHands[i].Print();
                Console.WriteLine();
            }
            //Console.WriteLine();
            //Console.ReadLine();
        }


    }
}
