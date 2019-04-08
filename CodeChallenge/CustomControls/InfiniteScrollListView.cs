// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfiniteScrollListView.cs" company="ArcTouch LLC">
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
//   Defines the InfiniteScrollListView type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace CodeChallenge.CustomControls
{
    /// <summary>
    /// This is a ListView custom control to create a bindable property to be used with command and MVVM
    /// This code is based on Angelo Belchior - InffiniteScroll
    /// https://youtu.be/fPxWd5POn4E
    /// </summary>
    public class InfiniteScrollListView : ListView
    {
        ///obsolete  Warning resolved
        /// public static readonly BindableProperty LoadCommandProperty = BindableProperty.Create<InfiniteScrollListView,ICommand>(bp => bp.LoadCommand, default(ICommand));

        public static readonly BindableProperty LoadCommandProperty = BindableProperty.Create(nameof(LoadCommand), typeof(ICommand), typeof(InfiniteScrollListView));

        public ICommand LoadCommand
        {
            get { return (ICommand)this.GetValue(LoadCommandProperty); }
            set { this.SetValue(LoadCommandProperty, value); }
        }

        public InfiniteScrollListView(ListViewCachingStrategy strategy) : base(strategy)
        {
            //attach ItemAppearing event to execute LoadCommand
            this.ItemAppearing += (object sender, ItemVisibilityEventArgs e) =>
            {
                var items = this.ItemsSource as IList;
                if (items != null && items.Count > 0 && e.Item == items[items.Count - 1])
                {
                    //verify if command can be executed
                    if (this.LoadCommand != null && this.LoadCommand.CanExecute(null))
                    {
                        //execute command
                        this.LoadCommand.Execute(null);
                    }
                }
            };
        }
    }
}
