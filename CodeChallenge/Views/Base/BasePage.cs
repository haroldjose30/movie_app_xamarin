// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasePage.cs" company="ArcTouch LLC">
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
//   Defines the BasePage type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using CodeChallenge.ViewModels.Base;
using Xamarin.Forms;

namespace CodeChallenge.Views.Base
{
    /// <summary>
    /// Base page.
    /// This code was based from Maratona Intermediaria Xamarin
    /// https://github.com/MonkeyNights/monkey-hub-app/blob/push-5/MonkeyHubApp/MonkeyHubApp/MonkeyHubApp/BasePage.cs
    /// </summary>
    public abstract class BasePage : ContentPage
    {
        public virtual IBaseViewModel ViewModel => BindingContext as IBaseViewModel;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //verify if viewmodel was created
            if (ViewModel == null) 
                return;

            //Title = ViewModel.Title;
            //ViewModel.PropertyChanged += TitlePropertyChanged;

            await ViewModel.OnAppearing();
        }


        protected override async void OnDisappearing()
        {
            base.OnAppearing();

            //verify if viewmodel was created
            if (ViewModel == null)
                return;

            //ViewModel.PropertyChanged -= TitlePropertyChanged;

            await ViewModel.OnAppearing();
        }
        /*
        private void TitlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ViewModel.Title)) return;

            Title = ViewModel.Title;
        }
        */
    }
}
