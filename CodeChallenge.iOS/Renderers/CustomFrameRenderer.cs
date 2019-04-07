﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomFrameRenderer.cs" company="ArcTouch LLC">
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
//   Defines the CustomFrameRenderer type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------



using System.ComponentModel;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(CodeChallenge.iOS.Renderers.CustomFrameRenderer))]
namespace CodeChallenge.iOS.Renderers
{
    public class CustomFrameRenderer: FrameRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null && e.NewElement.HasShadow)
            {
                UpdateElevation();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "ShadowRadius")
            {
                UpdateElevation();
            }
        }



        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            UpdateElevation();
        }

        private void UpdateElevation()
        {
            //this code was base on below website reference
            //reference: https://alexdunn.org/2017/05/30/xamarin-tips-adding-dynamic-elevation-to-your-xamarin-forms-buttons/

            Layer.ShadowRadius = 5.0f;
            Layer.ShadowColor = UIColor.Gray.CGColor;
            Layer.ShadowOffset = new CGSize(2, 2);
            Layer.ShadowOpacity = 0.80f;
            Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            Layer.MasksToBounds = false;
        }


    }
}
