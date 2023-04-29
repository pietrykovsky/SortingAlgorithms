namespace SortingAlgorithms
{
    internal class ArraySwap
    {
        public static void Swap(Movie[] movies, int i, int j)
        {
            Movie temp = movies[i];
            movies[i] = movies[j];
            movies[j] = temp;
        }
    }
}
