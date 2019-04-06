// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Result.cs" company="ArcTouch LLC">
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
//   Defines the Result type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace CodeChallenge.Models.Responses
{
    public class Result
    {
        public int VoteCount { get; set; }

        public int Id { get; set; }

        public bool Video { get; set; }

        public double VoteAverage { get; set; }

        public string Title { get; set; }

        public double Popularity { get; set; }

        public string PosterPath { get; set; }

        public string OriginalLanguage { get; set; }

        public string OriginalTitle { get; set; }

        public List<int> GenreIds { get; set; }

        public string BackdropPath { get; set; }

        public bool Adult { get; set; }

        public string Overview { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }
    }
}
