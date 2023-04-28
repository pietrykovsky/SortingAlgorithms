namespace SortingAlgorithms
{
    internal class QuickSort : ArraySwap
    {
        public static Movie[] Sort(Movie[] movieArr, Order order)
        {
            var movies = new Movie[movieArr.Length];
            movieArr.CopyTo(movies, 0);
            Sort(movies, 0, movies.Length - 1, order);
            return movies;
        }


        private static void Sort(Movie[] movies, int left, int right, Order order)
        {
            if (left >= right || left < 0)
                return;
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
    }
}
