namespace SortingAlgorithms
{
    internal class QuickSort : ArraySwap
    {
        public static void Sort(Movie[] movies, Order order)
        {
            Sort(movies, 0, movies.Length - 1, order);
        }

        /* private static void Sort(Movie[] movies, int left, int right, Order order)
         {
             if (left < right)
             {
                 int pivotIndex = Partition(movies, left, right, order);
                 Sort(movies, left, pivotIndex - 1, order);
                 Sort(movies, pivotIndex + 1, right, order);
             }
         }*/

        private static void Sort(Movie[] movies, int left, int right, Order order)
        {
            if (left < right)
            {
                int i = left;
                int j = right;
                Movie pivot = movies[left];

                while (i <= j)
                {
                    while ((order == Order.ascending && movies[i] < pivot) || (order == Order.descending && movies[i] > pivot))
                        i++;

                    while ((order == Order.ascending && movies[j] > pivot) || (order == Order.descending && movies[j] < pivot))
                        j--;

                    if (i <= j)
                    {
                        Swap(movies, i, j);
                        i++;
                        j--;
                    }
                }

                Sort(movies, left, j, order);
                Sort(movies, i, right, order);
            }
        }


        private static int Partition(Movie[] movies, int left, int right, Order order)
        {
            Movie pivotObject = movies[right];
            int pivotIndex = left - 1;

            for (int i = left; i <= right - 1; i++)
            {
                bool sortingCondition = (order == Order.descending) ? (movies[i] >= pivotObject) : movies[i] <= pivotObject;
                if (sortingCondition)
                {
                    pivotIndex++;
                    Swap(movies, i, pivotIndex);
                }
            }
            Swap(movies, pivotIndex + 1, right);
            return pivotIndex + 1;
        }
    }
}
