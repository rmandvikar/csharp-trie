﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace rm.Trie.Test;

/// <summary>
/// Trie test cases.
/// </summary>
[TestFixture]
[Category("Unit")]
public class TrieTest
{
	#region Tests

	[Test]
	public void GetWords01()
	{
		var trie = BuildSampleTrie();
		var words = trie.GetWords();
		Assert.AreEqual(10, words.Count());
	}

	[Test]
	public void GetWords_Prefix01()
	{
		var trie = BuildSampleTrie();
		var prefixWordsEmpty = trie.GetWords("");
		Assert.AreEqual(10, prefixWordsEmpty.Count());
	}

	[Test]
	public void GetWords_Prefix02()
	{
		var trie = BuildSampleTrie();
		var prefixWords1 = trie.GetWords("th");
		Assert.AreEqual(2, prefixWords1.Count());
	}

	[Test]
	public void GetWords_Prefix03()
	{
		var trie = BuildSampleTrie();
		var prefixWords1Upper = trie.GetWords("TH");
		Assert.AreEqual(1, prefixWords1Upper.Count());
	}

	[Test]
	public void GetWords_Prefix04()
	{
		var trie = BuildSampleTrie();
		var prefixWords2 = trie.GetWords("z");
		Assert.AreEqual(0, prefixWords2.Count());
	}

	[Test]
	public void GetWords_Prefix05()
	{
		var trie = BuildSampleTrie();
		var prefixWords2Upper = trie.GetWords("Z");
		Assert.AreEqual(0, prefixWords2Upper.Count());
	}

	[Test]
	public void GetWords_Prefix06()
	{
		var trie = BuildSampleTrie();
		var prefixWords3Digits = trie.GetWords("1");
		Assert.AreEqual(2, prefixWords3Digits.Count());
	}

	[Test]
	public void HasWord01()
	{
		var trie = BuildSampleTrie();
		bool hasWord1 = trie.HasWord("test");
		Assert.IsTrue(hasWord1);
	}

	[Test]
	public void HasWord02()
	{
		var trie = BuildSampleTrie();
		bool hasWord1Upper = trie.HasWord("TEST");
		Assert.IsFalse(hasWord1Upper);
	}

	[Test]
	public void HasWord03()
	{
		var trie = BuildSampleTrie();
		bool hasWord2 = trie.HasWord("zz");
		Assert.IsFalse(hasWord2);
	}

	[Test]
	public void HasWord04()
	{
		var trie = BuildSampleTrie();
		bool hasWord2Upper = trie.HasWord("ZZ");
		Assert.IsFalse(hasWord2Upper);
	}

	[Test]
	public void HasPrefix01()
	{
		var trie = BuildSampleTrie();
		bool hasPrefix1 = trie.HasPrefix("tes");
		Assert.IsTrue(hasPrefix1);
	}

	[Test]
	public void HasPrefix02()
	{
		var trie = BuildSampleTrie();
		bool hasPrefix2 = trie.HasPrefix("test");
		Assert.IsTrue(hasPrefix2);
	}

	[Test]
	public void HasPrefix03()
	{
		var trie = BuildSampleTrie();
		bool hasPrefix1Upper = trie.HasPrefix("TES");
		Assert.IsFalse(hasPrefix1Upper);
	}

	[Test]
	public void HasPrefix04()
	{
		var trie = BuildSampleTrie();
		bool hasPrefix2Upper = trie.HasPrefix("TEST");
		Assert.IsFalse(hasPrefix2Upper);
	}

	[Test]
	public void RemoveWord01()
	{
		var trie = BuildSampleTrie();
		Assert.AreEqual(1, trie.RemoveWord("this"));
		Assert.AreEqual(9, trie.GetWords().Count());
		Assert.AreEqual(1, trie.RemoveWord("the"));
		Assert.AreEqual(8, trie.GetWords().Count());
		Assert.Throws<ArgumentOutOfRangeException>(() => trie.RemoveWord("te"));
		Assert.AreEqual(8, trie.GetWords().Count());
		Assert.AreEqual(1, trie.RemoveWord("test"));
		Assert.AreEqual(7, trie.GetWords().Count());
		Assert.Throws<ArgumentOutOfRangeException>(() => trie.RemoveWord("word not present"));
		Assert.AreEqual(7, trie.GetWords().Count());
		Assert.AreEqual(1, trie.RemoveWord("123"));
		Assert.AreEqual(2, trie.RemoveWord("1"));
		foreach (var word in trie.GetWords().ToArray())
		{
			trie.RemoveWord(word);
		}
		Assert.AreEqual(0, trie.GetWords().Count());
	}

	[Test]
	public void RemovePrefix01()
	{
		var trie = BuildSampleTrie();
		trie.RemovePrefix("1");
		Assert.AreEqual(8, trie.GetWords().Count());
		trie.RemovePrefix("th");
		Assert.AreEqual(6, trie.GetWords().Count());
		trie.RemovePrefix("x");
		Assert.AreEqual(6, trie.GetWords().Count());
		trie.RemovePrefix("");
		Assert.AreEqual(0, trie.GetWords().Count());
	}

