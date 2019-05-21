using NUnit.Framework;

namespace rm.Trie.Test
{
	/// <summary>
	/// TrieNodeTest test cases.
	/// </summary>
	[TestFixture]
	[Category("Unit")]
	public class TrieNodeTest
	{
		[Test]
		public void HasValue_Value()
		{
			var tnode = new TrieNode<int>(' ');
			Assert.IsFalse(tnode.HasValue());
			tnode.Value = 0;
			Assert.IsTrue(tnode.HasValue());
			tnode.Clear();
			Assert.IsFalse(tnode.HasValue());
			tnode.Value = 1;
			Assert.IsTrue(tnode.HasValue());
			tnode.Value = 0;
			Assert.IsTrue(tnode.HasValue());
		}

		[Test]
		public void HasValue_Class()
		{
			var tnode = new TrieNode<Trie>(' ');
			Assert.IsFalse(tnode.HasValue());
			Assert.IsNull(tnode.Value);
			tnode.Value = new Trie();
			Assert.IsTrue(tnode.HasValue());
			Assert.IsNotNull(tnode.Value);
			tnode.Clear();
			Assert.IsFalse(tnode.HasValue());
			Assert.IsNull(tnode.Value);
			tnode.Value = new Trie();
			Assert.IsTrue(tnode.HasValue());
			tnode.Value = null;
			Assert.IsFalse(tnode.HasValue());
			Assert.IsNull(tnode.Value);
		}
	}
}
