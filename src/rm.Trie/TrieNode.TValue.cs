using System;
using System.Collections.Generic;
using System.Linq;

namespace rm.Trie
{
	/// <summary>
	/// TrieNode[TValue] node to save TValue item.
	/// </summary>
	/// <typeparam name="TValue">Type of Value at each TrieNode.</typeparam>
	public class TrieNode<TValue> : TrieNodeBase
	{
		#region data members

		/// <summary>
		/// TValue item.
		/// </summary>
		public TValue Value
		{
			get { return _value; }
			internal set
			{
				if (value == null)
				{
					hasValue = false;
					_value = value;
					return;
				}
				_value = value;
				hasValue = true;
			}
		}
		private bool hasValue;
		private TValue _value;

		#endregion

		#region ctors

		/// <summary>
		/// Creates a new TrieNode instance.
		/// </summary>
		internal TrieNode(char character)
			: base(character)
		{ }

		#endregion

		/// <summary>
		/// Returns true if contains value.
		/// </summary>
		public bool HasValue()
		{
			return hasValue;
		}

		#region TrieNodeBase

		internal override void Clear()
		{
			base.Clear();
			Value = default(TValue);
			hasValue = false;
		}

		public TrieNode<TValue> GetChild(char character)
		{
			return base.GetChildInner(character) as TrieNode<TValue>;
		}

		public bool HasChild(char character)
		{
			return GetChild(character) != null;
		}

		public TrieNode<TValue> GetTrieNode(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException(nameof(prefix));
			}
			return base.GetTrieNodeInner(prefix) as TrieNode<TValue>;
		}

		public IEnumerable<TrieNode<TValue>> GetChildren()
		{
			return base.GetChildrenInner().Cast<TrieNode<TValue>>();
		}

		#endregion
	}
}
