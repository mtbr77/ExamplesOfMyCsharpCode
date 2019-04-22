using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holdem
{
    enum Suits : byte { s, c, d, h };

    enum Denomination : byte { _2, _3, _4, _5, _6, _7, _8, _9, _10, J, Q, K, A };

    struct Card : IComparable
    {
        public static string spade = ((char)6).ToString();
        public static string clubs = ((char)5).ToString();
        public static string diamonds = ((char)4).ToString();
        public static string hurts = ((char)3).ToString();
        public static string StringOfSuits = spade + clubs + diamonds + hurts;
        public static string[] ArrayOfValue = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        public Denomination Value 
        { 
            get; set; 
        }

        public Suits Suit
        {
            get; set;
        }

        public Card(Denomination v, Suits s)
            : this()
        {
            Value = v;
            Suit = s;
        }

        public Card(byte v, byte s)
            : this()
        {
            Value = (Denomination)v;
            Suit = (Suits)s;
        }

        public Card(Card c)
        {
            this = c;
        }

        public void Change(ref Card c)
        {
            Card temp = c;
            c = this;
            this = temp;
        }

        public void Print()
        {
            Console.Write(ArrayOfValue[(int)Value].PadLeft(3) + StringOfSuits[(int)Suit]);
        }

        public void Print(String s)
        {
            Console.Write(s + ArrayOfValue[(int)Value] + StringOfSuits[(int)Suit]);
        }
        
        public int CompareTo(object v)
        {
            return -((int)Value - (int)((Card)v).Value);
        }

        public bool equals(Object o)
        {
            if (o == null)
            {
                return false;
            }

            Card co = (Card)o;

            return co.Value == Value && co.Suit == Suit;
        }
    }
}
