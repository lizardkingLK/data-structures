using dataStructures.Core.NonLinear.Tries;

namespace dataStructures.Program;

class Program
{
    static void Main()
    {
        SuffixTree st = new();
        Trie trie = new();
        trie.Insert("fo");
        trie.Insert("fo sho");
        trie.Insert("fo real");
        trie.Insert("foobar");
        trie.Insert("fus-ro-dah");
        
    }
}
