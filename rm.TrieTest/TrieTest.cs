using System.Collections.Generic;
using NUnit.Framework;

namespace rm.Trie.Test
{
    /// <summary>
    /// Trie test cases.
    /// </summary>
    [TestFixture]
    [Category("Unit")]
    public class TrieTest
    {
        private ITrie trie;

        [SetUp]
        public void SetUp()
        {
            trie = BuildSampleTrie();
        }
        [TearDown]
        public void TearDown()
        {
            trie = null;
        }
        
        #region Tests

        [Test]
        public void Test01()
        {
            var words = trie.GetWords();
            Assert.AreEqual(10, words.Count);
        }
        [Test]
        public void Test02()
        {
            var prefixWordsEmpty = trie.GetWords("");
            Assert.AreEqual(10, prefixWordsEmpty.Count);
        }
        [Test]
        public void Test03()
        {
            var prefixWords1 = trie.GetWords("th");
            Assert.AreEqual(2, prefixWords1.Count);
        }
        [Test]
        public void Test04()
        {
            var prefixWords1Upper = trie.GetWords("TH");
            Assert.AreEqual(1, prefixWords1Upper.Count);
        }
        [Test]
        public void Test05()
        {
            var prefixWords2 = trie.GetWords("z");
            Assert.AreEqual(0, prefixWords2.Count);
        }
        [Test]
        public void Test06()
        {
            var prefixWords2Upper = trie.GetWords("Z");
            Assert.AreEqual(0, prefixWords2Upper.Count);
        }
        [Test]
        public void Test07()
        {
            var prefixWords3Digits = trie.GetWords("1");
            Assert.AreEqual(2, prefixWords3Digits.Count);
        }
        [Test]
        public void Test08()
        {
            bool hasWord1 = trie.HasWord("test");
            Assert.IsTrue(hasWord1);
        }
        [Test]
        public void Test09()
        {
            bool hasWord1Upper = trie.HasWord("TEST");
            Assert.IsFalse(hasWord1Upper);
        }
        [Test]
        public void Test10()
        {
            bool hasWord2 = trie.HasWord("zz");
            Assert.IsFalse(hasWord2);
        }
        [Test]
        public void Test11()
        {
            bool hasWord2Upper = trie.HasWord("ZZ");
            Assert.IsFalse(hasWord2Upper);
        } 
        
        #endregion

        #region Different types of Tries

        private ITrie BuildSampleTrie()
        {
            return MixedTrie();
            return AsciiWords();
            return Digits();
            return Words();
            return UpperCaseWords();
            return LowerCaseWords();
        }

        private ITrie MixedTrie()
        {
            ITrie trie = TrieFactory.GetTrie();
            string[] strings = 
            { 
                "123", "1", "23", "1",
                "this", "test", "the", "TEMP", "TOKEN", "TAKE", "THUMP"
            };

            foreach (string s in strings)
            {
                trie.AddWord(s);
            }
            return trie;
        }

        private ITrie AsciiWords()
        {
            ITrie trie = TrieFactory.GetTrie();
            string[] strings = 
            { 
                "123", "1", "23", "1",
                "this", "test", "the", "TEMP", "TOKEN", "TAKE", "THUMP"
            };

            foreach (string s in strings)
            {
                trie.AddWord(s);
            }
            return trie;
        }

        private ITrie Digits()
        {
            ITrie trie = TrieFactory.GetTrie();
            string[] strings = 
            { 
                "123", "1", "23"
            };

            foreach (string s in strings)
            {
                trie.AddWord(s);
            }
            return trie;
        }

        private ITrie Words()
        {
            ITrie trie = TrieFactory.GetTrie();
            string[] strings = 
            { 
                "this", "test", "the", "TEMP", "TOKEN", "TAKE", "THUMP"
            };

            foreach (string s in strings)
            {
                trie.AddWord(s);
            }
            return trie;
        }

        private ITrie UpperCaseWords()
        {
            ITrie trie = TrieFactory.GetTrie();
            string[] strings = 
            { 
                "THIS", "TEST", "THE", "TEMP", "TOKEN", "TAKE", "THUMP"
            };

            foreach (string s in strings)
            {
                trie.AddWord(s);
            }
            return trie;
        }

        private ITrie LowerCaseWords()
        {
            ITrie trie = TrieFactory.GetTrie();
            string[] strings = 
            { 
                "this", "test", "the", "temp", "token", "take", "thump"
            };

            foreach (string s in strings)
            {
                trie.AddWord(s);
            }
            return trie;
        }

        #endregion
    }
}
