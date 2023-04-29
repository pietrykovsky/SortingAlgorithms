namespace SortingAlgorithms
{
    internal class QuickSort
    {
        public static Movie[] Sort(Movie[] movieArr, Order order)
        {
            var movies = new Movie[movieArr.Length];
            movieArr.CopyTo(movies, 0);
            Sort(movies, 0, movies.Length - 1, order);
            return movies;
        }


        public static void Sort(LinkedList<Movie> movieList, Order order)
        {
            if (left >= right || left < 0)
                return;
            {
                var condition = order == Order.ascending ? movieList.Last() < pivot : movieList.Last() > pivot;
                if (condition)
                {
                    L.AddLast(movieList.Last());
                    movieList.RemoveLast();
                }

                Sort(movies, left, j, order);
                Sort(movies, i, right, order);
            }
        }
    }
}