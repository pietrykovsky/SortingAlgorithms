using System.Diagnostics;
using System.Globalization;
using Microsoft.VisualBasic;

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
        var sampleLengths = new int[] {1_000, 10_000, 50_000, 100_000, 500_000, 1_000_000, 2_000_000};
        var iterations = 20;
        
        TestSortingAlgorithms(movieData, iterations, sampleLengths);
    }

    private static void TestSortingAlgorithms(LinkedList<Movie> movieData, int iterations, int[] sampleLengths)
    {
        var sortingAlgorithms = new Dictionary<string, Func<Movie[], Order, Movie[]>>
        {
            { "Quicksort", QuickSort.Sort },
            //{ "Mergesort", MergeSort.Sort },
            //{ "Bucketsort", BucketSort.Sort },
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
                int size = 0;
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
                    size = movieArr.Length;
                    isSorted = IsSorted(movieArr, Order.descending);
                }
                averageSortingTime /= iterations;
                Console.WriteLine($"\nResults for - {funcName} {length} records:");
                Console.WriteLine($"average time: {averageSortingTime.ToString(CultureInfo.InvariantCulture)} ms");
                Console.WriteLine($"average rating: {averageRating.ToString(CultureInfo.InvariantCulture)}");
                Console.WriteLine($"median rating: {medianRating.ToString(CultureInfo.InvariantCulture)}");
                Console.WriteLine($"size: {size}");
                Console.WriteLine($"sorted correctly: {isSorted}\n\n");
            }
        }
    }

    private static LinkedList<Movie> Copy(LinkedList<Movie> movieList, int count)
    {
        var copiedList = new LinkedList<Movie>();
        var i = 1;
        foreach (var movie in movieList)
        {
            if (i >= count)
                break;
            copiedList.AddLast(movie);
            i++;
        }
        return copiedList;
    }

    private static bool IsSorted(LinkedList<Movie> movieList, Order order)
    {
        bool condition;
        var count = 0;
        var previous = movieList.First();
        foreach (Movie movie in movieList)
        {
            condition = order == Order.ascending ? previous > movie : previous < movie; 
            if (count > 0 && condition)
            {
                return false;
            }
            previous = movie;
            count++;
        }
        return true;
    }

    private static void PrintList(LinkedList<Movie> movieList)
    {
        foreach (var movie in movieList)
            Console.WriteLine($"{movie.Id} | {movie.Title} | {movie.Rating:F1}");
    }

    private static LinkedList<Movie> ReadCSVFile(string filePath)
    {
        var data = new List<Movie>();
        var watch = Stopwatch.StartNew();
        var iterations = 0;
        var count = 0;
        using (var reader = new StreamReader(filePath))
        {
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
                    data.AddLast(new Movie(id, title, rating));
                }
                else
                    count++;

                iterations++;
            }
        }
        watch.Stop();
        var time = watch.ElapsedMilliseconds;
        Console.WriteLine($"Time loading records for csv: {time} ms");
        Console.WriteLine($"Records searched: {iterations}");
        Console.WriteLine($"Records filtered out: {count}");
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

    private static float GetAverage(LinkedList<Movie> movieList)
    {
        var sum = 0f;
        foreach (var movie in movieList)
        {
            sum += movie.Rating;
        }
        return sum / movieList.Count;
    }

    /// <summary>
    /// Get median from sorted sequence.
    /// </summary>
    /// <param name="movieList">List of movies.</param>
    /// <returns>Median.</returns>
    private static float GetMedian(LinkedList<Movie> movieList)
    {
        var size = movieList.Count;
        if (size % 2 == 1)
            return movieList.ElementAt(size / 2).Rating;
        return (movieList.ElementAt(size / 2).Rating + movieList.ElementAt(size / 2 + 1).Rating) / 2;
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
