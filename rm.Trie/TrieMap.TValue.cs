using System;
using System.Collections.Generic;
using System.Text;

namespace rm.Trie
{
	/// <summary>
	/// TrieMap data structure.
	/// </summary>
	/// <typeparam name="TValue">Type of Value at each TrieNode.</typeparam>
	public class TrieMap<TValue> : ITrieMap<TValue>
	{
		#region data members

		/// <summary>
		/// Root TrieNode.
		/// </summary>
		private readonly TrieNode<TValue> rootTrieNode;

		#endregion

		#region ctors

		/// <summary>
		/// Create a new TrieMap instance.
		/// </summary>
		public TrieMap()
		{
			rootTrieNode = new TrieNode<TValue>(' ');
		}

		#endregion

		#region ITrieMap<TValue>

		/// <summary>
		/// Gets TValue item for key from TrieMap.
		/// </summary>
		public TValue ValueBy(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException(nameof(key));
			}
			var trieNode = GetTrieNode(key);
			return trieNode != null ? trieNode.Value : default(TValue);
		}

		/// <summary>
		/// Gets TValue items by key prefix from TrieMap.
		/// </summary>
		public IEnumerable<TValue> ValuesBy(string keyPrefix)
		{
			if (keyPrefix == null)
			{
				throw new ArgumentNullException(nameof(keyPrefix));
			}
			foreach (var value in
				Traverse
				(
					GetTrieNode(keyPrefix), new StringBuilder(keyPrefix),
					(kBuilder, v) => v
				))
			{
				yield return value;
			}
		}

		/// <summary>
		/// Gets all TValue items from TrieMap.
		/// </summary>
		public IEnumerable<TValue> Values()
		{
			return ValuesBy("");
		}

		/// <summary>
		/// Gets all keys from TrieMap.
		/// </summary>
		public IEnumerable<string> Keys()
		{
			foreach (var key in
				Traverse
				(
					rootTrieNode, new StringBuilder(),
					(kBuilder, v) => kBuilder.ToString()
				))
			{
				yield return key;
			}
		}

		/// <summary>
		/// Gets all string->TValue pairs from TrieMap.
		/// </summary>
		public IEnumerable<KeyValuePair<string, TValue>> KeyValuePairs()
		{
			foreach (var kvPair in
				Traverse
				(
					rootTrieNode, new StringBuilder(),
					(kBuilder, v) => new KeyValuePair<string, TValue>(kBuilder.ToString(), v)
				))
			{
				yield return kvPair;
			}
		}

		/// <summary>
		/// Adds TValue item for key to TrieMap.
		/// </summary>
		public void Add(string key, TValue value)
		{
			var trieNode = rootTrieNode;
			foreach (var c in key)
			{
				var child = trieNode.GetChild(c);
				if (child == null)
				{
					child = new TrieNode<TValue>(c);
					trieNode.SetChild(child);
				}
				trieNode = child;
			}
			trieNode.Value = value;
		}

		/// <summary>
		/// Returns true if key present in TrieMap.
		/// </summary>
		public bool HasKey(string key)
		{
			return GetTrieNode(key)?.HasValue() ?? false;
		}

		/// <summary>
		/// Returns true if key prefix present in TrieMap.
		/// </summary>
		public bool HasKeyPrefix(string keyPrefix)
		{
			return GetTrieNode(keyPrefix) != null;
		}

		/// <summary>
		/// Gets the equivalent TrieNode in the TrieMap for given key prefix. 
		/// If prefix not present, then returns null.
		/// </summary>
		public TrieNode<TValue> GetTrieNode(string keyPrefix)
		{
			if (keyPrefix == null)
			{
				throw new ArgumentNullException(nameof(keyPrefix));
			}
			return rootTrieNode.GetTrieNode(keyPrefix);
		}

		/// <summary>
		/// Removes key from TrieMap.
		/// </summary>
		public void Remove(string key)
		{
			var trieNode = GetTrieNode(key);
			if (!trieNode?.HasValue() ?? true)
			{
				throw new ArgumentOutOfRangeException($"{key} does not exist in trieMap.");
			}
			trieNode.Clear();
		}

		/// <summary>
		/// Removes key prefix from TrieMap and return true else false.
		/// </summary>
		public bool RemoveKeyPrefix(string keyPrefix)
		{
			var trieNode = GetTrieNode(keyPrefix);
			trieNode?.Clear();
			return trieNode != null;
		}

		/// <summary>
		/// Clears all values from TrieMap.
		/// </summary>
		public void Clear()
		{
			rootTrieNode.Clear();
		}

		/// <summary>
		/// Gets the root TrieNode of the TrieMap.
		/// </summary>
		public TrieNode<TValue> GetRootTrieNode()
		{
			return rootTrieNode;
		}

		#endregion

		#region private methods

		/// <summary>
		/// DFS traversal starting from given TrieNode and yield.
		/// </summary>
		private IEnumerable<TResult> Traverse<TResult>(TrieNode<TValue> trieNode,
			StringBuilder buffer, Func<StringBuilder, TValue, TResult> transform)
		{
			if (trieNode == null)
			{
				yield break;
			}
			if (trieNode.HasValue())
			{
				yield return transform
				(
					buffer, trieNode.Value
				);
			}
			foreach (var child in trieNode.GetChildren())
			{
				// buffer is not used always but it's ok
				buffer.Append(child.Character);
				foreach (var item in Traverse(child, buffer, transform))
				{
					yield return item;
				}
				buffer.Length--;
			}
		}

		#endregion
	}
}
