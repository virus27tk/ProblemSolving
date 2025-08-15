public class Solution_Memo {
    public int NumDecodings(string s) {
        Dictionary<int, int> dp = new Dictionary<int, int>();

        return f(0, s, dp);
    }

    private int f(int i, string s, Dictionary<int, int> dp){
        if (dp.ContainsKey(i)){
            return dp[i];
        }
        if (i == s.Length){
            return dp[i] = 1;
        }

        if (i>s.Length){
            return 0;
        }

        if(s[i] == '0'){
            return dp[i] = 0;
        }

        int one = f(i+1, s, dp);
        int two = 0;

        if (i <= s.Length - 2){
            int twoDigit = int.Parse(s.Substring(i, 2));
            if (twoDigit >= 10 && twoDigit <= 26) {
                two = f(i + 2, s, dp);
            }
        } 

        return dp[i] = one + two;
    }
}

public class Solution {
    public int NumDecodings(string s) {
        int[] dp = new int[s.Length+1];

        dp[s.Length] = 1;

        for(int i = s.Length - 1; i< s.Length; i++)
        {
            dp[i] = dp[i+1];
        }

        for(int i = 0; i< s.Length; i++)
        {
           if (s[i] == '0'){
                dp[i] = 0;
            }
        }

        for(int i = s.Length - 2; i>=0; i--)
        {
            if (s[i] == '0'){
                dp[i] = 0;
            }
            else
            {
                int twoDigit = int.Parse(s.Substring(i, 2));
                if (twoDigit >= 10 && twoDigit <= 26) {
                    dp[i] = dp[i+1] + dp[i+2];
                }
                else
                {
                    dp[i] = dp[i+1];
                }
                
            }
        }

        return dp[0];

    }
}