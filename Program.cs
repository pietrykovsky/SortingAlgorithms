using System.Diagnostics;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace SortingAlgorithms;
public enum Order
{
    ascending,
    descending
}

internal class Program
{
    private const string FilePath = @"C:\Users\mivva\Desktop\projekty\C#\SortingAlgorithms\data.csv";
    private static void Main(string[] args)
    {
        var movieData = ReadCSVFile(FilePath);
        var sampleLengths = new int[] {1_000, 10_000, 50_000, 100_000, 500_000, 1_000_000};
        var iterations = 10;
        
        TestSortingAlgorithms(movieData, iterations, sampleLengths);
    }

    private static void TestSortingAlgorithms(List<Movie> movieData, int iterations, int[] sampleLengths)
    {
        var sortingAlgorithms = new Dictionary<string, Func<Movie[], Order, Movie[]>>
        {
            { "Quicksort", QuickSort.Sort },
            { "Mergesort", MergeSort.Sort },
            { "Bucketsort", BucketSort.Sort },
        };

        foreach (var pair in sortingAlgorithms)
        {
            var funcName = pair.Key;
            var sortingFunc = pair.Value;
            foreach (var length in sampleLengths)
            {
                var averageSortingTime = 0d;
                float averageRating = 0, medianRating = 0;
                bool isSorted = false;
                Console.WriteLine($"\n----{funcName} - data size {length}----");
                Console.WriteLine("\n{0,10} {1,20}", "Iteration", "Sorting Time (ms)");
                for (int i = 0; i < iterations; i++)
                {
                    var movieArr = CreateMovieArray(movieData, length);
                    var sortingTime = SortMovieArr(ref movieArr, Order.descending, sortingFunc);
                    averageRating = GetAverage(movieArr);
                    medianRating = GetMedian(movieArr);
                    Console.WriteLine("{0,10} {1,20}", i+1, sortingTime.ToString(CultureInfo.InvariantCulture));
                    averageSortingTime += sortingTime;
                    isSorted = IsSorted(movieArr, Order.descending);
                }
                averageSortingTime /= iterations;
                Console.WriteLine($"\nResults for - {funcName} {length} records:");
                Console.WriteLine($"average time: {averageSortingTime.ToString(CultureInfo.InvariantCulture)} ms");
                Console.WriteLine($"average rating: {averageRating.ToString(CultureInfo.InvariantCulture)}");
                Console.WriteLine($"median rating: {medianRating.ToString(CultureInfo.InvariantCulture)}");
                Console.WriteLine($"sorted correctly: {isSorted}\n\n");
            }
        }
    }

    private static bool IsSorted(Movie[] arr, Order order)
    {
        bool condition;
        for (int i = 1; i < arr.Length; i++)
        {
            condition = (order == Order.descending) ? (arr[i - 1] >= arr[i]) : arr[i - 1] <= arr[i];
            if (!condition)
                return false;
        }
        return true;
    }

    private static void PrintArray(Movie[] Arr)
    {
        foreach (var movie in Arr)
            Console.WriteLine($"{movie.Id} | {movie.Title} | {movie.Rating:F1}");
    }

    private static List<Movie> ReadCSVFile(string filePath)
    {
        var data = new List<Movie>();
        using (var reader = new StreamReader(filePath))
        {
            var count = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var columns = line.Split(',');
                if (IsRowClean(columns))
                {
                    int id = int.Parse(columns[0]);
                    string title = "";
                    float rating = float.Parse(columns[columns.Length - 1], CultureInfo.InvariantCulture.NumberFormat);
                    for (var i = 1; i < columns.Length - 1; i++)
                        title += $"{columns[i]}";
                    data.Add(new Movie(id, title, rating));
                }
            }
        }
        return data;
    }

    private static bool IsRowClean(string[] row)
    {
        if (row.Length < 3) return false;
        foreach (var column in row)
            if (column == "")
                return false;
        return true;
    }

    private static Movie[] CreateMovieArray(List<Movie> moviesData, int size)
    {
        size = moviesData.Count < size ? moviesData.Count : size;
        var movieArray = new Movie[size];
        for (var i = 0; i < size; i++)
            movieArray[i] = moviesData[i];
        return movieArray;
    }

    private static float GetAverage(Movie[] movieArr)
    {
        var sum = 0f;
        foreach (var movie in movieArr)
        {
            sum += movie.Rating;
        }
        return sum / movieArr.Length;
    }

    /// <summary>
    /// Get median from sorted collection.
    /// </summary>
    /// <param name="movieArr">Array of movies.</param>
    /// <returns>Median.</returns>
    private static float GetMedian(Movie[] movieArr)
    {
        var size = movieArr.Length;
        if (size % 2 == 1)
            return movieArr[size/2].Rating;
        return (movieArr[size / 2].Rating + movieArr[size / 2 + 1].Rating) / 2;
    }

    private static double SortMovieArr(ref Movie[] movieArr, Order order, Func<Movie[], Order, Movie[]> sortFunction)
    {
        var watch = new Stopwatch();
        watch.Start();
        movieArr = sortFunction(movieArr, order);
        watch.Stop();
        return watch.ElapsedMilliseconds;
    }
}