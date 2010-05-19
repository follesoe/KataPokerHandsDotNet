using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokerHandsTests
{
    [TestClass]
    public class PokerHandTests
    {  
        [TestMethod]
        public void Hand_has_high_card_KD()
        {
            assertThatHandHasHighCard("2H 3D 5S 9C KD", "KD");
            assertThatHandHasHighCard("2H 3D QD 9C 5C", "QD");
            assertThatHandHasHighCard("2H 3D JH 9C 4H", "JH");
            assertThatHandHasHighCard("3H 4S 2S 5C TD", "TD");
        }

        private void assertThatHandHasHighCard(string hand, string card)
        {
            AssertScore(hand, string.Format("High Card ({0})", card));
        }
        
        [TestMethod]
        public void Hand_has_one_Pair()
        {
            AssertScore("2H 3D JH 9C 2D", "Pair (2)");
        }

        [TestMethod]
        public void Hand_has_one_pair_not_including_lowest_card()
        {
            AssertScore("4H 9D JH 9C 2D", "Pair (9)");
        }

        [TestMethod]
        public void Hand_has_one_pair_including_highest_card()
        {
            AssertScore("4H AD JH AC 2D", "Pair (A)");
        }

        [TestMethod]
        public void Hand_has_two_pairs()
        {
            AssertScore("2H 3D JH 3C 2D", "Two pair (2, 3)");
        }

        [TestMethod]
        public void Hand_has_two_pairs_not_including_lowest_card()
        {
            AssertScore("3H 4D 4S 6C 6S", "Two pair (4, 6)");
        }

        [TestMethod]
        public void Hand_has_three_of_a_kind()
        {
            AssertScore("2H 3D JH 2S 2D", "Three of a kind (2)");
        }

        [TestMethod]
        public void Hand_has_three_of_a_kind_in_odd_order()
        {
            AssertScore("6H 9D 8H 9S 9D", "Three of a kind (9)");
        }

        [TestMethod]
        public void Hand_has_four_of_a_kind()
        {
            AssertScore("2H 3D 2C 2S 2D", "Four of a kind (2)");
        }

        [TestMethod]
        public void Hand_has_four_of_a_kind_of_highest_rank()
        {
            AssertScore("3H 3S 3D 2H 3C", "Four of a kind (3)");
        }

        [TestMethod]
        public void Hand_has_straight()
        {
            AssertScore("3H 5C 4S 6C 7S", "Straight (7) high");
        }

        [TestMethod]
        public  void Hand_has_straight_with_low_ace()
        {
            AssertScore("AH 2C 3H 4S 5C", "Straight (5) high");
        }

        [TestMethod]
        public void Hand_has_flush()
        {
            AssertScore("3H 5H 3H 4H 9H", "Flush (H)");
        }

        [TestMethod]
        public void Hand_has_full_house()
        {
            AssertScore("9H 2D 2C 9C 9S", "Full house (9 over 2)");
        }

        [TestMethod]
        public void Hand_has_straight_flush()
        {
            AssertScore("3H 5H 4H 6H 7H", "Straight flush (7) high");
        }


        private void AssertScore(string hand, string expectedScore)
        {
            var hand1 = new Hand(hand);
            string score = hand1.GetScore();

            Assert.AreEqual(expectedScore, score);
        }
    }
}
