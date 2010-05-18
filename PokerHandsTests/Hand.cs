using System;
using System.Linq;

namespace PokerHandsTests
{
    public class Card
    {
        public char Value { get; private set; }
        public char Suit { get; private set; }

        public Card(string card)
        {
            Value = card[0];
            Suit = card[1];
        }

        public override string ToString()
        {
            return Value + Suit.ToString();
        }

        public int NumericValue()
        {
            if (Value == 'J') return 11;
            if (Value == 'Q') return 12;
            if (Value == 'K') return 13;
            if (Value == 'A') return 14;
            return Convert.ToInt32(Value.ToString());
        }

        public bool IsSubsequentTo(Card card)
        {
            return NumericValue() - card.NumericValue() == 1;
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

        private readonly Card[] _cards;

        public Hand(string cards)
        {
            _cards = (from card in cards.Split(' ')
                      select new Card(card))
                      .OrderBy(c => c.NumericValue())
                      .ToArray();
        }


        internal string GetScore()
        {
            if (HasFlush()) return string.Format(FlushFormat, _cards[4].Suit);
            if (HasStraight()) return string.Format(StraightFormat, _cards[4].Value);
            if (HasFourOfAKind()) return string.Format(FourOfAKindFormat, _cards[0].Value);
            if (HasThreeOfAKind()) return string.Format(ThreeOfAKindFormat, _cards[0].Value);
            if (HasTwoPairs()) return string.Format(TwoPairFormat, _cards[0].Value, _cards[2].Value);
            if (HasOnePair()) return string.Format(OnePairFormat, _cards[0].Value);
            
            return string.Format(HighCardFormat, _cards[4]);
        }

        private bool HasFlush()
        {
            for (int cardIndex = 1; cardIndex < 5; cardIndex++)
            {
                if (_cards[cardIndex].Suit != _cards[cardIndex - 1].Suit)
                {
                    return false;
                }
            }
            return true;
        }

        private bool HasStraight()
        {
            for (int cardIndex = 1; cardIndex < 5; cardIndex++)
            {
                if (!_cards[cardIndex].IsSubsequentTo(_cards[cardIndex - 1]))
                {
                    return false;
                }
            }
            return true;
        }

        private bool HasOnePair()
        {
            return _cards[0].Value == _cards[1].Value;
        }

        private bool HasTwoPairs()
        {
            return _cards[0].Value == _cards[1].Value &&
                   _cards[2].Value == _cards[3].Value;
        }

        private bool HasThreeOfAKind()
        {
            return _cards[0].Value == _cards[1].Value &&
                   _cards[1].Value == _cards[2].Value;
        }

        private bool HasFourOfAKind()
        {
            return _cards[0].Value == _cards[1].Value &&
                   _cards[1].Value == _cards[2].Value &&
                   _cards[2].Value == _cards[3].Value;
        }
    }
}
