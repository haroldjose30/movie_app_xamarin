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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CodeChallenge.Models;
using CodeChallenge.Models.Responses;
using CodeChallenge.Services;

namespace CodeChallenge.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CodeChallenge.ViewModels.HomePageViewModel"/> class.
        /// </summary>
        /// <param name="movieService">Movie service.</param>
        public HomePageViewModel(MovieService movieService)
        {
            this.movieService = movieService;
            this.movies = new ObservableCollection<MovieItemViewModel>();
        }

        #region Properties region

        private readonly MovieService movieService;
        private ObservableCollection<MovieItemViewModel> movies;

        public event PropertyChangedEventHandler PropertyChanged;


        public ObservableCollection<MovieItemViewModel> Movies
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


        DateTimeOffset _MinimumReleaseDate;
        public DateTimeOffset MinimumReleaseDate
        {
            get => this._MinimumReleaseDate;
            private set
            {
                SetProperty(ref this._MinimumReleaseDate, value);
                //notify to update view to HeaderSubTitle property too
                OnPropertyChanged(HeaderSubTitlePropertyName);
            }
        }

        DateTimeOffset _MaximumReleaseDate;
        public DateTimeOffset MaximumReleaseDate
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
            //todo: get date format from cultural/region device
            get => $"From {MinimumReleaseDate.ToString("dd/MM")} to {MaximumReleaseDate.ToString("dd/MM")}";
        }
        private readonly string HeaderSubTitlePropertyName = "HeaderSubTitle";


        /// <summary>
        /// Gets the footer title.
        /// </summary>
        /// <value>The footer title.</value>
        public string FooterTitle
        {
            get => $"Pages: {CurrentlyPage.ToString()}/{TotalPages.ToString()}";
        }

        private readonly string FooterTitlePropertyName = "FooterTitle";




        #endregion


        #region Methods Region

        public async Task OnAppearing()
        {

            //verify if movies was loaded
            if (movies.Count==0)
            {
                //start CurrentlyPage counter
                CurrentlyPage = 0;
                GetNextPageOfMovies();
            }

        }

        /// <summary>
        /// Gets the next page of movies from backend.
        /// </summary>
        public void GetNextPageOfMovies()
        {
            //increase page
            CurrentlyPage++;

            //get next page
            GetMoviesFromPageAsync(CurrentlyPage);
        }

        /// <summary>
        /// Load movies from backend using movieservices
        /// </summary>
        /// <param name="currentlyPage">pass number page is loaded</param>
        private async void GetMoviesFromPageAsync(int currentlyPage)
        {
            //todo: network improvements: verify if is connected on the internet
            //todo: network improvements: cache data and try retreave from cache first


            try
            {

                Loading = true;

                //call back end
                UpcomingMoviesResponse upcomingMoviesResponse = await this.movieService.UpcomingMovies(currentlyPage);

                //update properties on view model who needed be updates on view
                TotalPages = upcomingMoviesResponse.TotalPages;
                MinimumReleaseDate = upcomingMoviesResponse.Dates.Minimum;
                MaximumReleaseDate = upcomingMoviesResponse.Dates.Maximum;

                //add movies on the list
                foreach (var movie in upcomingMoviesResponse.Results)
                {
                    Movies.Add(ToMovieItemViewModel(movie));
                }
            }
            finally
            {
                Loading = false;
            }

        }

        public Task OnDisappearing() => Task.CompletedTask;

        public MovieItemViewModel ToMovieItemViewModel(Movie result) => new MovieItemViewModel(result);


        #endregion


        #region ViewModel Region


        private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion




    }
}
