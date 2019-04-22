using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holdem
{
    struct PoketHand : IComparable
    {
        public Card[] Cards
        {
            get; set;
        }

        private void Init()
        {
            if (Cards == null) Cards = new Card[2];
        }

        public PoketHand(Card c1, Card c2)
            : this()
        {
            Init();
            Cards[0] = c1;
            Cards[1] = c2;
            Array.Sort(Cards);
        }

        public PoketHand(PoketHand ph)
        {
            this = ph;
        }

        public void PrintBest(string s)
        {
            //Console.WriteLine(); 
            for (byte i = 0; i < 2; ++i)
            {
                (Cards[i]).Print();
            }
            Console.WriteLine(" -Best Hand " + s);
            //Console.ReadLine();
        }

        public void Print()
        {
            //Console.WriteLine(); 
            for (byte i = 0; i < 2; ++i)
            {
                (Cards[i]).Print();
            }
            
            //Console.ReadLine();
        }

        public int CompareTo(object v)
        {
            if (Cards[0].Value<((PoketHand)v).Cards[0].Value)
            {
                return -1;
            }
            else
                if (Cards[0].Value>((PoketHand)v).Cards[0].Value)
                {
                    return 1;
                }
                else
                
                    if (Cards[1].Value<((PoketHand)v).Cards[1].Value)
                    {
                        return -1;
                    }
                    else
                        if (Cards[1].Value > ((PoketHand)v).Cards[1].Value)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                
            

            //return ( (int)(Cards[0].Value) - (int)(((PoketHand)v).Cards[0].Value) );
        }


    }
}
