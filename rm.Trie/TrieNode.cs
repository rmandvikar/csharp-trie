using System.Collections.Generic;

namespace rm.Trie
{
	/// <summary>
	/// TrieNode is an internal object to encapsulate recursive, helper etc. methods. 
	/// </summary>
	internal class TrieNode
	{
		#region data members

		/// <summary>
		/// The character for the TrieNode.
		/// </summary>
		internal char Character { get; private set; }

		/// <summary>
		/// Children Character->TrieNode map.
		/// </summary>
		IDictionary<char, TrieNode> Children { get; set; }

		/// <summary>
		/// Boolean to indicate whether the root to this node forms a word.
		/// </summary>
		internal bool IsWord
		{
			get { return WordCount > 0; }
		}

		/// <summary>
		/// The count of words for the TrieNode.
		/// </summary>
		internal int WordCount { get; set; }

		#endregion

		#region constructors

		/// <summary>
		/// Create a new TrieNode instance.
		/// </summary>
		/// <param name="character">The character for the TrieNode.</param>
		/// <param name="children">Children of TrieNode.</param>
		/// <param name="wordCount"></param>
		internal TrieNode(char character, IDictionary<char, TrieNode> children,
			int wordCount)
		{
			Character = character;
			Children = children;
			WordCount = wordCount;
		}

		#endregion

		#region methods

		/// <summary>
		/// For readability.
		/// </summary>
		/// <returns>Character.</returns>
		public override string ToString()
		{
			return Character.ToString();
		}

		public override bool Equals(object obj)
		{
			TrieNode that;
			return
				obj != null
				&& (that = obj as TrieNode) != null
				&& that.Character == this.Character;
		}

		public override int GetHashCode()
		{
			return Character.GetHashCode();
		}

		internal IEnumerable<TrieNode> GetChildren()
		{
			return Children.Values;
		}

		internal TrieNode GetChild(char character)
		{
			TrieNode trieNode;
			Children.TryGetValue(character, out trieNode);
			return trieNode;
		}

		internal void SetChild(TrieNode child)
		{
			Children[child.Character] = child;
		}

		internal void RemoveChild(char character)
		{
			Children.Remove(character);
		}

		internal void Clear()
		{
			WordCount = 0;
			Children.Clear();
		}

		#endregion
	}
}