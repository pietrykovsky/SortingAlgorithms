namespace SortingAlgorithms;
internal class Movie
{
    public Movie()
    {
    }

    public Movie(int id, string title, float rating)
    {
        Id = id;
        Title = title;
        Rating = rating;
    }


    public int Id { get; set; }
    public string Title { get; set; }
    public float Rating { get; set; }

    public static bool operator <(Movie a, Movie b) => a.Rating < b.Rating;
    public static bool operator >(Movie a, Movie b) => a.Rating > b.Rating;
    public static bool operator <=(Movie a, Movie b) => a.Rating <= b.Rating;
    public static bool operator >=(Movie a, Movie b) => a.Rating >= b.Rating;
    public static bool operator ==(Movie a, Movie b) => a.Rating == b.Rating;
    public static bool operator !=(Movie a, Movie b) => a.Rating != b.Rating;
}