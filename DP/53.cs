public class Solution_HeadRecursion {
    public int MaxSubArray(int[] nums) {
        return f(0, nums);
    }

    private int f(int i, int[] nums){
        if (i == nums.Count()-1){
            return nums[i];
        }

        int without = f(i+1, nums);
        int with = g(i, nums);
        return Math.Max(without, with);
    }

    private int g(int i, int[] nums){
        if (i == nums.Count()-1){
            return nums[i];
        }
        return Math.Max(nums[i], nums[i] + g(i+1, nums));
    }


}

public class Solution_Memoization {
    public int MaxSubArray(int[] nums) {
        Dictionary<int, int> dpf = new Dictionary<int, int>();
        Dictionary<int, int> dpg = new Dictionary<int, int>();
        return f(0, nums, dpf, dpg);
    }

    private int f(int i, int[] nums, Dictionary<int, int> dpf, Dictionary<int, int> dpg){
        if (dpf.ContainsKey(i)){
            return dpg[i];
        }

        if (i == nums.Count()-1){
            dpf.Add(i, nums[i]);
            return nums[i];
        }

        int without = f(i+1, nums, dpf, dpg);
        int with = g(i, nums, dpg);
        dpf.Add(i, Math.Max(without, with));
        return Math.Max(without, with);
    }

    private int g(int i, int[] nums, Dictionary<int, int> dpg){
        if (dpg.ContainsKey(i)){
            return dpg[i];
        }
        if (i == nums.Count()-1){
            dpg.Add(i, nums[i]);
            return nums[i];
        }
        var result = Math.Max(nums[i], nums[i] + g(i+1, nums, dpg));
        dpg.Add(i, result);
        return result;
    }


}

public class Solution {
    public int MaxSubArray(int[] nums) {
        List<int> f = new List<int>();
        List<int> g = new List<int>();

        for(int i=0;i<nums.Count();i++){
            f.Add(-1);
            g.Add(-1);
        }

        f[0] = nums[0];
        g[0] = nums[0];

        for(int i=1;i<nums.Count();i++){
            g[i] = Math.Max(nums[i], nums[i] + g[i-1]);
            f[i] = Math.Max(f[i-1], g[i]);
        }

        return f[nums.Count()-1];
    }
}