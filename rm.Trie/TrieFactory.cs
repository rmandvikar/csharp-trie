using System.Collections.Generic;

namespace rm.Trie
{
	/// <summary>
	/// Trie factory to create Trie instances.
	/// </summary>
	public static class TrieFactory
	{
		/// <summary>
		/// Create a new Trie instance.
		/// </summary>
		public static ITrie CreateTrie()
		{
			return new Trie(
				CreateTrieNode(' ')
				);
		}
		/// <summary>
		/// Create a new TrieNode instance.
		/// </summary>
		/// <param name="character">Character of the TrieNode.</param>
		internal static TrieNode CreateTrieNode(char character)
		{
			return new TrieNode(character,
				new Dictionary<char, TrieNode>(),
				0
				);
		}
	}
}
