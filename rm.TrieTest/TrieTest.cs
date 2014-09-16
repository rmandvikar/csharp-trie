using System;
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
        [Test]
        public void Test12()
        {
            trie.RemoveWord("this");
            Assert.AreEqual(9, trie.GetWords().Count);
            trie.RemoveWord("the");
            Assert.AreEqual(8, trie.GetWords().Count);
            Assert.Throws<ArgumentOutOfRangeException>(() => trie.RemoveWord("te"));
            Assert.AreEqual(8, trie.GetWords().Count);
            trie.RemoveWord("test");
            Assert.AreEqual(7, trie.GetWords().Count);
            Assert.Throws<ArgumentOutOfRangeException>(() => trie.RemoveWord("word not present"));
            Assert.AreEqual(7, trie.GetWords().Count);
            trie.RemoveWord("123");
            foreach (var word in trie.GetWords())
            {
                trie.RemoveWord(word);
            }
            Assert.AreEqual(0, trie.GetWords().Count);
        }
        [Test]
        public void Test13()
        {
            var trie = TrieFactory.CreateTrie();
            trie.AddWord("");
            trie.AddWord("");
            Assert.AreEqual(1, trie.GetWords().Count);
            trie.RemoveWord("");
            Assert.AreEqual(0, trie.GetWords().Count);
            trie.AddWord("");
            Assert.AreEqual(1, trie.GetWords().Count);
            trie.RemoveWord("");
            Assert.AreEqual(0, trie.GetWords().Count);
        }
        [Test]
        public void Test14()
        {
            trie.AddWord("the longest word");
            var expected = new[] { "the longest word" };
            var longestWords = trie.GetLongestWords();
            Assert.AreEqual(expected, longestWords);
        }
        [Test]
        public void Test15()
        {
            trie.AddWord("the longest word 1");
            trie.AddWord("the longest word 2");
            var expected = new[] { "the longest word 1", "the longest word 2" };
            var longestWords = trie.GetLongestWords();
            Assert.AreEqual(expected, longestWords);
        }
        [Test]
        public void Test16()
        {
            trie.AddWord("");
            Assert.AreNotEqual(0, trie.GetWords().Count);
            trie.Clear();
            Assert.AreEqual(0, trie.GetWords().Count);
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
            ITrie trie = TrieFactory.CreateTrie();
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
            ITrie trie = TrieFactory.CreateTrie();
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
            ITrie trie = TrieFactory.CreateTrie();
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
            ITrie trie = TrieFactory.CreateTrie();
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
            ITrie trie = TrieFactory.CreateTrie();
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
            ITrie trie = TrieFactory.CreateTrie();
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
