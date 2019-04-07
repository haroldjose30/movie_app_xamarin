// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="ArcTouch LLC">
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
//   Defines the App type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using CodeChallenge.Models;
using CodeChallenge.Views;
using CodeChallenge.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CodeChallenge.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CodeChallenge
{
    public partial class App : Application
    {
        public static List<Genre> Genres { get; private set; }

        public App()
        {
            InitializeComponent();


            //todo: change to view locations to improve this code
            var homePage = new HomePage();
            //create  Service
            var movieService = DependencyService.Get<IMovieService>();
            var homePageViewModel = new HomePageViewModel(movieService);
            homePage.BindingContext = homePageViewModel;
            MainPage = new NavigationPage(homePage);
        }

        protected override async void OnStart()
        {
            var genreResponse = await new MovieService().GetGenres();
            Genres = genreResponse.Genres;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
