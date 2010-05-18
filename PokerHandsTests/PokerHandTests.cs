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
        }

        private void assertThatHandHasHighCard(string hand, string card)
        {
            var hand1 = new Hand(hand);
            string score = hand1.GetScore();

            Assert.AreEqual(string.Format("High Card ({0})", card), score);
        }
        
        [TestMethod]
        public void Hand_has_one_Pair()
        {
            var hand1 = new Hand("2H 3D JH 9C 2D");
            string score = hand1.GetScore();

            Assert.AreEqual("Pair (2)", score);
        }

        [TestMethod]
        public void Hand_has_two_pairs()
        {
            var hand1 = new Hand("2H 3D JH 3C 2D");
            string score = hand1.GetScore();

            Assert.AreEqual("Two pair (2, 3)", score);
        }

        [TestMethod]
        public void Hand_has_three_of_a_kind()
        {
            var hand1 = new Hand("2H 3D JH 2S 2D");
            string score = hand1.GetScore();

            Assert.AreEqual("Three of a kind (2)", score);
        }

        [TestMethod]
        public void Hand_has_four_of_a_kind()
        {
            var hand1 = new Hand("2H 3D 2C 2S 2D");
            string score = hand1.GetScore();

            Assert.AreEqual("Four of a kind (2)", score);
        }

        [TestMethod]
        public void Hand_has_straight()
        {
            var hand1 = new Hand("3H 5C 4S 6C 7S");
            string score = hand1.GetScore();

            Assert.AreEqual("Straight (7) high", score);
        }

        [TestMethod]
        public void Hand_has_flush()
        {
            var hand1 = new Hand("3H 5H 3H 4H 9H");
            string score = hand1.GetScore();

            Assert.AreEqual("Flush (H)", score);
        }
    }
}
