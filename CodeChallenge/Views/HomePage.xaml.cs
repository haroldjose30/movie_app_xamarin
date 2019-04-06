// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="ArcTouch LLC">
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
//   Defines the MainPage type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using CodeChallenge.Models;
using CodeChallenge.Services;
using CodeChallenge.ViewModels;
using Xamarin.Forms;

namespace CodeChallenge.Views
{
    public partial class HomePage : ContentPage
    {
        private HomePageViewModel homePageViewModel;
        public HomePage()
        {
            InitializeComponent();
            this.homePageViewModel = new HomePageViewModel(new MovieService());
            BindingContext = this.homePageViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is HomePageViewModel viewModel)
            {
                await viewModel.OnAppearing();
            }
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is HomePageViewModel viewModel)
            {
                await viewModel.OnDisappearing();
            }
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            //todo: convert to MVVM - show MovieDetailPage
            if (BindingContext is HomePageViewModel viewModel)
            {

                if (e.SelectedItem is MovieItemViewModel movieItemViewModel)
                {
                    Navigation.PushAsync(new MovieDetailPage(movieItemViewModel.movie));
                }
            }
        }

        void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            //todo: convert to MVVM - paginations

            if (BindingContext is HomePageViewModel viewModel)
            {

                //Obs: Solution based from Angelo Belchior Video and montemagno website
                //Do new request to get new page
                if (sender is ListView listview)
                {
                    var movies = listview.ItemsSource as IList;
                    if (movies != null && e.Item == movies[movies.Count - 1])
                    {
                        //load next page from movie
                        viewModel.GetNextPageOfMovies();
                    }
                }
            }
        }
    }
}
