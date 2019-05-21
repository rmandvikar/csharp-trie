using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace rm.Trie
{
	/// <summary>
	/// TrieNodeBase is an internal abstract class to encapsulate recursive, helper etc. methods.
	/// </summary>
	[DebuggerDisplay("Character = {Character}")]
	public abstract class TrieNodeBase : IEquatable<TrieNodeBase>
	{
		#region data members

		/// <summary>
		/// The character for the TrieNode.
		/// </summary>
		public char Character { get; private set; }

		/// <summary>
		/// Children Character->TrieNode map.
		/// </summary>
		private readonly IDictionary<char, TrieNodeBase> children;

		#endregion

		#region ctors

		/// <summary>
		/// Creates a new TrieNode instance.
		/// </summary>
		/// <param name="character">The character for the TrieNode.</param>
		internal TrieNodeBase(char character)
		{
			Character = character;
			children = new Dictionary<char, TrieNodeBase>();
		}

		#endregion

		#region methods

		internal IEnumerable<TrieNodeBase> GetChildrenInner()
		{
			return children.Values;
		}

		internal TrieNodeBase GetChildInner(char character)
		{
			TrieNodeBase trieNode;
			children.TryGetValue(character, out trieNode);
			return trieNode;
		}

		internal TrieNodeBase GetTrieNodeInner(string prefix)
		{
			TrieNodeBase trieNode = this;
			foreach (var prefixChar in prefix)
			{
				trieNode = trieNode.GetChildInner(prefixChar);
				if (trieNode == null)
				{
					break;
				}
			}
			return trieNode;
		}

		internal void SetChild(TrieNodeBase child)
		{
			if (child == null)
			{
				throw new ArgumentNullException(nameof(child));
			}
			children[child.Character] = child;
		}

		internal void RemoveChild(char character)
		{
			children.Remove(character);
		}

		internal virtual void Clear()
		{
			children.Clear();
		}

		#region IEquatable<TrieNodeBase>

		public bool Equals(TrieNodeBase other)
		{
			return Character == other.Character;
		}

		#endregion

		#endregion
	}
}
