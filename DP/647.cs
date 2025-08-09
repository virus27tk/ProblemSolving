public class Solution {
    public int CountSubstrings(string s) 
    {
        int result = 0;
        for (int k=0; k<s.Length; k++)
        {
            int i=k;
            int j=k;
            while(i >=0 && j<s.Length && s[i] == s[j])
            {
                result++;
                i--;j++;
            }

            i=k;
            j=k+1;
            while(i >=0 && j<s.Length && s[i] == s[j])
            {
                result++;
                i--;j++;
            }
        }

        return result;
        
    }
}