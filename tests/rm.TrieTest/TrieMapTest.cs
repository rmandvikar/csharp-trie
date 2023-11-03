using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace rm.Trie.Test;

/// <summary>
/// TrieMapTest test cases.
/// </summary>
[TestFixture]
[Category("Unit")]
public class TrieMapTest
{
	[DebuggerDisplay("Data = {Data}")]
	class ValueClass
	{
		public int Data { get; set; }
	}

	#region Tests

	[Test]
	public void ValueBy01()
	{
		var trie = BuildSampleTrie();
		Assert.AreEqual(123, trie.ValueBy("key123"));
		Assert.AreEqual(23, trie.ValueBy("key23"));
		Assert.AreEqual(0, trie.ValueBy("key"));
	}

	[Test]
	public void ValueBy02()
	{
		var trieO = BuildSampleTrieO();
		Assert.AreEqual(123, trieO.ValueBy("key123").Data);
		Assert.AreEqual(23, trieO.ValueBy("key23").Data);
		Assert.AreEqual(null, trieO.ValueBy("key"));
	}

	[Test]
	public void Values01()
	{
		var trie = BuildSampleTrie();
		Assert.AreEqual(3, trie.Values().Count());
		var values = new HashSet<int> { 123, 23, 1 };
		Assert.IsTrue(values.OrderBy(x => x).SequenceEqual(trie.Values().OrderBy(x => x)));
	}

	[Test]
	public void Values02()
	{
		var trieO = BuildSampleTrieO();
		Assert.AreEqual(4, trieO.Values().Count());
		var values = new HashSet<int> { 123, 23, 1, 10 };
		Assert.IsTrue
		(
			values.OrderBy(x => x)
				.SequenceEqual(trieO.Values().Select(x => x.Data).OrderBy(x => x))
		);
	}

	[Test]
	public void ValuesBy01()
	{
		var trie = BuildSampleTrie();
		Assert.AreEqual(2, trie.ValuesBy("key1").Count());
		Assert.AreEqual(1, trie.ValuesBy("key123").Count());
	}

	[Test]
	public void ValuesBy02()
	{
		var trieO = BuildSampleTrieO();
		Assert.AreEqual(3, trieO.ValuesBy("key1").Count());
		Assert.AreEqual(1, trieO.ValuesBy("key123").Count());
	}

	[Test]
	public void Keys01()
	{
		var trie = BuildSampleTrie();
		Assert.AreEqual(3, trie.Keys().Count());
		var keys = new HashSet<string> { "key123", "key23", "key1" };
		foreach (var key in trie.Keys())
		{
			Assert.IsTrue(keys.Contains(key));
		}
	}

	[Test]
	public void Keys02()
	{
		var trie = BuildSampleTrie();
		var trieO = BuildSampleTrieO();
		Assert.AreEqual(4, trieO.Keys().Count());
		var keys = new HashSet<string> { "key123", "key23", "key1", "key10" };
		foreach (var key in trie.Keys())
		{
			Assert.IsTrue(keys.Contains(key));
		}
	}

	[Test]
	public void KeysBy01()
	{
		var trie = BuildSampleTrie();
		Assert.AreEqual(2, trie.KeysBy("key1").Count());
		Assert.AreEqual(1, trie.KeysBy("key123").Count());
	}

	[Test]
	public void KeysBy02()
	{
		var trieO = BuildSampleTrieO();
		Assert.AreEqual(3, trieO.KeysBy("key1").Count());
		Assert.AreEqual(1, trieO.KeysBy("key123").Count());
	}

	[Test]
	public void KeyValuePairs01()
	{
		var trie = BuildSampleTrie();
		Assert.AreEqual(3, trie.KeyValuePairs().Count());
		var keys = new HashSet<string> { "key123", "key23", "key1", "key10" };
		foreach (var kv in trie.KeyValuePairs())
		{
			Assert.IsTrue(keys.Contains(kv.Key)
				&& kv.Value == int.Parse(kv.Key.Replace("key", "")));
		}
	}

	[Test]
	public void KeyValuePairs02()
	{
		var trieO = BuildSampleTrieO();
		Assert.AreEqual(4, trieO.KeyValuePairs().Count());
		var keys = new HashSet<string> { "key123", "key23", "key1", "key10" };
		foreach (var kv in trieO.KeyValuePairs())
		{
			Assert.IsTrue(keys.Contains(kv.Key)
				&& kv.Value.Data == int.Parse(kv.Key.Replace("key", "")));
		}
	}

	[Test]
	public void KeyValuePairsBy01()
	{
		var trie = BuildSampleTrie();
		Assert.AreEqual(2, trie.KeyValuePairsBy("key1").Count());
		var keys = new HashSet<string> { "key1", "key123" };
		foreach (var kv in trie.KeyValuePairsBy("key1"))
		{
			Assert.IsTrue(keys.Contains(kv.Key)
				&& kv.Value == int.Parse(kv.Key.Replace("key", "")));
		}
	}

