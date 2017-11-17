csharp-trie
===========

A trie (prefix tree) data structure implementation in C#.

### Methods

```c#
// Adds a word to the Trie.
void AddWord(string word);

// Removes word from the Trie.
int RemoveWord(string word);

// Removes words by prefix from the Trie.
void RemovePrefix(string prefix);

// Gets all words in the Trie.
ICollection<string> GetWords();

// Gets words for given prefix.
ICollection<string> GetWords(string prefix);

// Returns true if the word is present in the Trie.
bool HasWord(string word);

// Returns true if the prefix is present in the Trie.
bool HasPrefix(string prefix);

// Returns the count for the word in the Trie.
int WordCount(string word);

// Gets longest words from the Trie.
ICollection<string> GetLongestWords();

// Gets shortest words from the Trie.
ICollection<string> GetShortestWords();

// Clears all words from the Trie.
void Clear();

// Gets total word count in the Trie.
int Count();

// Gets unique word count in the Trie.
int UniqueCount();
```

### Example

```c#
// Creates a new trie.
ITrie trie = new Trie();

// Adds words.
trie.AddWord("test");
trie.AddWord("this");

// Gets all words.
var allWords = trie.GetWords();

// Gets words for a prefix.
var tePrefixWords = trie.GetWords("te");

// Checks if a word is present.
var hasWord = trie.HasWord("test");

// Checks if a prefix is present.
var hasPrefix = trie.HasPrefix("tes");

// Gets word count for a word.
var wordCount = trie.WordCount("test");

// Removes word.
int removeCount = trie.RemoveWord("test"); // removeCount = 1

// Removes words by prefix.
trie.RemovePrefix("thi");

// Gets longest words.
var longestWords = trie.GetLongestWords();

// Gets shortest words.
var shortestWords = trie.GetShortestWords();

// Clears all words.
trie.Clear();

// Gets counts.
trie.AddWord("test"); // adding "test" again
var count = trie.Count();
var uniqueCount = trie.UniqueCount();
```

#### Note: 

Not thread-safe.