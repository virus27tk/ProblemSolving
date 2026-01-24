public class Solution {
    //Construct Trie
    //Check each word, whether each prefix of the word in present in Trie
    //Longest such word will be the answer
    public string LongestWord(string[] words)
    {
        Node root = new Node();
        foreach(var word in words)
        {
            var temp = root;
            foreach(var c in word)
            {
                if (temp.Contains(c))
                {
                    temp = temp.Move(c);
                }
                else
                {
                    temp = temp.CreateAndMove(c);
                }
            }

            temp.flag = true;
        }

        string result = string.Empty;

        foreach(var word in words)
        {
            var isPresent = IsWordPresent(root, word);
            if (isPresent)
            {
                if (result.Length == word.Length)
                {
                    result = FindSmall(result, word);
                }
                else
                {
                    result = result.Length > word.Length ? result : word;
                }
                
            }
        }

        return result;

    }

    public string FindSmall(string a, string b)
    {
        for (int i=0; i<a.Length; i++)
        {
            if (a[i] < b[i])
            {
                return a;
            }
            else if (a[i] > b[i])
            {
                return b;
            }
        }

        return a;
    }

    public bool IsWordPresent(Node root, string word)
    {
        var temp = root;
        foreach(var c in word)
        {
            if (temp.Contains(c))
            {
                temp = temp.Move(c);
                if (!temp.flag)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return temp.flag;
    }

    public class Node
    {
        public Node[] childs;
        public bool flag;

        public Node()
        {
            childs = new Node[26];
            flag = false;
        }

        public bool Contains(char c)
        {
            return childs[c-'a'] != null;
        }

        public Node Move(char c)
        {
            return childs[c-'a'];
        }

        public Node CreateAndMove(char c)
        {
            var node = new Node();
            childs[c-'a'] = node;
            return childs[c-'a'];
        }
    }
}