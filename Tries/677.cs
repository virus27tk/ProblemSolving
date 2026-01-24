//Constrcut Trie
//At each node maintain sum of all values of keys which pass through that node
//return sum at the end of prefix
//Use dictionary to maintain key-value pairs for updating values
//When inserting, if key already present, update the difference in values along the path
public class MapSum {
    Dictionary<string, int> dict;
    Node root;
    public MapSum() {
        dict = new Dictionary<string, int>();
        root = new Node();
    }
    
    public void Insert(string key, int val) {
        if (dict.ContainsKey(key))
        {
            int tempValue = dict[key];
            dict[key] = val;
            AddToTrie(root, key, val-tempValue);
        }
        else
        {
            dict.Add(key, val);
            AddToTrie(root, key, val);
        }
    }
    
    public int Sum(string prefix) {
        var temp = root;
        int sum = 0;
        foreach(char c in prefix)
        {
            if (temp.Contains(c))
            {
                temp = temp.Move(c);
            }
            else
            {
                return 0;
            }
        }

        return temp.sum;
    }

    public void AddToTrie(Node root, string key, int val)
    {
        var temp = root;
        foreach(char c in key)
        {
            if (temp.Contains(c))
            {
                temp = temp.Move(c);
                temp.sum += val;
            }
            else
            {
                temp.childs[c-'a'] = new Node();
                temp = temp.Move(c);
                temp.sum = val;
            }
        }
    }

    public class Node
    {
        public Node[] childs;
        public int sum;

        public Node()
        {
            childs = new Node[26];
            sum = 0;
        }

        public bool Contains(char c)
        {
            return childs[c-'a'] != null;
        }

        public Node Move(char c)
        {
            return childs[c-'a'];
        }
    }
}