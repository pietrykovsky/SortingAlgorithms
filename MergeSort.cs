namespace SortingAlgorithms
{
    internal class MergeSort : ArraySwap
    {

        public static void Sort(Movie[] movies, Order order)
        {
            Sort(movies, 0, movies.Length - 1, order);
        }

        private static void Sort(Movie[] movies, int left, int right, Order order)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                Sort(movies, left, mid, order);
                Sort(movies, mid + 1, right, order);
                Merge(movies, left, mid, right, order);
            }
        }

        private static void Merge(Movie[] movies, int left, int mid, int right, Order order)
        {
            int len1 = mid - left + 1;
            int len2 = right - mid;
            var leftArr = new Movie[len1];
            var rightArr = new Movie[len2];
            int i, j;

            for (i = 0; i < len1; ++i)
                leftArr[i] = movies[left + i];
            for (j = 0; j < len2; ++j)
                rightArr[j] = movies[mid + 1 + j];

            i = j = 0;
            int k = left;
            bool sortingCondition;

            while (i < len1 && j < len2)
            {
                sortingCondition = (order == Order.descending)
                    ? (leftArr[i].Rating >= rightArr[j].Rating)
                    : leftArr[i].Rating <= rightArr[j].Rating;
                if (sortingCondition)
                    movies[k++] = leftArr[i++];
                else
                    movies[k++] = rightArr[j++];
            }

            while (i < len1)
                movies[k++] = leftArr[i++];
            while (j < len2)
                movies[k++] = rightArr[j++];
        }
    }
}