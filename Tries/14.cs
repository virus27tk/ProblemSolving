public class Solution {
    public string LongestCommonPrefix(string[] strs) {
        Node root = new Node();
        foreach(var str in strs)
        {
            InsertToTrie(str, root);
        }

        if (root.childs.Count > 1)
        {
            return string.Empty;
        }

        var temp = root;
        var result = 0;

        while(temp.childs.Count == 1 && temp.childs.First().Value.cp == strs.Length)
        {
            result ++;
            temp = temp.childs.First().Value;
        }

        return strs[0].Substring(0, result);

    }

    public void InsertToTrie(string str, Node root)
    {
        var temp = root;
        foreach(var c in str)
        {
            if (temp.Contains(c))
            {
                temp = temp.MoveToChild(c);
                temp.cp++;
            }
            else
            {
                temp.AddChild(c);
                temp = temp.MoveToChild(c);
            }
        }
    }

    public class Node
    {
        public Dictionary<int, Node> childs;
        public int cp;

        public Node()
        {
            childs = new Dictionary<int, Node>();
            cp = 1;
        }

        public bool Contains(char c)
        {
            return childs.ContainsKey(c - 'a');
        }

        public Node MoveToChild(char c)
        {
            return childs[c-'a'];
        }

        public void AddChild(char c)
        {
            var node = new Node();
            childs.Add(c - 'a', node); 
        }
    }
}