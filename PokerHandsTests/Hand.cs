using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHandsTests
{
    public class Card
    {
        public char Rank { get; private set; }
        public char Suit { get; private set; }

        public Card(string card)
        {
            Rank = card[0];
            Suit = card[1];
        }

        public override string ToString()
        {
            return Rank + Suit.ToString();
        }

        public int NumericRank()
        {
            if (Rank == 'T') return 10;
            if (Rank == 'J') return 11;
            if (Rank == 'Q') return 12;
            if (Rank == 'K') return 13;
            if (Rank == 'A') return 14;
            return Convert.ToInt32(Rank.ToString());
        }

        public bool IsSubsequentTo(Card card)
        {
            return NumericRank() - card.NumericRank() == 1;
        }
    }

    public class Hand
    {
        private const string FlushFormat = "Flush ({0})";
        private const string StraightFormat = "Straight ({0}) high";
        private const string FourOfAKindFormat = "Four of a kind ({0})";
        private const string ThreeOfAKindFormat = "Three of a kind ({0})";
        private const string TwoPairFormat = "Two pair ({0}, {1})";
        private const string OnePairFormat = "Pair ({0})";
        private const string HighCardFormat = "High Card ({0})";
        private const string FullHouseFormat = "Full house ({0} over {1})";

        private readonly Card[] _cards;

        public Hand(string cards)
        {
            _cards = (from card in cards.Split(' ')
                      select new Card(card))
                      .OrderBy(c => c.NumericRank())
                      .ToArray();
        }


        internal string GetScore()
        {
            return FindStraightFlush() ??
                   FindFourOfAKind() ??
                   FindFullHouse() ?? 
                   FindFlush() ??
                   FindStraight() ?? 
                   FindThreeOfAKind() ?? 
                   FindTwoPairs() ?? 
                   FindOnePair() ?? 
                   FindHighCard();
        }

        private string FindFullHouse()
        {
            char threeOfAKindRank = FindGroupsOfSize(3).FirstOrDefault();
            char pairRank = FindGroupsOfSize(2).FirstOrDefault();

            if (threeOfAKindRank != default(char) && pairRank != default(char))
                return string.Format(FullHouseFormat, threeOfAKindRank, pairRank);

            return null;
        }

        private string FindStraightFlush()
        {
            if (FindFlush() != null)
            {
                // TODO: Fix this ugly hack.
                string result = FindStraight();
                if (result != null)
                    return result.Replace("Straight", "Straight flush");     
            }
               
            return null;
        }

        private string FindHighCard()
        {
            return string.Format(HighCardFormat, _cards[4]);
        }

        private string FindFlush()
        {
            for (int cardIndex = 1; cardIndex < 5; cardIndex++)
            {
                if (_cards[cardIndex].Suit != _cards[cardIndex - 1].Suit)
                {
                    return null;
                }
            }
            return string.Format(FlushFormat, _cards[0].Suit);
        }

        private string FindStraight()
        {
            for (int cardIndex = 1; cardIndex < 4; cardIndex++)
            {
                if (!_cards[cardIndex].IsSubsequentTo(_cards[cardIndex - 1]))
                {
                    return null;
                }
            }

            bool isNormalStraight = _cards[4].IsSubsequentTo(_cards[3]);
            if (isNormalStraight)
            {
                return string.Format(StraightFormat, _cards[4].Rank);
            }

 
            bool isAceLowStraight = _cards[4].Rank == 'A' && _cards[0].Rank == '2';
            if (isAceLowStraight) 
            {
                return string.Format(StraightFormat, _cards[3].Rank);
            }

            return null;
        }

        private string FindOnePair()
        {
            return FindCardsOfEqualRank(2, OnePairFormat);
        }

        private string FindTwoPairs()
        {
            var groupedCards = FindGroupsOfSize(2);

            if (groupedCards.Count() == 2)
            {
                return string.Format(TwoPairFormat, groupedCards.ElementAt(0), groupedCards.ElementAt(1));
            }

            return null;
        }

        private string FindThreeOfAKind()
        {
            return FindCardsOfEqualRank(3, ThreeOfAKindFormat);
        }

        private string FindFourOfAKind()
        {
            return FindCardsOfEqualRank(4, FourOfAKindFormat);
        }

        private string FindCardsOfEqualRank(int groupSize, string scoreFormat)
        {
            var groupedCards = FindGroupsOfSize(groupSize);
            if (groupedCards.Count() == 1)
            {
                return string.Format(scoreFormat, groupedCards.Single());
            }

            return null;
        }

        private IEnumerable<char> FindGroupsOfSize(int size)
        {
            return _cards.GroupBy(c => c.Rank)
                    .Where(c => c.Count() == size).Select(e => e.Key);
        }
    }
}
