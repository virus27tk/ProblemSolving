public class Solution {
    public IList<int> GetRow(int rowIndex) {
        if (rowIndex == 0){
            return new List<int>{1};
        }

        if (rowIndex == 1){
            return new List<int>{1,1};
        }

        var result = new List<int>{1,1};

        for (int i = 2; i <=rowIndex; i++)
        {
            int inter = -1;
            for(int j=1; j<=i; j++){
                if (j == i){
                    result[j-1] = inter;
                    result.Add(1);
                }
                else
                {
                    var temp = result[j] + result[j-1];
                    if (inter != -1){
                        result[j-1] = inter;
                    }
                    inter = temp;
                }
            }
        }

        return result;
        
    }
}