	[Test]
	public void AddWord_EmptyString01()
	{
		var trie = new Trie();
		Assert.AreEqual(0, trie.GetWords().Count());
		trie.AddWord("");
		Assert.AreNotEqual(0, trie.GetWords().Count());
	}

	[Test]
	public void AddWord_RemoveWord01()
	{
		var trie = new Trie();
		trie.AddWord("");
		trie.AddWord("");
		Assert.AreEqual(1, trie.GetWords().Count());
		Assert.AreEqual(2, trie.RemoveWord(""));
		Assert.AreEqual(0, trie.GetWords().Count());
		trie.AddWord("");
		Assert.AreEqual(1, trie.GetWords().Count());
		Assert.AreEqual(1, trie.RemoveWord(""));
		Assert.AreEqual(0, trie.GetWords().Count());
	}

	[Test]
	public void GetLongestWords01()
	{
		var trie = BuildSampleTrie();
		trie.AddWord("the longest word");
		var expected = new[] { "the longest word" };
		var longestWords = trie.GetLongestWords();
		Assert.AreEqual(expected, longestWords);
	}

	[Test]
	public void GetLongestWords02()
	{
		var trie = BuildSampleTrie();
		trie.AddWord("the longest word 1");
		trie.AddWord("the longest word 2");
		var expected = new[] { "the longest word 1", "the longest word 2" };
		var longestWords = trie.GetLongestWords();
		Assert.AreEqual(expected, longestWords);
	}

	[Test]
	public void GetShortestWords01()
	{
		var trie = BuildSampleTrie();
		trie.AddWord("a");
		var expected = new[] { "1", "a" };
		var shortestWords = trie.GetShortestWords();
		Assert.AreEqual(expected, shortestWords);
	}

	[Test]
	public void GetShortestWords02()
	{
		var trie = BuildSampleTrie();
		trie.AddWord("");
		var expected = new[] { "" };
		var shortestWords = trie.GetShortestWords();
		Assert.AreEqual(expected, shortestWords);
	}

	[Test]
	[TestCase("test", "tester")]
	[TestCase("123", "1234-bomb!")]
	public void GetLongestPrefixMatch01(string expected, string word)
	{
		var trie = BuildSampleTrie();
		var longestPrefixMatch = trie.GetLongestPrefixMatch(word);
		Assert.AreEqual(expected, longestPrefixMatch);
	}

	[Test]
	[TestCase("", "bomb!")]
	public void GetLongestPrefixMatch02(string expected, string word)
	{
		var trie = BuildSampleTrie();
		trie.AddWord("");
		var longestPrefixMatch = trie.GetLongestPrefixMatch(word);
		Assert.AreEqual(expected, longestPrefixMatch);
	}

	[Test]
	[TestCase("cater", "caterer")]
	[TestCase("base", "basemexy")]
	[TestCase(null, "child")]
	public void GetLongestPrefixMatch03(string expected, string word)
	{
		var trie = BuildSampleTrie2();
		var longestPrefixMatch = trie.GetLongestPrefixMatch(word);
		Assert.AreEqual(expected, longestPrefixMatch);
	}

	[Test]
	public void Clear01()
	{
		var trie = BuildSampleTrie();
		Assert.AreNotEqual(0, trie.GetWords().Count());
		trie.Clear();
		Assert.AreEqual(0, trie.GetWords().Count());
	}

	[Test]
	public void Count01()
	{
		var trie = BuildSampleTrie();
		Assert.AreEqual(11, trie.Count());
	}

	[Test]
	public void UniqueCount01()
	{
		var trie = BuildSampleTrie();
		Assert.AreEqual(10, trie.UniqueCount());
	}

	[Test]
	public void GetTrieNode01()
	{
		var trie = BuildSampleTrie();
		var teNode = trie.GetTrieNode("te");
		Assert.IsNotNull(teNode);
		Assert.AreEqual('e', teNode.Character);
		Assert.IsTrue(teNode.HasChild('s'));
		var tesNode = teNode.GetChild('s');
		Assert.AreEqual('s', tesNode.Character);
		Assert.IsFalse(tesNode.IsWord);
		var testNode = teNode.GetTrieNode("st");
		Assert.AreEqual('t', testNode.Character);
		Assert.IsTrue(testNode.IsWord);
	}

	[Test]
	public void GetTrieNode02()
	{
		var trie = BuildSampleTrie();
		var teNode = trie.GetTrieNode("te");
		Assert.IsFalse(teNode.HasChild('z'));
		var tezNode = teNode.GetChild('z');
		Assert.IsNull(tezNode);
	}

	[Test]
	public void GetRoot01()
	{
		var trie = BuildSampleTrie();
		var root = trie.GetRootTrieNode();
		Assert.IsNotNull(root);
		Assert.AreEqual(' ', root.Character);
	}

	#endregion

	private ITrie BuildSampleTrie()
	{
		ITrie trie = new Trie();
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

	private ITrie BuildSampleTrie2()
	{
		ITrie trie = new Trie();
		string[] strings =
		{
			"are", "area", "base", "cat", "cater", "children", "basement"
		};

		foreach (string s in strings)
		{
			trie.AddWord(s);
		}
		return trie;
	}
}
