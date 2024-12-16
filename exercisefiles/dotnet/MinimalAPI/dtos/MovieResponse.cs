using System;

namespace MinimalAPI.dtos;

// Define the MovieResponse class
public class MovieResponse
{
    public List<Movie> Search { get; set; }
}

public class Movie
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string imdbID { get; set; }
    public string Type { get; set; }
    public string Poster { get; set; }
}