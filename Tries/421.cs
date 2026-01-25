public class Solution {
    public Node root;
    public int FindMaximumXOR(int[] nums)
    {
        root = new Node();

        foreach(var num in nums)
        {
            var temp = root;
            for(int i=31; i>=0; i--)
            {
                var checkBit = (num >> i) & 1;
                
                if (temp.Contains(checkBit))
                {
                    temp = temp.Move(checkBit);
                }
                else
                {
                    temp.bit[checkBit] = new Node();
                    temp = temp.Move(checkBit);
                }
            }
        }

        var maxi = -1;

        foreach(var num in nums)
        {
            maxi = Math.Max(maxi, getMax(num));
        }

        return maxi;
    }

    public int getMax(int num)
    {
        Node temp = root;

        int maxi = 0;

        for(int i=31; i>=0 ;i--)
        {
            int bit = num >> i & 1;
            if (temp.Contains(1-bit))
            {
                maxi = maxi | (1 << i);
                temp = temp.Move(1-bit);
            }
            else if (temp.Contains(bit))
            {
                temp = temp.Move(bit);
            }
        }

        return maxi;
    }

    public class Node
    {
        public Node[] bit;

        public Node()
        {
            bit = new Node[2];
        }

        public bool Contains(int i)
        {
            return bit[i] != null;
        }

        public Node Move(int i)
        {
            return bit[i];
        }
    }
}