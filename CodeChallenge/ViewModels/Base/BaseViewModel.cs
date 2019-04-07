// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="ArcTouch LLC">
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
//   Defines the BaseViewModel type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CodeChallenge.Services;
using Xamarin.Forms;
namespace CodeChallenge.ViewModels.Base
{
    /// <summary>
    /// Base view model.
    /// This code was based on Maratona Intermediaria Xamarin
    /// https://github.com/MonkeyNights/monkey-hub-app/blob/push-5/MonkeyHubApp/MonkeyHubApp/MonkeyHubApp/ViewModels/BaseViewModel.cs
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged, IBaseViewModel
    {

        #region Properties Region

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region ViewModel Region


        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        #endregion


        #region Methods/Commands Region



        /// <summary>
        /// View locator
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="args">Arguments.</param>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public async Task PushAsync<TViewModel>(params object[] args) where TViewModel : BaseViewModel
        {
            var viewModelTypeName = "View Model";
            try
            {
                var viewModelType = typeof(TViewModel);
                viewModelTypeName = viewModelType.Name;
                var viewModelWordLength = "ViewModel".Length;
                var viewTypeName = $"CodeChallenge.Views.{viewModelTypeName.Substring(0, viewModelTypeName.Length - viewModelWordLength)}";
                var viewType = Type.GetType(viewTypeName);

                //exit from method if type don't locate
                if (viewType == null) return;

                //try locate the page
                var page = Activator.CreateInstance(viewType) as Page;

                //exit from method if Page don't locate
                if (page == null) return;

                //if on contructor expected service to connect to backend  try to get service from dependence service
                if (viewModelType.GetTypeInfo().DeclaredConstructors.Any(c => c.GetParameters().Any(p => p.ParameterType == typeof(IMovieService))))
                {
                    var argsList = args.ToList();
                    var movieService = DependencyService.Get<IMovieService>();
                    argsList.Insert(0, movieService);
                    args = argsList.ToArray();
                }

                //try locate the ViewModel
                var viewModel = Activator.CreateInstance(viewModelType, args);


                //bind page with view model
                if (viewModel != null)
                    page.BindingContext = viewModel;

                //Show page
                await Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                //show error to user if Something is wrong
                await DisplayAlert("Something is wrong happened!", 
                                    $"The Page for {viewModelTypeName} can not be located!" + Environment.NewLine +
                                    $"Error message: {ex.Message}",
                                    "OK"
                                    );
            }

          
        }

        public virtual Task OnAppearing()
        {
            return Task.FromResult(0);
        }

        public virtual Task OnDisappearing()
        {
            return Task.FromResult(0);
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task DisplayAlert(string title, string message, string accept, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        #endregion
    }
}
