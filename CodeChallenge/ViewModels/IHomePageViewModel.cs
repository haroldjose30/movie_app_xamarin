﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CodeChallenge.Models;
using CodeChallenge.ViewModels.Base;
using Xamarin.Forms;

namespace CodeChallenge.ViewModels
{
    public interface IHomePageViewModel: IBaseViewModel
    {
        ObservableCollection<IMovieItemViewModel> Movies { get; set; }
        int CurrentlyPage { get; }
        int TotalPages { get; }
        string MinimumReleaseDate { get; }
        string MaximumReleaseDate { get; }
        bool Loading { get; }
        string HeaderSubTitle { get; }
        string FooterTitle { get; }

        void ExecuteNextPageRequest();
        IMovieItemViewModel ToMovieItemViewModel(Movie result);
        Task ItemSelected(IMovieItemViewModel movieItemViewModel);
    }
}