using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace rm.Trie
{
	/// <summary>
	/// TrieNode is an internal object to encapsulate recursive, helper etc. methods.
	/// </summary>
	[DebuggerDisplay("Character = {Character}")]
	public class TrieNode
	{
		#region data members

		/// <summary>
		/// The character for the TrieNode.
		/// </summary>
		public char Character { get; private set; }

		/// <summary>
		/// Children Character->TrieNode map.
		/// </summary>
		IDictionary<char, TrieNode> Children { get; set; }

		/// <summary>
		/// Boolean to indicate whether the root to this node forms a word.
		/// </summary>
		public bool IsWord
		{
			get { return WordCount > 0; }
		}

		/// <summary>
		/// The count of words for the TrieNode.
		/// </summary>
		public int WordCount { get; internal set; }

		#endregion

		#region constructors

		/// <summary>
		/// Creates a new TrieNode instance.
		/// </summary>
		/// <param name="character">The character for the TrieNode.</param>
		internal TrieNode(char character)
		{
			Character = character;
			Children = new Dictionary<char, TrieNode>();
			WordCount = 0;
		}

		#endregion

		#region methods

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

		internal void SetChild(TrieNode child)
		{
			if (child == null)
			{
				throw new ArgumentNullException(nameof(child));
			}
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

		public TrieNode GetChild(char character)
		{
			TrieNode trieNode;
			Children.TryGetValue(character, out trieNode);
			return trieNode;
		}

		public bool HasChild(char character)
		{
			return GetChild(character) != null;
		}

		public TrieNode GetTrieNode(string prefix)
		{
			TrieNode trieNode = this;
			foreach (var prefixChar in prefix)
			{
				trieNode = trieNode.GetChild(prefixChar);
				if (trieNode == null)
				{
					break;
				}
			}
			return trieNode;
		}

		public IEnumerable<TrieNode> GetChildren()
		{
			return Children.Values;
		}

		#endregion
	}
}
