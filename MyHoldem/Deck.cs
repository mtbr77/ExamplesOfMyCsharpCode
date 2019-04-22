using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holdem
{
    class Deck
    {
        public static Card[] Cards
        {
            get; set;
        }

        static Deck()
        {
            Cards = new Card[52];
            for (byte i = 0; i < 13; ++i)
            {
                for (byte j = 0; j < 4; ++j)
                {
                    Cards[4*i+j] = (new Card(i, j));
                }
            }   
        }

        public Deck()
        {
            Shuffle();
        }

        public void Shuffle()
        {
            //ArrayList CopyOfMyDeck = new ArrayList(Cards);

            //Random ran1 = new Random();
            //int r /*= ran1.Next()*/;
            //Random ran2 = new Random(r);
            Random ran1 = new Random();
            for (byte i = 0; i < 52; i++)
            {
                int r = ran1.Next(i, 52) /*+ ran1.Next(0, i)) % i*/;
                Cards[i].Change(ref Cards[r]);
                //CopyOfMyDeck.RemoveAt(r);
            }
        }


        public void Print()
        {
            for (byte i = 0; i < 52; ++i)
            {
                Cards[i].Print();
                Console.Write(" ");
                if ( (i+1) % 9 == 0) Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
            //Console.ReadLine();
        }

        

    }//Deck


}
