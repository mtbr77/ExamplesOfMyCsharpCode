using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holdem
{
    enum Combination : byte { HighCard, Pair, TwoPair, Set, Straight, Flush, Full, Poker, SFlush}

    struct Hand
    {
        public static string[] ArrayOfCombi = { "HighCard", "Pair", "TwoPair", "Set", "Straight", "Flush", "FullHouse", "Poker", "SFlush"};

        public List<Card> Cards
        {
            get; set;
        }

        public Combination Combi
        {
            get; set;
        }

        public static bool operator ==(Hand h1, Hand h2)
        {
            bool b = h1.Combi == h2.Combi;
            if (b)
            {
                for (byte i = 0; i < 5; ++i)
                {
                    if (h1.Cards[i].Value != h2.Cards[i].Value)
                    {
                        b = false;
                        break;
                    }
                }
            }
            
            return b;
        }

        public static bool operator !=(Hand h1, Hand h2)
        {
            return !(h1 == h2);
        }

        public static bool operator <(Hand h1, Hand h2)
        {
            return !(h1 > h2) && h1 != h2;
        }

        public static bool operator >(Hand h1, Hand h2)
        {
            bool b = true;

            if (h1.Combi < h2.Combi)
            {
                b = false;
            }
            else
                if (h1.Combi == h2.Combi)
                {
                    int begin = 0;
                    switch (h1.Combi)
                    {
                        case Combination.Pair:
                        case Combination.TwoPair:
                            begin = 1;
                            break;
                        case Combination.Set:
                        case Combination.Full:
                            begin = 2;
                            break;
                        case Combination.Poker:
                            begin = 3;
                            break;
                    }
                    
                    for (int i = begin; i < 5; ++i)
                    {
                        if (h1.Cards[i].Value < h2.Cards[i].Value)
                        {
                            b = false;
                            break;
                        }
                    }
                    
                }

            return b;
        }


        private bool IsFlushProperty()
        {
            bool b = true;
            for (byte i = 0; i < 4; ++i)
            {
                if (Cards[i].Suit != Cards[i + 1].Suit)
                {
                    b = false;
                    break;
                }
            }
            return b;
        }

        private bool IsFlushProperty(List<Card> cards)
        {
            int n = cards.Count;
            Dictionary<Suits, int> suitsCount = new Dictionary<Suits, int>();
            foreach (Suits s in Enum.GetValues(typeof(Suits))) suitsCount.Add(s,0);
            
            for (byte i = 0; i < n; ++i)
            {
                suitsCount[cards[i].Suit]++;
            }

            foreach (var i in suitsCount) 
            if (i.Value >= 5)
            {
                int c = 0;
                foreach (Card card in cards)
                    if (card.Suit == i.Key && c <= 5)
                        Cards.Add(card);
                return true;
            }

            return false;
        }

        private bool IsStreetProperty()
        {
            bool b = true;
            for (byte i = 0; i < 4; ++i)
            {
                if ((int)Cards[i].Value - (int)Cards[i + 1].Value != 1)
                {
                    b = false;
                    break;
                }
            }

            b = b || Cards[0].Value == Denomination.A &&  
                Cards[1].Value == Denomination._5 &&
                Cards[2].Value == Denomination._4 &&
                Cards[3].Value == Denomination._3 &&
                Cards[4].Value == Denomination._2;
            return b;
        }

        private bool IsStreetProperty(List<Card> cards)
        {
            bool b = false;
            int n = cards.Count;
            int c = 1;
            int iLast = 0;
            for (byte i = 0; i < n - 1; ++i)
            {
                byte diff = cards[i].Value - cards[i + 1].Value;
                if (diff == 0) continue;

                if (diff == 1)
                {
                    c++;
                    iLast = i + 1;
                }
                else c = 1;

                if (c == 5)
                {
                    b = true;
                    int k = 0;
                    Cards[4] = cards[iLast];
                    do
                    {
                        if (cards[iLast - k].Value != cards[iLast - k - 1].Value)
                        {
                            Cards[4 - k - 1] = cards[iLast - k - 1];
                            k++;
                        }
                    } while (k < 5);
                        
                    break;
                }
            }

            if (c == 4 && cards[iLast].Value == Denomination._2 && cards[0].Value == Denomination.A)
            {
                b = true;
                Cards[0] = cards[0];
                Cards[1] = new Card(Denomination._5, Suits.c);
                Cards[2] = new Card(Denomination._4, Suits.h);
                Cards[3] = new Card(Denomination._3, Suits.d);
                Cards[4] = cards[iLast];
            }

            return b;
        }

        private bool IsStraightFlush()
        {
            return IsStreetProperty() && IsFlushProperty();
        }

        private bool IsStraightFlush(List<Card> cards)
        {
            bool b = false;
            int n = cards.Count;
            int c = 1;
            int iLast = 0;
            for (byte i = 0; i < n - 1; ++i)
            {
                byte diff = cards[i].Value - cards[i + 1].Value;
                bool isSameSuit = cards[i].Suit == cards[i + 1].Suit;
                if (diff == 0 || !isSameSuit) continue;

                if (diff == 1 && isSameSuit)
                {
                    c++;
                    iLast = i + 1;
                }
                else c = 1;

                if (c == 5)
                {
                    b = true;
                    int k = 0;
                    Cards[4] = cards[iLast];
                    do
                    {
                        if (cards[iLast - k].Value != cards[iLast - k - 1].Value)
                        {
                            Cards[4 - k - 1] = cards[iLast - k - 1];
                            k++;
                        }
                    } while (k < 5);

                    break;
                }
            }

            if (c == 4 && cards[iLast].Value == Denomination._2 && cards.Contains(new Card(Denomination.A, cards[iLast].Suit)))
            {
                b = true;
                Cards[0] = cards[0];
                Cards[1] = new Card(Denomination._5, cards[iLast].Suit);
                Cards[2] = new Card(Denomination._4, cards[iLast].Suit);
                Cards[3] = new Card(Denomination._3, cards[iLast].Suit);
                Cards[4] = cards[iLast];
            }

            return b;
        }

        private bool IsPoker()
        {
            bool b = true;
            for (byte i = 1; i < 3; ++i)
            {
                if (Cards[i].Value != Cards[i + 1].Value)
                {
                    b = false;
                    break;
                }
            }

            b = b && (Cards[0].Value == Cards[1].Value || Cards[3].Value == Cards[4].Value);
            return b;
        }

        private bool IsPoker(List<Card> cards)
        {
            int n = cards.Count;
            int c = 1;
            int iLast = 0;
            for (byte i = 0; i < n - 1; ++i)
            {
                if (cards[i].Value == cards[i + 1].Value)
                {
                    c++;
                    iLast = i + 1;
                }
                else c = 1;

                if (c == 4) break;
            }

            if (c == 4)
            {
                Cards[0] = cards[0];
                if (iLast == 3)
                {
                    Cards[1] = cards[1];
                    Cards[2] = cards[2];
                    Cards[3] = cards[3];
                    Cards[4] = cards[4];
                }
                else
                {
                    Cards[1] = cards[iLast - 3];
                    Cards[2] = cards[iLast - 2];
                    Cards[3] = cards[iLast - 1];
                    Cards[4] = cards[iLast];
                }
                return true;
            }
            
            return false;
        }

        private bool IsFull()
        {
            bool b = Cards[0].Value == Cards[1].Value && Cards[3].Value == Cards[4].Value;
            b = b && (Cards[1].Value == Cards[2].Value || Cards[2].Value == Cards[3].Value);
            return b;
        }

        private bool IsFull(List<Card> cards)
        {
            int n = cards.Count;
            int c = 1;
            int pairLast = 0;
            int setLast = 0;
            bool isPair = false;
            bool isSet = false;
            for (byte i = 0; i < n - 1; ++i)
            {
                if (cards[i].Value == cards[i + 1].Value)
                {
                    if (i <= n - 2 && cards[i + 1].Value == cards[i + 2].Value)
                    {
                        setLast = i;
                        isSet = true;
                        Cards.Add(cards[i]);
                        Cards.Add(cards[i + 1]);
                        Cards.Add(cards[i + 2]);
                    }
                    else
                    {
                        isPair = true;
                        pairLast = i;
                        Cards.Add(cards[i]);
                        Cards.Add(cards[i + 1]);
                    }
                }
                
                if (isPair && isSet) return true;
            }

            return false;
        }

        private int HelpCount()
        {
            byte c = 0;
            for (int i = 0; i < 4; ++i)
            {
                for (int j = i + 1; j < 5; ++j)
                {
                    if (Cards[i].Value == Cards[j].Value)
                    {
                        c++;
                    }
                }
            }

            return c;
        }

        private int HelpCount(Card[] cards)
        {
            int c = 0;
            int n = cards.Length;
            for (int i = 0; i < n - 1; ++i)
            {
                for (int j = i + 1; j < n; ++j)
                {
                    if (Cards[i].Value == Cards[j].Value)
                    {
                        c++;
                    }
                }
            }

            return c;
        }


        private bool IsSet()
        {
            return HelpCount() == 3;
        }

        private bool IsTwoPair()
        {
            return HelpCount() == 2;
        }

        private bool IsPair()
        {
            return !IsFlushProperty() && !IsStreetProperty() && HelpCount() == 1;
        }

        private bool IsHighCard()
        {
            return !IsFlushProperty() && !IsStreetProperty() && HelpCount() == 0;
        }


        private void DetermineCombination()
        {
            Cards.Sort();
            switch (HelpCount())
            {
                case 0:
                    bool isStreet = IsStreetProperty();
                    bool isFlush = IsFlushProperty();
                    if (isStreet)
                    {
                        if (isFlush)
                        {
                            Combi = Combination.SFlush;
                        }
                        else
                        {
                            Combi = Combination.Straight;
                        }
                    }
                    else
                        if (isFlush)
                        {
                            Combi = Combination.Flush;
                        }
                        else
                        {
                            Combi = Combination.HighCard;
                        }
                    break;
                case 1:
                    Combi = Combination.Pair;
                    SortPair();
                    break;
                case 2:
                    Combi = Combination.TwoPair;
                    SortTwoPair();
                    break;
                case 3:
                    Combi = Combination.Set;
                    SortSet();
                    break;
                case 4:
                    Combi = Combination.Full;
                    SortFull();
                    break;
                case 6:
                    Combi = Combination.Poker;
                    SortPoker();
                    break;
            }
        }

        private void DetermineCombination(List<Card> cards)
        {
            cards.Sort();
            if (IsStraightFlush(cards))
            {
                Combi = Combination.SFlush;
                return;
            }

            if (IsPoker(cards))
            {
                Combi = Combination.SFlush;
                return;
            }

            if (IsFullHouse(cards))
            {
                Combi = Combination.SFlush;
                return;
            }

            if (IsFlush(cards))
            {
                Combi = Combination.SFlush;
                return;
            }

            if (IsStraight(cards))
            {
                Combi = Combination.Straight;
                return;
            }

            if (IsSet(cards))
            {
                Combi = Combination.Set;
                return;
            }

            if (IsTwoPair(cards))
            {
                Combi = Combination.TwoPair;
                return;
            }

            if (IsPair(cards))
            {
                Combi = Combination.Pair;
                return;
            }

            Combi = Combination.HighCard;
           
        }

        private void SortPair()
        {
            if (Cards[0].Value != Cards[1].Value)
            {
                for (byte i = 1; i < 4; ++i)
                {
                    if (Cards[i].Value == Cards[i + 1].Value)
                    {
                        Cards[i].Change(ref Cards[0]);
                        Cards[i + 1].Change(ref Cards[1]);
                        break;
                    }
                }
                Array.Sort(Cards,2,3);
            }
            
        }

        private void SortTwoPair()
        {
            SortPair();
            if (Cards[2].Value != Cards[3].Value)
            {
                Cards[3].Change(ref Cards[2]);
                Cards[4].Change(ref Cards[3]);
            }

        }

        private void SortSet()
        {
            if (Cards[0].Value != Cards[1].Value)
            {
                for (byte i = 1; i < 3; ++i)
                {
                    if (Cards[i].Value == Cards[i + 1].Value)
                    {
                        Cards[i].Change(ref Cards[0]);
                        Cards[i + 1].Change(ref Cards[1]);
                        Cards[i + 2].Change(ref Cards[2]);
                        break;
                    }
                }
                Array.Sort(Cards, 3, 2);
            }

        }

        private void SortFull()
        {
            if (Cards[2].Value != Cards[3].Value)
            {
                Cards[4].Change(ref Cards[0]);
                Cards[3].Change(ref Cards[1]);
            }
        }

        private void SortPoker()
        {
            if (Cards[0].Value != Cards[1].Value)
            {
                Cards[4].Change(ref Cards[0]);
            }
        }



        private void Init()
        {
            if (Cards == null) Cards = new List<Card>(5);
        }

        public Hand(Card c1, Card c2, Card c3, Card c4, Card c5)
            :this()
        {
            Init();
            Cards[0] = c1;
            Cards[1] = c2;
            Cards[2] = c3;
            Cards[3] = c4;
            Cards[4] = c5;
            DetermineCombination();
        }

        public Hand(Hand h)
            :this()
        {
            this = h;
        }

        public Hand(PoketHand h, Flop f)
            :this()
        {
            Init();
            Cards[0] = h.Cards[0];
            Cards[1] = h.Cards[1];
            Cards[2] = f.Cards[0];
            Cards[3] = f.Cards[1];
            Cards[4] = f.Cards[2];
            DetermineCombination();
        }

        public Hand(PoketHand h, Flop f, Card t, Card r)
            : this()
        {
            Init();
            Card[] cards = new Card[7];
            cards[0] = h.Cards[0];
            cards[1] = h.Cards[1];
            cards[2] = f.Cards[0];
            cards[3] = f.Cards[1];
            cards[4] = f.Cards[2];
            cards[5] = t;
            cards[6] = r;
            DetermineCombination();
        }
        
        public void Print()
        {
            for (byte i = 0; i < 5; ++i)
            {
                (Cards[i]).Print();
                Console.Write(" ");
            }
            Console.WriteLine(ArrayOfCombi[(int)Combi]);
            //Console.Write("of {0}", );
            //Console.WriteLine();
        }


    }
}
