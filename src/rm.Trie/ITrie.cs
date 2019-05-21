using System.Collections.Generic;

namespace rm.Trie
{
	/// <summary>
	/// Interface for Trie data structure.
	/// </summary>
	/// <remarks>
	/// ITrie does not inherit from ITrieMap{TValue} for flexibility.
	/// </remarks>
	public interface ITrie
	{
		/// <summary>
		/// Adds a word to the Trie.
		/// </summary>
		void AddWord(string word);

		/// <summary>
		/// Removes word from the Trie.
		/// </summary>
		/// <returns>Count of words removed.</returns>
		int RemoveWord(string word);

		/// <summary>
		/// Removes words by prefix from the Trie.
		/// </summary>
		void RemovePrefix(string prefix);

		/// <summary>
		/// Gets all words in the Trie.
		/// </summary>
		IEnumerable<string> GetWords();

		/// <summary>
		/// Gets words for given prefix.
		/// </summary>
		IEnumerable<string> GetWords(string prefix);

		/// <summary>
		/// Returns true if the word is present in the Trie.
		/// </summary>
		bool HasWord(string word);

		/// <summary>
		/// Returns true if the prefix is present in the Trie.
		/// </summary>
		bool HasPrefix(string prefix);

		/// <summary>
		/// Gets the equivalent TrieNode in the Trie for given prefix. 
		/// If prefix not present, then returns null.
		/// </summary>
		TrieNode GetTrieNode(string prefix);

		/// <summary>
		/// Returns the count for the word in the Trie.
		/// </summary>
		int WordCount(string word);

		/// <summary>
		/// Gets longest words from the Trie.
		/// </summary>
		ICollection<string> GetLongestWords();

		/// <summary>
		/// Gets shortest words from the Trie.
		/// </summary>
		ICollection<string> GetShortestWords();

		/// <summary>
		/// Clears all words from the Trie.
		/// </summary>
		void Clear();

		/// <summary>
		/// Gets total word count in the Trie.
		/// </summary>
		int Count();

		/// <summary>
		/// Gets unique word count in the Trie.
		/// </summary>
		int UniqueCount();

		/// <summary>
		/// Gets the root TrieNode of the Trie.
		/// </summary>
		TrieNode GetRootTrieNode();
	}
}
