// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieSearchPageViewModel.cs" company="ArcTouch LLC">
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
//   Defines the MovieSearchPageViewModel type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CodeChallenge.Models;
using CodeChallenge.Models.Responses;
using CodeChallenge.Services;
using CodeChallenge.ViewModels.Base;
using Xamarin.Forms;

namespace CodeChallenge.ViewModels
{
    public class MovieSearchPageViewModel : BaseViewModel, IMovieSearchPageViewModel
    {
        public MovieSearchPageViewModel(string query)
        {
            this.SearchText = query;
            this.movieService = DependencyService.Get<IMovieService>();
            this._movies = new ObservableCollection<IMovieItemViewModel>();
        }

        #region Properties region

        private readonly IMovieService movieService;
        private ObservableCollection<IMovieItemViewModel> _movies;

        public ObservableCollection<IMovieItemViewModel> Movies
        {
            get => this._movies;
            set => SetProperty(ref this._movies, value);
        }




        string _SearchText = string.Empty;
        public string SearchText
        {
            get => this._SearchText;
            set
            {
                SetProperty(ref this._SearchText, value);
            }
        }

        /// <summary>
        /// Page Counter
        /// </summary>
        int _CurrentlyPage = 0;
        public int CurrentlyPage
        {
            get => this._CurrentlyPage;
            set => SetProperty(ref this._CurrentlyPage, value);
        }

        /// <summary>
        /// total pages of movies
        /// </summary>
        int _TotalPages = 0;
        public int TotalPages
        {
            get => this._TotalPages;
            set
            {
                SetProperty(ref this._TotalPages, value);
                //notify to update view to FooterTitle property too
                OnPropertyChanged(FooterTitlePropertyName);
            }

        }


        /// <summary>
        /// to show indicator on the screen during de loading data
        /// </summary>
        bool _Loading = false;
        public bool Loading
        {
            get => this._Loading;
            set
            {
                SetProperty(ref this._Loading, value);
            }
        }


        /// <summary>
        /// Gets the footer title.
        /// </summary>
        /// <value>The footer title.</value>
        public string FooterTitle
        {
            get => $"Pages loaded: {CurrentlyPage.ToString()}/{TotalPages.ToString()}";
        }

        private readonly string FooterTitlePropertyName = "FooterTitle";



        #endregion


        #region Methods/Commands Region

        

        public override Task OnAppearing()
        {
            base.OnAppearing();

            //verify if movies was loaded
            if (Movies.Count == 0)
            {
                //execute request to get first page
                ExecuteNextPageRequest();
            }

            return Task.FromResult(0);

        }


        /// <summary>
        /// Gets the next page of movies from backend.
        /// </summary>
        public async void ExecuteNextPageRequest()
        {

            //verify if all pages was loaded
            if (CurrentlyPage > 0 && CurrentlyPage == TotalPages)
                return;

            //get next page from Search
            if (await ExecuteSearchMoviesRequest(SearchText, CurrentlyPage + 1))
                //increase page counter
                CurrentlyPage++;
        }

        /// <summary>
        /// Load movies from backend using movieservices
        /// </summary>
        /// <param name="page">pass number page is loaded</param>
        private async Task<Boolean> ExecuteSearchMoviesRequest(string query, int page)
        {
            //todo: network improvements: verify if is connected on the internet
            //todo: network improvements: cache data and try retreave from cache first

            //if doing exit
            if (Loading) return false;

            try
            {

                Loading = true;

                //call back end
                SearchMovieResponse searchMovieResponse = await this.movieService.SearchMovie(query, page);


                //verify if exists new movie
                if (searchMovieResponse != null && searchMovieResponse.Results.Count > 0)
                {
                    //update properties on view model who needed be updates on view
                    TotalPages = searchMovieResponse.TotalPages;

                    //add movies on the list
                    foreach (var movie in searchMovieResponse.Results)
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
        /// Load Movies from Backend, try load next page
        /// </summary>
        /// <value>The load command.</value>
        public ICommand LoadCommand { get => new Command(() => ExecuteNextPageRequest()); }



        #endregion





    }
}
