namespace SortingAlgorithms
{
    internal class QuickSort
    {

        public static void Sort(LinkedList<Movie> movieList, Order order)
        {
            if (movieList.Count <= 1)
                return;
            var pivot = movieList.Last();
            var L = new LinkedList<Movie>();
            var E = new LinkedList<Movie>();
            var G = new LinkedList<Movie>();
            while (movieList.Count > 0)
            {
                var condition = order == Order.ascending ? movieList.Last() < pivot : movieList.Last() > pivot;
                if (condition)
                {
                    L.AddLast(movieList.Last());
                    movieList.RemoveLast();
                }
                else if (movieList.Last() == pivot)
                {
                    E.AddLast(movieList.Last());
                    movieList.RemoveLast();
                }
                else
                {
                    G.AddLast(movieList.Last());
                    movieList.RemoveLast();
                }
            }
            Sort(L, order);
            Sort(G, order);
            while (L.Count > 0)
            {
                movieList.AddLast(L.First());
                L.RemoveFirst();
            }
            while (E.Count > 0)
            {
                movieList.AddLast(E.First());
                E.RemoveFirst();
            }
            while (G.Count > 0)
            {
                movieList.AddLast(G.First());
                G.RemoveFirst();
            }
        }
    }
}