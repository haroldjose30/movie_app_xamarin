using System;
using CodeChallenge.Models;
using CodeChallenge.ViewModels.Base;
using Xamarin.Forms;

namespace CodeChallenge.ViewModels
{
    public interface IMovieItemViewModel: IBaseViewModel
    {
        Movie movie { get; set; }
        string PosterPath { get; set; }
        string ReleaseDate { get; set; }
        string Genres { get; set; }
        Command TapCommand { get; }
    }
}