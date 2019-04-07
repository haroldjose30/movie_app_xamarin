using System;
using CodeChallenge.ViewModels.Base;

namespace CodeChallenge.ViewModels
{
    public interface IMovieDetailPageViewModel: IBaseViewModel
    {
        string Title { get; set; }
        string Overview { get; set; }
        string PosterPath { get; set; }
        string BackdropPath { get; set; }
        string ReleaseDate { get; set; }
        string Genres { get; set; }
    }
}