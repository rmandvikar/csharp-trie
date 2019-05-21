using System;
using System.Collections.Generic;
using System.Linq;

namespace rm.Trie
{
	/// <summary>
	/// TrieNode node to save WordCount information.
	/// </summary>
	/// <remarks>
	/// TrieNode could inherit from TrieNode{int} and expose a WordCount property
	/// but TrieNode{int}.Value is exposed as public and the design is not
	/// intuitive.
	/// </remarks>
	public class TrieNode : TrieNodeBase
	{
		#region data members

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

		#region ctors

		/// <summary>
		/// Creates a new TrieNode instance.
		/// </summary>
		/// <param name="character">The character for the TrieNode.</param>
		internal TrieNode(char character)
			: base(character)
		{
			WordCount = 0;
		}

		#endregion

		#region TrieNodeBase methods

		internal override void Clear()
		{
			base.Clear();
			WordCount = 0;
		}

		public TrieNode GetChild(char character)
		{
			return base.GetChildInner(character) as TrieNode;
		}

		public bool HasChild(char character)
		{
			return GetChild(character) != null;
		}

		public TrieNode GetTrieNode(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException(nameof(prefix));
			}
			return base.GetTrieNodeInner(prefix) as TrieNode;
		}

		public IEnumerable<TrieNode> GetChildren()
		{
			return base.GetChildrenInner().Cast<TrieNode>();
		}

		#endregion
	}
}
