csharp-trie
===========


A trie (prefix tree) data structure implementation in C#.

master: [![Build Status](https://travis-ci.org/rmandvikar/csharp-trie.svg?branch=master)](https://travis-ci.org/rmandvikar/csharp-trie)
dev:    [![Build Status](https://travis-ci.org/rmandvikar/csharp-trie.svg?branch=dev)](https://travis-ci.org/rmandvikar/csharp-trie)

#### nuget:
```
Install-Package rm.Trie
```

### Trie Methods

```c#
// Adds a word to the Trie.
void AddWord(string word);

// Removes word from the Trie.
int RemoveWord(string word);

// Removes words by prefix from the Trie.
void RemovePrefix(string prefix);

// Gets all words in the Trie.
IEnumerable<string> GetWords();

// Gets words for given prefix.
IEnumerable<string> GetWords(string prefix);

// Returns true if the word is present in the Trie.
bool HasWord(string word);

// Returns true if the prefix is present in the Trie.
bool HasPrefix(string prefix);

// Gets the equivalent TrieNode in the Trie for given prefix.
TrieNode GetTrieNode(string prefix)

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

// Gets the root trieNode.
TrieNode GetRootTrieNode();
```

### Trie Example

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

// Gets trieNode for prefix.
TrieNode trieNode = trie.GetTrieNode("tes");

// Gets trieNode from trieNode by prefix.
TrieNode testTrieNode = trieNode.GetTrieNode("t");

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

// Gets the root trieNode.
var root = trie.GetRootTrieNode();

```

### TrieMap Methods

```c#
// Gets TValue item for key from TrieMap.
TValue ValueBy(string key);

// Gets TValue items by key prefix from TrieMap.
IEnumerable<TValue> ValuesBy(string keyPrefix);

// Gets all TValue items from TrieMap.
IEnumerable<TValue> Values();

// Gets all keys from TrieMap.
IEnumerable<string> Keys();

// Gets all string->TValue pairs from TrieMap.
IEnumerable<KeyValuePair<string, TValue>> KeyValuePairs();

// Adds TValue item for key to TrieMap.
void Add(string key, TValue value);

// Returns true if key present in TrieMap.
bool HasKey(string key);

// Returns true if key prefix present in TrieMap.
bool HasKeyPrefix(string keyPrefix);

// Gets TrieNode for given key prefix.
TrieNode<TValue> GetTrieNode(string keyPrefix)

// Removes key from TrieMap.
void Remove(string key);

// Removes key prefix from TrieMap and return true else false.
bool RemoveKeyPrefix(string keyPrefix);

// Clears all values from TrieMap.
void Clear();

// Gets the root trieNode.
TrieNode<TValue> GetRootTrieNode();
```

### TrieMap Example

```c#
ITrieMap<int> trieMap = new TrieMap<int>();

// Adds key-value.
trieMap.Add("key1", 1);
trieMap.Add("key12", 12);
trieMap.Add("key2", 2);

// Gets value item for key.
var value = trieMap.ValueBy("key1"); // value = 1

// Gets value items by key prefix.
var values = trieMap.ValuesBy("key1"); // values = { 1, 12 }

// Gets all value items.
var values = trieMap.Values(); // values = { 1, 12, 2 }

// Gets all value items.
var keys = trieMap.Keys(); // keys = { "key1", "key12", "key2" }

// Gets all key-value items.
var kvPairs = trieMap.KeyValuePairs();

// Returns true if key present.
bool hasKey = trieMap.HasKey("key1"); // hasKey = true

// Returns true if key prefix present.
bool hasKeyPrefix = trieMap.HasKeyPrefix("key"); // hasKeyPrefix = true

// Gets trieNode for key prefix.
TrieNode<int> key1TrieNode = trie.GetTrieNode("key1");

// Gets trieNode from trieNode by prefix.
TrieNode<int> key12TrieNode = key1TrieNode.GetTrieNode("2");

// Removes key.
trieMap.Remove("key1");
trieMap.Remove("key"); // throws

// Removes by key prefix.
bool isKeyPrefixRemoved = trieMap.RemoveKeyPrefix("key"); // isKeyPrefixRemoved = true

// Clears triemap.
trieMap.Clear();

// Gets the root trieNode.
var root = trie.GetRootTrieNode();
```


#### Note: 

Not thread-safe.