	[Test]
	public void KeyValuePairsBy02()
	{
		var trieO = BuildSampleTrieO();
		Assert.AreEqual(3, trieO.KeyValuePairsBy("key1").Count());
		var keys = new HashSet<string> { "key1", "key10", "key123" };
		foreach (var kv in trieO.KeyValuePairsBy("key1"))
		{
			Assert.IsTrue(keys.Contains(kv.Key)
				&& kv.Value.Data == int.Parse(kv.Key.Replace("key", "")));
		}
	}

	[Test]
	public void HasKey01()
	{
		var trie = BuildSampleTrie();
		Assert.IsTrue(trie.HasKey("key123"));
		Assert.IsFalse(trie.HasKey("key"));
	}

	[Test]
	public void HasKey02()
	{
		var trieO = BuildSampleTrieO();
		Assert.IsTrue(trieO.HasKey("key123"));
		Assert.IsFalse(trieO.HasKey("key"));
	}

	[Test]
	public void HasKeyPrefix01()
	{
		var trie = BuildSampleTrie();
		Assert.IsTrue(trie.HasKeyPrefix("key123"));
		Assert.IsTrue(trie.HasKeyPrefix("key"));
		Assert.IsFalse(trie.HasKeyPrefix("1"));
	}

	[Test]
	public void HasKeyPrefix02()
	{
		var trieO = BuildSampleTrieO();
		Assert.IsTrue(trieO.HasKeyPrefix("key123"));
		Assert.IsTrue(trieO.HasKeyPrefix("key"));
		Assert.IsFalse(trieO.HasKeyPrefix("1"));
	}

	[Test]
	public void Remove01()
	{
		var trie = BuildSampleTrie();
		trie.Remove("key123");
		Assert.IsFalse(trie.HasKey("key123"));
		Assert.Throws<ArgumentOutOfRangeException>(() => trie.Remove("key"));
		Assert.Throws<ArgumentOutOfRangeException>(() => trie.Remove("x"));
	}

	[Test]
	public void Remove02()
	{
		var trie = BuildSampleTrie();
		var trieO = BuildSampleTrieO();
		trieO.Remove("key123");
		Assert.IsFalse(trieO.HasKey("key123"));
		Assert.Throws<ArgumentOutOfRangeException>(() => trie.Remove("key"));
		Assert.Throws<ArgumentOutOfRangeException>(() => trie.Remove("x"));
	}

	[Test]
	public void RemoveKeyPrefix01()
	{
		var trie = BuildSampleTrie();
		Assert.IsTrue(trie.RemoveKeyPrefix("key123"));
		Assert.IsFalse(trie.HasKey("key123"));
		Assert.IsFalse(trie.RemoveKeyPrefix("x"));
		Assert.IsTrue(trie.RemoveKeyPrefix("key"));
		Assert.AreEqual(0, trie.Values().Count());
	}

	[Test]
	public void RemoveKeyPrefix02()
	{
		var trie = BuildSampleTrie();
		var trieO = BuildSampleTrieO();
		Assert.IsTrue(trieO.RemoveKeyPrefix("key123"));
		Assert.IsFalse(trieO.HasKey("key123"));
		Assert.IsFalse(trie.RemoveKeyPrefix("x"));
		Assert.IsTrue(trieO.RemoveKeyPrefix("key"));
		Assert.AreEqual(0, trieO.Values().Count());
	}

	[Test]
	public void GetLongestKeyValuePairs01()
	{
		var trie = BuildSampleTrie();
		trie.Add("the longest word", 1337);
		var expected = new[] { new KeyValuePair<string, int>("the longest word", 1337) };
		var longestKeyValuePairs = trie.GetLongestKeyValuePairs();
		Assert.AreEqual(expected, longestKeyValuePairs);
	}

	[Test]
	public void GetLongestKeyValuePairs02()
	{
		var trie = BuildSampleTrie();
		trie.Add("the longest word1", 1337);
		trie.Add("the longest word2", 1337);
		var expected = new[] { new KeyValuePair<string, int>("the longest word1", 1337), new KeyValuePair<string, int>("the longest word2", 1337) };
		var longestKeyValuePairs = trie.GetLongestKeyValuePairs();
		Assert.AreEqual(expected, longestKeyValuePairs);
	}

	[Test]
	public void GetShortestKeyValuePairs01()
	{
		var trie = BuildSampleTrie();
		var expected = new[] { new KeyValuePair<string, int>("key1", 1) };
		var shortestKeyValuePairs = trie.GetShortestKeyValuePairs();
		Assert.AreEqual(expected, shortestKeyValuePairs);
	}

