﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieDetailPage.xaml.cs" company="ArcTouch LLC">
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
//   Defines the MovieDetailPage.xaml type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using CodeChallenge.Models;
using CodeChallenge.ViewModels;
using Xamarin.Forms;

namespace CodeChallenge.Views
{
    public partial class MovieDetailPage : ContentPage
    {
        private MovieDetailPageViewModel movieDetailViewModel;
        public MovieDetailPage(Movie movie)
        {
            InitializeComponent();
            this.movieDetailViewModel = new MovieDetailPageViewModel(movie);
            BindingContext = this.movieDetailViewModel;
        }
    }
}