public class Solution_BruteForce {
    public int LongestMountain(int[] arr) {
        
        int maxi = 0;

        for(int i=0; i< arr.Count()-2; i++)
        {
            int j=i;

            if (arr[j+1]<=arr[j]){
                continue;
            }

            bool asc = true;
            while(j<arr.Count()-1){
                if (asc && arr[j]==arr[j+1]){
                    break;
                }

                if (asc && arr[j+1]>arr[j]){
                    j++;
                }
                else if (asc && arr[j+1]<arr[j]){
                    j++;
                    asc = false;
                }
                else if (!asc && arr[j]>arr[j+1]){
                    j++;
                }
                else if (!asc && arr[j+1]>=arr[j]){
                    if (j-i+1 >= 3)
                        maxi=Math.Max(maxi, j-i+1);
                    break;
                }
            }

            if (!asc && j==arr.Count()-1 && j-i+1 >=3){
                maxi=Math.Max(maxi, j-i+1);
            }
        }
        return maxi;

    }
}

public class Solution_Greedy {
    public int LongestMountain(int[] arr) {
        List<int> incr = new List<int>();
        for(int i=0;i<arr.Count();i++){
            incr.Add(1);
        }

        List<int> decr = new List<int>();
        for(int i=0;i<arr.Count();i++){
            decr.Add(1);
        }

        //Console.WriteLine($"{incr.Count()}, {decr.Count()}, {arr.Count()}");

        bool isValid = false;
        for(int i=1; i< arr.Count();i++){
            if (arr[i] > arr[i-1]){
                isValid  = true;
                incr[i] = incr[i-1] + 1;
            }
        }

        if (!isValid){
            return 0;
        }

        isValid = false;
        for(int i=decr.Count()-2;i>=0;i--){
            if(arr[i]>arr[i+1]){
                isValid  = true;
                decr[i] = decr[i+1] + 1;
            }
        }

        if (!isValid){
            return 0;
        }
        
        int maxi = 0;
        for(int i=0;i<arr.Count();i++){
            if (incr[i] > 1 && decr[i] > 1 && incr[i]+decr[i]-1 >=3){
                maxi  = Math.Max(maxi, incr[i]+decr[i]-1);
            }
        }

        return maxi;
    }
}