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
		public void GetWords01()
		{
			var words = trie.GetWords();
			Assert.AreEqual(10, words.Count);
		}
		[Test]
		public void GetWords_Prefix01()
		{
			var prefixWordsEmpty = trie.GetWords("");
			Assert.AreEqual(10, prefixWordsEmpty.Count);
		}
		[Test]
		public void GetWords_Prefix02()
		{
			var prefixWords1 = trie.GetWords("th");
			Assert.AreEqual(2, prefixWords1.Count);
		}
		[Test]
		public void GetWords_Prefix03()
		{
			var prefixWords1Upper = trie.GetWords("TH");
			Assert.AreEqual(1, prefixWords1Upper.Count);
		}
		[Test]
		public void GetWords_Prefix04()
		{
			var prefixWords2 = trie.GetWords("z");
			Assert.AreEqual(0, prefixWords2.Count);
		}
		[Test]
		public void GetWords_Prefix05()
		{
			var prefixWords2Upper = trie.GetWords("Z");
			Assert.AreEqual(0, prefixWords2Upper.Count);
		}
		[Test]
		public void GetWords_Prefix06()
		{
			var prefixWords3Digits = trie.GetWords("1");
			Assert.AreEqual(2, prefixWords3Digits.Count);
		}
		[Test]
		public void HasWord01()
		{
			bool hasWord1 = trie.HasWord("test");
			Assert.IsTrue(hasWord1);
		}
		[Test]
		public void HasWord02()
		{
			bool hasWord1Upper = trie.HasWord("TEST");
			Assert.IsFalse(hasWord1Upper);
		}
		[Test]
		public void HasWord03()
		{
			bool hasWord2 = trie.HasWord("zz");
			Assert.IsFalse(hasWord2);
		}
		[Test]
		public void HasWord04()
		{
			bool hasWord2Upper = trie.HasWord("ZZ");
			Assert.IsFalse(hasWord2Upper);
		}
		[Test]
		public void RemoveWord01()
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
		public void RemovePrefix01()
		{
			trie.RemovePrefix("1");
			Assert.AreEqual(8, trie.GetWords().Count);
			trie.RemovePrefix("th");
			Assert.AreEqual(6, trie.GetWords().Count);
			trie.RemovePrefix("x");
			Assert.AreEqual(6, trie.GetWords().Count);
			trie.RemovePrefix("");
			Assert.AreEqual(0, trie.GetWords().Count);
		}
		[Test]
		public void AddWord_EmptyString01()
		{
			trie = TrieFactory.CreateTrie();
			Assert.AreEqual(0, trie.GetWords().Count);
			trie.AddWord("");
			Assert.AreNotEqual(0, trie.GetWords().Count);
		}
		[Test]
		public void AddWord_RemoveWord01()
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
		public void GetLongestWords01()
		{
			trie.AddWord("the longest word");
			var expected = new[] { "the longest word" };
			var longestWords = trie.GetLongestWords();
			Assert.AreEqual(expected, longestWords);
		}
		[Test]
		public void GetLongestWords02()
		{
			trie.AddWord("the longest word 1");
			trie.AddWord("the longest word 2");
			var expected = new[] { "the longest word 1", "the longest word 2" };
			var longestWords = trie.GetLongestWords();
			Assert.AreEqual(expected, longestWords);
		}
		[Test]
		public void GetShortestWords01()
		{
			trie.AddWord("a");
			var expected = new[] { "1", "a" };
			var shortestWords = trie.GetShortestWords();
			Assert.AreEqual(expected, shortestWords);
		}
		[Test]
		public void GetShortestWords02()
		{
			trie.AddWord("");
			var expected = new[] { "" };
			var shortestWords = trie.GetShortestWords();
			Assert.AreEqual(expected, shortestWords);
		}
		[Test]
		public void Clear01()
		{
			Assert.AreNotEqual(0, trie.GetWords().Count);
			trie.Clear();
			Assert.AreEqual(0, trie.GetWords().Count);
		}
		[Test]
		public void Count01()
		{
			Assert.AreEqual(11, trie.Count());
		}
		[Test]
		public void UniqueCount01()
		{
			Assert.AreEqual(10, trie.UniqueCount());
		}

		#endregion

		private ITrie BuildSampleTrie()
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
	}
}
