namespace SortingAlgorithms
{
    internal class BucketSort
    {
        public static Movie[] Sort(Movie[] movieArr, Order order)
        {
            int numberOfBuckets = 10;
            var movies = new Movie[movieArr.Length];
            movieArr.CopyTo(movies, 0);
            List<Movie>[] buckets = new List<Movie>[numberOfBuckets];

            // Initialize buckets
            for (int i = 0; i < numberOfBuckets; i++)
            {
                buckets[i] = new List<Movie>();
            }

            // Put the movies into the appropriate buckets
            foreach (Movie movie in movies)
            {
                int bucketIndex = (int)movie.Rating-1;
                buckets[bucketIndex].Add(movie);
            }

            // Sort the buckets using QuickSort
            foreach (List<Movie> bucket in buckets)
            {
                MergeSort.Sort(bucket.ToArray(), order);
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

            return movies;
        }
    }
}
