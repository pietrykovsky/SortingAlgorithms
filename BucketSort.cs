namespace SortingAlgorithms
{
    internal class BucketSort
    {
        public static void Sort(Movie[] movies, Order order)
        {
            int numberOfBuckets = (int)Math.Sqrt(movies.Length);
            List<Movie>[] buckets = new List<Movie>[numberOfBuckets];

            // Initialize buckets
            for (int i = 0; i < numberOfBuckets; i++)
            {
                buckets[i] = new List<Movie>();
            }

            // Calculate the range of the Ratings
            float minValue = movies[0].Rating;
            float maxValue = movies[0].Rating;

            foreach (Movie movie in movies)
            {
                minValue = Math.Min(minValue, movie.Rating);
                maxValue = Math.Max(maxValue, movie.Rating);
            }

            float range = maxValue - minValue;

            // Put the movies into the appropriate buckets
            foreach (Movie movie in movies)
            {
                int bucketIndex = (int)((movie.Rating - minValue) / range * (numberOfBuckets - 1));
                buckets[bucketIndex].Add(movie);
            }

            // Sort the buckets using QuickSort
            foreach (List<Movie> bucket in buckets)
            {
               // QuickSort.Sort(bucket.ToArray(), order);
            }

            // Concatenate the sorted buckets to get the sorted array
            int index = 0;
            if (order == Order.ascending)
            {
                foreach (List<Movie> bucket in buckets)
                {
                    foreach (Movie movie in bucket)
                    {
                        movies[index++] = movie;
                    }
                }
            }
            else // Order.descending
            {
                for (int i = buckets.Length - 1; i >= 0; i--)
                {
                    List<Movie> bucket = buckets[i];
                    foreach (Movie movie in bucket)
                    {
                        movies[index++] = movie;
                    }
                }
            }
        }
    }
}
