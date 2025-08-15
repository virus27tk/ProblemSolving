public class Solution_topdown {
    public int UniquePathsWithObstacles(int[][] obstacleGrid) {

        return f(0,0, obstacleGrid.Count(), obstacleGrid[0].Count(), obstacleGrid);
    }

    private int f(int i, int j, int m, int n, int[][] obstacleGrid)
    {
        if (i >= m || j>=n){
            return 0;
        } 

        if (obstacleGrid[i][j] == 1){
            return 0;
        }
        
        if (i==m-1 && j==n-1){
            return 1;
        }

        return f(i+1, j, m, n, obstacleGrid) + f(i, j+1, m, n, obstacleGrid);
    }
}

public class Solution {
    public int UniquePathsWithObstacles(int[][] obstacleGrid) {
        int m = obstacleGrid.Count();
        int n = obstacleGrid[0].Count();

        int[,] dp = new int[m,n];

        int i=0;
        while(i<m && obstacleGrid[i][0] == 0){
            dp[i,0] = 1;
            i++;
        }
        while(i<m){
            dp[i,0] = 0;
            i++;
        }

        int j=0;
        while(j<n && obstacleGrid[0][j] == 0){
            dp[0,j] = 1;
            j++;
        }
        while(j<n){
            dp[0,j] = 0;
            j++;
        }

        for(i=1;i<m;i++){
            for(j=1;j<n;j++){
                dp[i,j] = obstacleGrid[i][j] == 1 ? 0 : dp[i-1,j] + dp[i,j-1];
            }
        }

        return dp[m-1, n-1];
    }

}