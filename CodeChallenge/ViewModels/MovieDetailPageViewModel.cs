// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieDetailViewModel.cs" company="ArcTouch LLC">
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
//   Defines the MovieDetailViewModel type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Linq;
using CodeChallenge.Models;
using CodeChallenge.ViewModels.Base;

namespace CodeChallenge.ViewModels
{

    public class MovieDetailPageViewModel : BaseViewModel
    {
        public MovieDetailPageViewModel(Movie movie)
        {
            Title = movie.Title;
            Overview = movie.Overview;
            PosterPath = Utils.MovieImageUrlBuilder.BuildPosterUrl(movie.PosterPath);
            BackdropPath = Utils.MovieImageUrlBuilder.BuildBackdropUrl(movie.BackdropPath);
            ReleaseDate = movie.ReleaseDate;
            Genres = string.Join(", ", movie.GenreIds.Select(m => App.Genres?.First(g => g.Id == m)?.Name));
        }

        #region Properties Region

        private string posterPath;
        private string backdropPath;
        public string Overview { get; set; }
        public string PosterPath { get => this.posterPath; set => SetProperty(ref this.posterPath, value); }
        public string BackdropPath { get => this.backdropPath; set => SetProperty(ref this.backdropPath, value); }
        public DateTimeOffset ReleaseDate { get; set; }
        public string Genres { get; set; }

        #endregion



    }
}
