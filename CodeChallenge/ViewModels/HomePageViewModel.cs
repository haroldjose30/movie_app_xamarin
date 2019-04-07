// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomePageViewModel.cs" company="ArcTouch LLC">
//   Copyright 2019 ArcTouch LLC.
//   All rights reserved.
//
//   This file, its contents, concepts, methods, behavior, and operation
//   (collectively the "Software") are protected by trade secret, patent,
//   and copyright laws. The use of the Software is governed by a license
//   agreement. Disclosure of the Software to third parties, in any form,
//   in whole or in part, is expressly prohibited except as authorized by
//   the license agreement.
// </copyright>
// <summary>
//   Defines the HomePageViewModel type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CodeChallenge.Models;
using CodeChallenge.Services;
using CodeChallenge.ViewModels.Base;
using Xamarin.Forms;

namespace CodeChallenge.ViewModels
{
    public class HomePageViewModel : BaseViewModel, IHomePageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CodeChallenge.ViewModels.HomePageViewModel"/> class.
        /// </summary>
        /// <param name="movieService">Movie service.</param>
        public HomePageViewModel(IMovieService movieService)
        {
            this.movieService = movieService;
            this.movies = new ObservableCollection<IMovieItemViewModel>();
        }

        #region Properties region

        private readonly IMovieService movieService;
        private ObservableCollection<IMovieItemViewModel> movies;

        public ObservableCollection<IMovieItemViewModel> Movies
        {
            get => this.movies;
            set => SetProperty(ref this.movies, value);
        }


        /// <summary>
        /// Page Counter
        /// </summary>
        int _CurrentlyPage = 0;
        public int CurrentlyPage
        {
            get => this._CurrentlyPage;
            private set
            {
                SetProperty(ref this._CurrentlyPage, value);
            }
        }

        /// <summary>
        /// total pages of movies
        /// </summary>
        int _TotalPages = 0;
        public int TotalPages
        {
            get => this._TotalPages;
            private set
            {
                SetProperty(ref this._TotalPages, value);
                //notify to update view to FooterTitle property too
                OnPropertyChanged(FooterTitlePropertyName);
            }
        }


        DateTimeOffset? _MinimumReleaseDate;
        public DateTimeOffset? MinimumReleaseDate
        {
            get => this._MinimumReleaseDate;
            private set
            {
                SetProperty(ref this._MinimumReleaseDate, value);
                //notify to update view to HeaderSubTitle property too
                OnPropertyChanged(HeaderSubTitlePropertyName);
            }
        }

        DateTimeOffset? _MaximumReleaseDate;
        public DateTimeOffset? MaximumReleaseDate
        {
            get => this._MaximumReleaseDate;
            private set
            {
                SetProperty(ref this._MaximumReleaseDate, value);
                //notify to update view to HeaderSubTitle property too
                OnPropertyChanged(HeaderSubTitlePropertyName);
            }
        }


        /// <summary>
        /// to show indicator on the screen during de loading data
        /// </summary>
        bool _Loading = false;
        public bool Loading
        {
            get => this._Loading;
            private set
            {
                SetProperty(ref this._Loading, value);
            }
        }


        public string HeaderSubTitle
        {
            get
            {
                if (MinimumReleaseDate == null || MaximumReleaseDate == null)
                    return "";
                else //todo: get date format from cultural/region device ou a Humanize style
                    return $"From {MinimumReleaseDate?.ToString("dd/MM")} to {MaximumReleaseDate?.ToString("dd/MM")}";

            }
        }
        private readonly string HeaderSubTitlePropertyName = "HeaderSubTitle";


        /// <summary>
        /// Gets the footer title.
        /// </summary>
        /// <value>The footer title.</value>
        public string FooterTitle
        {
            get => $"Page loaded: {CurrentlyPage.ToString()}/{TotalPages.ToString()}";
        }

        private readonly string FooterTitlePropertyName = "FooterTitle";




        #endregion


        #region Methods/Commands Region

        public override Task OnAppearing()
        {
            base.OnAppearing();

            //verify if movies was loaded
            if (movies.Count == 0)
            {
                //start CurrentlyPage counter
                CurrentlyPage = 0;
                GetNextPageAsync();
            }

            return Task.FromResult(0);

        }


        /// <summary>
        /// Gets the next page of movies from backend.
        /// </summary>
        public async void GetNextPageAsync()
        {

            //verify if all pages was loaded
            if (CurrentlyPage > 0 && CurrentlyPage == TotalPages)
                return;

            //get next page
            if (await GetUpcomingMovies(CurrentlyPage + 1))
                //increase page counter
                CurrentlyPage++;
        }

        /// <summary>
        /// Load movies from backend using movieservices
        /// </summary>
        /// <param name="page">pass number page is loaded</param>
        private async Task<Boolean> GetUpcomingMovies(int page)
        {
            //todo: network improvements: verify if is connected on the internet
            //todo: network improvements: cache data and try retreave from cache first

            //if doing exit
            if (Loading) return false;

            try
            {

                Loading = true;

                //call back end
                var upcomingMoviesResponse = await this.movieService.UpcomingMovies(page);


                //verify if exists new movie
                if (upcomingMoviesResponse != null && upcomingMoviesResponse.Results.Count > 0)
                {
                    //update properties on view model who needed be updates on view
                    TotalPages = upcomingMoviesResponse.TotalPages;
                    MinimumReleaseDate = upcomingMoviesResponse.Dates.Minimum;
                    MaximumReleaseDate = upcomingMoviesResponse.Dates.Maximum;

                    //add movies on the list
                    foreach (var movie in upcomingMoviesResponse.Results)
                    {
                        Movies.Add(ToMovieItemViewModel(movie));
                    }

                    return true;
                }


            }
            finally
            {
                Loading = false;
            }

            return false;

        }

        public IMovieItemViewModel ToMovieItemViewModel(Movie result)
        {
            return new MovieItemViewModel(result); 
        }

        public async Task ItemSelected(IMovieItemViewModel movieItemViewModel)
        {
            //Call ViewLocator
            await PushAsync<MovieDetailPageViewModel>(movieItemViewModel.movie);
        }

        /// <summary>
        /// Load Movies from Backend
        /// </summary>
        /// <value>The load command.</value>
        public ICommand LoadCommand
        {
            get
            {
                //try load next page
                return new Command(() => GetNextPageAsync());
            }
        }


        #endregion





    }
}