	[Test]
	public void GetShortestKeyValuePairs02()
	{
		var trie = BuildSampleTrie();
		trie.Add("keya", 1337);
		var expected = new[] { new KeyValuePair<string, int>("key1", 1), new KeyValuePair<string, int>("keya", 1337) };
		var shortestKeyValuePairs = trie.GetShortestKeyValuePairs();
		Assert.AreEqual(expected, shortestKeyValuePairs);
	}

	[Test]
	public void GetLongestPrefixMatch01()
	{
		var trie = BuildSampleTrie();
		var trieO = BuildSampleTrieO();
		var value = trie.GetLongestPrefixMatch("key1234-bomb!");
		var valueO = trieO.GetLongestPrefixMatch("key1234-bomb!");
		Assert.AreEqual("key123", value.Value.Key);
		Assert.AreEqual("key123", valueO.Value.Key);
	}

	[Test]
	public void GetLongestPrefixMatch02()
	{
		var trie = BuildSampleTrie();
		var trieO = BuildSampleTrieO();
		var value = trie.GetLongestPrefixMatch("bomb!");
		var valueO = trieO.GetLongestPrefixMatch("bomb!");
		Assert.IsNull(value);
		Assert.IsNull(valueO);
	}

	[Test]
	public void Clear01()
	{
		var trie = BuildSampleTrie();
		trie.Clear();
		Assert.IsFalse(trie.HasKey("key123"));
		Assert.AreEqual(0, trie.Values().Count());
	}

	[Test]
	public void Clear02()
	{
		var trieO = BuildSampleTrieO();
		trieO.Clear();
		Assert.IsFalse(trieO.HasKey("key123"));
		Assert.AreEqual(0, trieO.Values().Count());
	}

	[Test]
	public void GetTrieNode_NotNull01()
	{
		var trie = BuildSampleTrie();
		var key1Node = trie.GetTrieNode("key1");
		Assert.IsNotNull(key1Node);
		Assert.AreEqual('1', key1Node.Character);
		Assert.AreEqual(1, key1Node.Value);
		Assert.IsTrue(key1Node.HasChild('2'));
		var key12Node = key1Node.GetChild('2');
		Assert.AreEqual('2', key12Node.Character);
		// "key12" does not exists
		Assert.AreEqual(0, key12Node.Value);
		var key123Node = key1Node.GetTrieNode("23");
		Assert.AreEqual('3', key123Node.Character);
		Assert.AreEqual(123, key123Node.Value);
	}

	[Test]
	public void GetTrieNode_NotNull02()
	{
		var trieO = BuildSampleTrieO();
		var key1Node = trieO.GetTrieNode("key1");
		Assert.IsNotNull(key1Node);
		Assert.AreEqual('1', key1Node.Character);
		Assert.AreEqual(1, key1Node.Value.Data);
		Assert.IsTrue(key1Node.HasChild('2'));
		var key12Node = key1Node.GetChild('2');
		Assert.AreEqual('2', key12Node.Character);
		// key "key12" does not exists
		Assert.IsNull(key12Node.Value);
		var key123Node = key1Node.GetTrieNode("23");
		Assert.AreEqual('3', key123Node.Character);
		Assert.IsNotNull(key123Node.Value);
		Assert.AreEqual(123, key123Node.Value.Data);
	}

	[Test]
	public void GetTrieNode_Null01()
	{
		var trie = BuildSampleTrie();
		var key1Node = trie.GetTrieNode("key1");
		var key1zNode = key1Node.GetChild('z');
		Assert.IsNull(key1zNode);
	}

	[Test]
	public void GetTrieNode_Null02()
	{
		var trieO = BuildSampleTrieO();
		var key1Node = trieO.GetTrieNode("key1");
		Assert.IsFalse(key1Node.HasChild('z'));
		var key1zNode = key1Node.GetChild('z');
		Assert.IsNull(key1zNode);
	}

	[Test]
	public void GetRoot01()
	{
		var trie = BuildSampleTrie();
		var root = trie.GetRootTrieNode();
		Assert.IsNotNull(root);
		Assert.AreEqual(' ', root.Character);
	}

	[Test]
	public void GetRoot02()
	{
		var trieO = BuildSampleTrieO();
		var root = trieO.GetRootTrieNode();
		Assert.IsNotNull(root);
		Assert.AreEqual(' ', root.Character);
	}

	#endregion

	private ITrieMap<int> BuildSampleTrie()
	{
		ITrieMap<int> trie = new TrieMap<int>();
		int[] items =
		{
			123, 1, 23, 1,
		};
		foreach (int i in items)
		{
			trie.Add($"key{i}", i);
		}
		return trie;
	}

	private ITrieMap<ValueClass> BuildSampleTrieO()
	{
		ITrieMap<ValueClass> trieO = new TrieMap<ValueClass>();
		int[] items =
		{
			123, 1, 23, 1, 10
		};
		foreach (int i in items)
		{
			trieO.Add($"key{i}", new ValueClass { Data = i });
		}
		return trieO;
	}
}
