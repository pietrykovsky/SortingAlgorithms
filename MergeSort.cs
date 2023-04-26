namespace SortingAlgorithms;

internal class MergeSort : ArraySwap
{
    public static Movie[] Sort(Movie[] movieArr, Order order)
    {
        if (movieArr.Length <= 1)
            return movieArr;
        var movieArrays = Divide(movieArr);
        movieArrays.Item1 = Sort(movieArrays.Item1, order);
        movieArrays.Item2 = Sort(movieArrays.Item2, order);
        var result = Merge(movieArrays.Item1, movieArrays.Item2, order);
        return result;
    }

    private static (Movie[], Movie[]) Divide(Movie[] movieArr)
    {
        var len1 = movieArr.Length / 2;
        var len2 = movieArr.Length - len1;
        var array1 = new Movie[len1];
        var array2 = new Movie[len2];
        for (var i = 0; i < len1; i++)
            array1[i] = movieArr[i];
        for (var i = 0; i < len2; i++)
            array2[i] = movieArr[len1 + i - 1];
        return (array1, array2);
    }

    private static Movie[] Merge(Movie[] array1, Movie[] array2, Order order)
    {
        var len = array1.Length + array2.Length;
        var result = new Movie[len];
        int i = 0, j = 0, k = 0;
        bool sortingCondition;
        while (i < array1.Length && j < array2.Length)
        {
            sortingCondition = order == Order.ascending ? array1[i] <= array2[j] : array1[i] >= array2[j];
            if (sortingCondition)
            {
                result[k] = array1[i];
                i++;
            }
            else
            {
                result[k] = array2[j];
                j++;
            }

            k++;
        }

        while (i < array1.Length)
        {
            result[k] = array1[i];
            i++;
            k++;
        }

        while (j < array2.Length)
        {
            result[k] = array2[j];
            j++;
            k++;
        }

        return result;
    }
}