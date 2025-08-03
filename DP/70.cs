public class Solution_HeadRecuursion
{
    public int ClimbStairs(int n)
    {
        return foo(0, n);
    }

    private int foo(int i, int n)
    {
        if (i >= n - 1)
        {
            return 1;
        }
        return foo(i + 1, n) + foo(i + 2, n);
    }
}

public class Solution_Memoized
{
    public int ClimbStairs(int n)
    {
        Dictionary<int, int> dp = new Dictionary<int, int>();
        return foo(0, n, dp);
    }

    private int foo(int i, int n, Dictionary<int, int> dp)
    {
        if (dp.ContainsKey(i))
        {
            return dp[i];
        }
        if (i >= n - 1)
        {
            return dp[i] = 1;
        }
        return dp[i] = foo(i + 1, n, dp) + foo(i + 2, n, dp);
    }
}

public class Solution_DP
{
    public int ClimbStairs(int n)
    {
        List<int> dp = new List<int>();
        dp.Add(1);
        dp.Add(1);

        for (int i = 2; i <= n; i++)
        {
            var x = dp[i - 1] + dp[i - 2];
            dp.Add(x);
        }

        return dp[n];
    }
}