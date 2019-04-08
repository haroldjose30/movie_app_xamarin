// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieService.cs" company="ArcTouch LLC">
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
//   Defines the MovieService type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------


using System.Collections.Generic;
using System.Threading.Tasks;
using CodeChallenge.Common;
using CodeChallenge.Models;
using CodeChallenge.Models.Responses;
using Xamarin.Forms;

[assembly: Dependency(typeof(CodeChallenge.Services.MovieService))]
namespace CodeChallenge.Services
{
    public class MovieService : IMovieService
    {

        //todo: To implementar SOLID aproach  - Single Responsability  is better segregate in another class the cache control
        //List of Genres for caching data in memory
        private static List<Genre> GenresCached { get; set; }


        /// <summary>
        /// return if Genres was cached
        /// </summary>
        /// <returns><c>true</c>, if is cached was genresed, <c>false</c> otherwise.</returns>
        private bool GenresIsCached() => (GenresCached != null && GenresCached.Count > 0);


        /// <summary>
        /// Get the list of official genres for movies.
        /// ref: https://developers.themoviedb.org/3/genres/get-movie-list
        /// </summary>
        /// <returns>The genres.</returns>
        public async Task<List<Genre>> GetGenres()
        {
            //return cached data to improve performance
            if (GenresIsCached())
                return GenresCached;

            //Make request to get data
            GenreResponse genreResponse = await TmdbApi.GetApi().GetGenres(Constants.API_KEY, Constants.DEFAULT_LANGUAGE);
            GenresCached = genreResponse.Genres;

            return GenresCached;
        }

        /// <summary>
        /// Upcomings the movies.
        /// Get a list of upcoming movies in theatres.This is a release type query that looks for all movies that have a release type of 2 or 3 within the specified date range.
        /// You can optionally specify a region prameter which will narrow the search to only look for theatrical release dates within the specified country.
        /// ref: https://developers.themoviedb.org/3/movies/get-upcoming
        /// </summary>
        public async Task<UpcomingMoviesResponse> UpcomingMovies(int page)
        {
            //try GetGenres before get movies
            await GetGenres();

            var moviesResponse = await TmdbApi.GetApi().UpcomingMovies(Constants.API_KEY, Constants.DEFAULT_LANGUAGE, page, Constants.DEFAULT_REGION);
            return moviesResponse;
        }



        /// <summary>
        /// Get the primary information about a movie.
        /// ref: https://developers.themoviedb.org/3/movies/get-movie-details
        /// </summary>
        public async Task<Movie> GetMovie(int movieId)
        {
            //try GetGenres before get movies
            await GetGenres();

            var moviesResponse = await TmdbApi.GetApi().GetMovie(Constants.API_KEY, Constants.DEFAULT_LANGUAGE, movieId);
            return moviesResponse;
        }

        /// <summary>
        /// Search for movies.
        /// ref: https://developers.themoviedb.org/3/search/search-movies 
        /// </summary>
        public async Task<SearchMovieResponse> SearchMovie(string query, int page)
        {
            //try GetGenres before get movies
            await GetGenres();

            var searchMovieResponse = await TmdbApi.GetApi().SearchMovie(Constants.API_KEY, Constants.DEFAULT_LANGUAGE, query, page);
            return searchMovieResponse;
        }

        public List<Genre> GetGenresCached()
        {
            return GenresCached;
        }
    }
}