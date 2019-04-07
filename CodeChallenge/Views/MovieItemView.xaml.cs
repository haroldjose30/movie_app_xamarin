// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieItemView.xaml.cs" company="ArcTouch LLC">
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
//   Defines the MovieItemView type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using CodeChallenge.ViewModels;
using Xamarin.Forms;

namespace CodeChallenge.Views
{
    public partial class MovieItemView : ViewCell
    {

        Image image = null;

        public MovieItemView()
        {
            InitializeComponent();

            //todo: i don't know if this code is 100% ok, but de reference website return only one image to view
            image = this.FindByName<Image>("ImagePosterPath");
            //image = new Image();
            //View = image;
        }

        //Issue: Images may disappear upon scrolling on the Android ListView #9
        //References:   https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/listview/performance
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            //here to prevent showing old images occasionally
            image.Source = null;


            if (BindingContext is MovieItemViewModel movieItemViewModel)
            {
                image.Source = movieItemViewModel.PosterPath;
            }

        }
    }
}
