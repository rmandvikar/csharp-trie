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

        /// <summary>
        /// Parent TrieNode of this TrieNode.
        /// </summary>
        /// <remarks>Required to easily remove word from Trie.</remarks>
        internal TrieNode Parent { get; private set; }

        #endregion

        #region constructors

        /// <summary>
        /// Create a new TrieNode instance.
        /// </summary>
        /// <param name="character">The character for the TrieNode.</param>
        /// <param name="children">Children of TrieNode.</param>
        /// <param name="isWord">If root TrieNode to this TrieNode is a word.</param>
        /// <param name="wordCount"></param>
        internal TrieNode(char character, IDictionary<char, TrieNode> children,
            int wordCount, TrieNode parent)
        {
            Character = character;
            Children = children;
            WordCount = wordCount;
            Parent = parent;
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

        private void RemoveChild(char character)
        {
            Children.Remove(character);
        }

        internal void RemoveNode(TrieNode rootTrieNode)
        {
            // set this node's word count to 0
            WordCount = 0;
            RemoveNode_Recursive(rootTrieNode);
        }

        /// <summary>
        /// Recursive method to remove word. Remove only if node does not 
        /// have children and is not a word node and has a parent node.
        /// </summary>
        private void RemoveNode_Recursive(TrieNode rootTrieNode)
        {
            if (Children.Count == 0 // should not have any children
                && !IsWord // should not be a word
                && this != rootTrieNode // do not remove root node
                )
            {
                var parent = Parent;
                Parent.RemoveChild(Character);
                Parent = null;
                parent.RemoveNode_Recursive(rootTrieNode);
            }
        }

        internal void Clear()
        {
            WordCount = 0;
            Children.Clear();
        }

        #endregion
    }
}