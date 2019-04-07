// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieItemViewModel.cs" company="ArcTouch LLC">
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
//   Defines the MovieItemViewModel type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Linq;
using CodeChallenge.Models;
using CodeChallenge.ViewModels.Base;
using Xamarin.Forms;

namespace CodeChallenge.ViewModels
{
    public class MovieItemViewModel : BaseViewModel, IMovieItemViewModel
    {
        public MovieItemViewModel(Movie movie)
        {
            this._movie = movie;
            Title = movie.Title;
            PosterPath = Utils.MovieImageUrlBuilder.BuildPosterUrl(movie.PosterPath);
            ReleaseDate = movie.ReleaseDate;
            Genres = string.Join(", ", movie.GenreIds.Select(m => App.Genres?.First(g => g.Id == m)?.Name));
        }


        #region Properties Region

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private Movie _movie;
        public Movie movie { get => _movie; }
        private string posterPath;
        public string PosterPath { get => this.posterPath; set => SetProperty(ref this.posterPath, value); }
        public DateTimeOffset ReleaseDate { get; set; }
        public string Genres { get; set; }


        #endregion


        #region Methods/Commands Region


        /// <summary>
        /// When an item is tapped 
        /// </summary>
        /// <value>The tap command.</value>
        public Command TapCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //call view locator to  Show MovieDetailPage
                    await PushAsync<MovieDetailPageViewModel>(movie);
                });
            }
        }


        #endregion



    }
}
