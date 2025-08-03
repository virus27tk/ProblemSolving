public class Solution {
    public bool IsSubsequence(string s, string t) {
        int res = 0;

        if (s.Length == 0)
        {
            return true;
        }

        for(int i =0; i<t.Length; i++)
        {
            if (t[i] == s[res]){
                res++;
            }

            if (res == s.Length){
                return true;
            }
        }

        return res == s.Length;
    }
}