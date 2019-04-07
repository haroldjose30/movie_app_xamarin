using System;

namespace CodeChallenge.ViewModels
{
    public interface IMovieDetailPageViewModel
    {
        string Title { get; set; }
        string Overview { get; set; }
        string PosterPath { get; set; }
        string BackdropPath { get; set; }
        DateTimeOffset ReleaseDate { get; set; }
        string Genres { get; set; }
    }
}