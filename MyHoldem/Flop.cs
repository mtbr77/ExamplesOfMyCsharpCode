using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holdem
{
    struct Flop
    {
        public Card[] Cards
        {
            get; set;
        }

        private void Init()
        {
            if (Cards == null) Cards = new Card[3];
        }

        public Flop(Card c1, Card c2, Card c3)
            :this()
        {
            Init();
            Cards[0] = c1;
            Cards[1] = c2;
            Cards[2] = c3;
        }

        public Flop(Flop f)
            :this()
        {
            this = f;
        }

        public void Print()
        {
            Console.Write("Flop: ");
            foreach (Card c in Cards)
            {
                c.Print();
            }
        }
    }
}
