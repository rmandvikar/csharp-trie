csharp-trie
===========

A trie (prefix tree) data structure implementation in C#.

### Methods

```c#
// Add a word to the Trie.
void AddWord(string word);

// Remove word from the Trie.
void RemoveWord(string word);

// Remove words by prefix from the Trie.
void RemovePrefix(string prefix);

// Get all words in the Trie.
ICollection<string> GetWords();

// Get words for given prefix.
ICollection<string> GetWords(string prefix);

// Returns true or false if the word is present in the Trie.
bool HasWord(string word);

// Returns the count for the word in the Trie.
int WordCount(string word);

// Get longest words from the Trie.
ICollection<string> GetLongestWords();

// Get shortest words from the Trie.
ICollection<string> GetShortestWords();

// Clear all words from the Trie.
void Clear();

// Get total word count in the Trie.
int Count();

// Get unique word count in the Trie.
int UniqueCount();
```

### Example

```c#
// Create a new trie.
ITrie trie = TrieFactory.CreateTrie();

// Add words.
trie.AddWord("test");
trie.AddWord("this");

// Get all words.
var allWords = trie.GetWords();

// Get words for a prefix.
var tePrefixWords = trie.GetWords("te");

// Check if a word is present.
var hasWord = trie.HasWord("test");

// Get word count for a word.
var wordCount = trie.WordCount("test");

// Remove word.
trie.RemoveWord("test");

// Remove words by prefix.
trie.RemovePrefix("thi");

// Get longest words.
var longestWords = trie.GetLongestWords();

// Get shortest words.
var shortestWords = trie.GetShortestWords();

// Clear all words.
trie.Clear();

// Get counts.
trie.AddWord("test"); // adding "test" again
var count = trie.Count();
var uniqueCount = trie.UniqueCount();
```

#### Note: 

Not thread-safe.