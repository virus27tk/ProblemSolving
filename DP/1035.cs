public class Solution_HeadRecusion {
    public int MaxUncrossedLines(int[] nums1, int[] nums2) {
        return f(0,0, nums1, nums2);
    }

    private int f(int i, int j, int[] nums1, int[] nums2)
    {
        if (i >= nums1.Count() || j >=nums2.Count()){
            return 0;
        }

        if (nums1[i] == nums2[j]){
            return 1 + f(i+1, j+1, nums1, nums2);
        }
        else
        {
            return Math.Max(f(i, j+1, nums1, nums2), f(i+1, j, nums1, nums2));
        }
    }
}

public class Solution_Memo {
    public int MaxUncrossedLines(int[] nums1, int[] nums2) {
        Dictionary<Tuple<int, int>, int> dp = new Dictionary<Tuple<int, int>, int>();
        return f(0,0, nums1, nums2, dp);
    }

    private int f(int i, int j, int[] nums1, int[] nums2, Dictionary<Tuple<int, int>, int> dp)
    {
        if (dp.ContainsKey(Tuple.Create(i,j))){
            return dp[Tuple.Create(i,j)];
        }

        if (i >= nums1.Count() || j >=nums2.Count()){
            return dp[Tuple.Create(i,j)] = 0;
        }

        if (nums1[i] == nums2[j]){
            return dp[Tuple.Create(i,j)] = 1 + f(i+1, j+1, nums1, nums2, dp);
        }
        else
        {
            return dp[Tuple.Create(i,j)] = Math.Max(f(i, j+1, nums1, nums2, dp), f(i+1, j, nums1, nums2, dp));
        }
    }
}

public class Solution {
    public int MaxUncrossedLines(int[] nums1, int[] nums2) 
    {
        int[,] dp = new int[nums1.Count()+1, nums2.Count()+1];

        int m = nums1.Count();
        int n = nums2.Count();

        for(int i=0;i<=m;i++){
            dp[i,n] = 0;
        }

        for(int j=0;j<=n;j++){
            dp[m,j] = 0;
        }

        for (int i = m-1; i >=0; i--)
        {
            for(int j= n-1; j>=0; j--)
            {
                if (nums1[i] == nums2[j])
                {
                    dp[i,j] = 1 + dp[i+1, j+1];
                }
                else
                {
                    dp[i,j] = Math.Max(dp[i+1,j], dp[i,j+1]);
                }

            }
        }

        return dp[0,0]; 
    }
}