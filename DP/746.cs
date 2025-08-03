public class Solution_HeadRecursion
{
    public int MinCostClimbingStairs(int[] cost)
    {
        return Math.Min(foo(cost, 0), foo(cost, 1));
    }

    private int foo(int[] cost, int i)
    {
        if (i == cost.Count() - 1)
        {
            return cost[i];
        }

        if (i > cost.Count() - 1)
        {
            return 0;
        }
        int onestep = cost[i] + foo(cost, i + 1);
        int twostep = cost[i] + foo(cost, i + 2);
        return Math.Min(onestep, twostep);
    }
}

public class Solution_Memoization
{
    public int MinCostClimbingStairs(int[] cost)
    {
        Dictionary<int, int> dp = new Dictionary<int, int>();
        return Math.Min(foo(cost, 0, dp), foo(cost, 1, dp));
    }

    private int foo(int[] cost, int i, Dictionary<int, int> dp)
    {
        if (i > cost.Count() - 1)
        {
            return 0;
        }

        if (dp.ContainsKey(i))
        {
            return dp[i];
        }

        if (i == cost.Count() - 1)
        {
            dp[i] = cost[i];
            return cost[i];
        }


        int onestep = cost[i] + foo(cost, i + 1, dp);
        int twostep = cost[i] + foo(cost, i + 2, dp);
        return dp[i] = Math.Min(onestep, twostep);
    }
}

public class Solution
{
    public int MinCostClimbingStairs(int[] cost)
    {
        Dictionary<int, int> dp = new Dictionary<int, int>();
        return Math.Min(foo(cost, 0, dp), foo(cost, 1, dp));
    }

    private int foo(int[] cost, int i, Dictionary<int, int> dp)
    {
        if (i > cost.Count() - 1)
        {
            return 0;
        }

        if (dp.ContainsKey(i))
        {
            return dp[i];
        }

        if (i == cost.Count() - 1)
        {
            dp[i] = cost[i];
            return cost[i];
        }


        int onestep = cost[i] + foo(cost, i + 1, dp);
        int twostep = cost[i] + foo(cost, i + 2, dp);
        return dp[i] = Math.Min(onestep, twostep);
    }
}