namespace dataStructures.Core.NonLinear.Tries;

public class Trie
{
    private record TrieNode
    {
        public Dictionary<char, TrieNode> ChildMap = [];
        public bool IsEndOfWord { get; set; }
    }

    private readonly TrieNode _root;

    public Trie()
    {
        _root = new();
    }

    public void Insert(string word)
    {
        word = word.ToLowerInvariant().Trim();

        TrieNode? current = _root;
        foreach (char letter in word)
        {
            if (!current.ChildMap.TryGetValue(letter, out TrieNode? child))
            {
                child = new();
                current.ChildMap[letter] = child;
            }

            current = child;
        }

        current.IsEndOfWord = true;
    }

    public void Delete(string word)
    {
        Delete(word.ToLowerInvariant().Trim(), _root, 0);
    }

    private static bool Delete(string word, TrieNode current, int index)
    {
        if (word.Length == index)
        {
            if (!current.IsEndOfWord)
            {
                return false;
            }

            current.IsEndOfWord = false;

            return current.ChildMap.Count == 0;
        }

        if (!current.ChildMap.TryGetValue(word[index], out TrieNode? childNode))
        {
            return false;
        }

        bool canDeleteChild = Delete(word, childNode, index + 1);
        if (canDeleteChild)
        {
            current.ChildMap.Remove(word[index]);
        }

        return !current.IsEndOfWord && current.ChildMap.Count == 0;
    }

    public IEnumerable<string> AutocompleteIterative(string prefix)
    {
        prefix = prefix.ToLowerInvariant().Trim();

        TrieNode? current = _root;
        for (int i = 0; i < prefix.Length; i++)
        {
            if (!current.ChildMap.TryGetValue(prefix[i], out TrieNode? child))
            {
                yield break;
            }

            current = child;
        }

        List<char> chars = [.. prefix];
        Stack<(TrieNode, List<char>)> tries = new();
        tries.Push((current, chars));
        while (tries.TryPop(out (TrieNode, List<char>) popped))
        {
            (current, chars) = popped;
            if (current == null)
            {
                continue;
            }

            foreach ((char childLetter, TrieNode? child) in current.ChildMap)
            {
                chars.Add(childLetter);
                tries.Push((child, [.. chars]));
                chars.RemoveAt(chars.Count - 1);
            }

            if (current.IsEndOfWord)
            {
                yield return string.Join(null, chars);
            }
        }
    }

    public IEnumerable<string> AutocompleteRecursive(string prefix)
    {
        prefix = prefix.ToLowerInvariant().Trim();

        if (!IsValidPrefix(prefix, out TrieNode? current))
        {
            yield break;
        }

        foreach (string match in AutoCompleteRecursively(current!))
        {
            yield return prefix + match;
        }
    }

    private bool IsValidPrefix(string prefix, out TrieNode? current)
    {
        current = _root;
        foreach (char letter in prefix)
        {
            if (!current.ChildMap.TryGetValue(letter, out TrieNode? child))
            {
                return false;
            }

            current = child;
        }

        return true;
    }

    private static IEnumerable<string> AutoCompleteRecursively(TrieNode current)
    {
        if (current.IsEndOfWord)
        {
            yield return string.Empty;
        }

        foreach ((char letter, TrieNode? child) in current.ChildMap)
        {
            foreach (string collected in AutoCompleteRecursively(child))
            {
                yield return letter + collected;
            }
        }
    }

    public bool Search(string word)
    {
        word = word.ToLowerInvariant().Trim();

        TrieNode? current = _root;
        foreach (char letter in word)
        {
            if (!current.ChildMap.TryGetValue(letter, out TrieNode? child))
            {
                return false;
            }

            current = child;
        }

        return current.IsEndOfWord;
    }

    public bool StartsWith(string prefix)
    {
        prefix = prefix.ToLowerInvariant().Trim();

        TrieNode? current = _root;
        foreach (char letter in prefix)
        {
            if (!current.ChildMap.TryGetValue(letter, out TrieNode? child))
            {
                return false;
            }

            current = child;
        }

        return true;
    }

    public int Count(string prefix) => AutocompleteIterative(prefix).Count();
}