// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Movie.cs" company="ArcTouch LLC">
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
//   Defines the Movie type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace CodeChallenge.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string BackdropPath { get; set; }

        public List<int> GenreIds { get; set; }

        public List<Genre> Genres { get; set; }

        public string Overview { get; set; }

        public string PosterPath { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public string Title { get; set; }
    }
}
