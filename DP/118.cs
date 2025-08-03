public class Solution {
    public IList<IList<int>> Generate(int numRows) {
        var result = new List<IList<int>>();
        result.Add(new List<int>{ 1 });
        if (numRows == 1){
            return result;
        }

        result.Add(new List<int>{ 1, 1 });
        if (numRows == 2){
            return result;
        }

        for (int i = 2; i<numRows; i++){
            var inter = new List<int>();
            for(int j=0; j<i+1; j++){
                if (j == 0 || j==i){
                    inter.Add(1);
                }
                else
                {
                    var valueToAdd = result[i-1][j] + result[i-1][j-1];
                    inter.Add(valueToAdd);
                }
            } 

            result.Add(inter);
        }

        return result;
    }
}