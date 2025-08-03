public class Solution_HeadRecursion
{
    public bool CanPartition(int[] nums)
    {
        var sum = 0;
        foreach (int i in nums)
        {
            sum += i;
        }

        if (sum % 2 != 0)
        {
            return false;
        }

        var expectedSum = sum / 2;
        return foo(0, nums, expectedSum);

    }

    private bool foo(int i, int[] nums, int sum)
    {
        if (sum == 0)
        {
            return true;
        }

        if (i > nums.Count() - 1)
        {
            return false;
        }

        var with = foo(i + 1, nums, sum - nums[i]);
        var without = foo(i + 1, nums, sum);

        return with || without;
    }
}

public class Solution_Memoization
{
    public bool CanPartition(int[] nums)
    {
        var sum = 0;
        foreach (int i in nums)
        {
            sum += i;
        }

        if (sum % 2 != 0)
        {
            return false;
        }

        var expectedSum = sum / 2;
        Dictionary<(int, int), bool> dp = new Dictionary<(int, int), bool>();
        return foo(0, nums, expectedSum, dp);

    }

    private bool foo(int i, int[] nums, int sum, Dictionary<(int, int), bool> dp)
    {
        if (dp.ContainsKey((i, sum)))
        {
            return dp[(i, sum)];
        }

        if (sum == 0)
        {
            return dp[(i, sum)] = true;
        }

        if (i > nums.Count() - 1)
        {
            return dp[(i, sum)] = false;
        }

        var with = foo(i + 1, nums, sum - nums[i], dp);
        var without = foo(i + 1, nums, sum, dp);

        dp[(i, sum)] = with || without;
        return with || without;
    }
}

public class Solution_Memoization
{
    public bool CanPartition(int[] nums)
    {
        var sum = 0;
        foreach (int i in nums)
        {
            sum += i;
        }

        if (sum % 2 != 0)
        {
            return false;
        }

        var expectedSum = sum / 2;
        Dictionary<(int, int), bool> dp = new Dictionary<(int, int), bool>();
        return foo(0, nums, expectedSum, dp);

    }

    private bool foo(int i, int[] nums, int sum, Dictionary<(int, int), bool> dp)
    {
        if (dp.ContainsKey((i, sum)))
        {
            return dp[(i, sum)];
        }

        if (sum == 0)
        {
            return dp[(i, sum)] = true;
        }

        if (i > nums.Count() - 1)
        {
            return dp[(i, sum)] = false;
        }

        var with = foo(i + 1, nums, sum - nums[i], dp);
        var without = foo(i + 1, nums, sum, dp);

        dp[(i, sum)] = with || without;
        return with || without;
    }
}

public class Solution {
    public bool CanPartition(int[] nums) {

        var sum1 = 0;
        foreach (int i in nums){
            sum1 += i;
        }

        if (sum1%2 != 0 ){
            return false;
        }

        var expectedSum = sum1/2;
        bool[,] dp = new bool[nums.Count()+1, expectedSum+1];
        dp[0,0] = true;
        for(int n = 0 ; n<=nums.Count() ; n++){
            dp[n,0] = true;
        }

        for(int sum=1; sum<=expectedSum; sum++){
            dp[0,sum] = false;
        }

        for(int i = 1 ; i<=nums.Count() ; i++)
        {
            for(int j=1; j<=expectedSum; j++)
            {
                if (j - nums[i-1] < 0){
                    dp[i,j] = dp[i-1,j];
                }
                else if (j - nums[i-1] >= 0 )
                {
                    dp[i,j] = dp[i-1,j - nums[i-1]] || dp[i-1,j];
                }
            }
        }

        return dp[nums.Count(), expectedSum];

    }
}