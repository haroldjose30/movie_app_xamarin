// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TmdbApi.cs" company="ArcTouch LLC">
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
//   Defines the TmdbApi type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Threading.Tasks;
using CodeChallenge.Common;
using CodeChallenge.Models;
using CodeChallenge.Models.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace CodeChallenge.Services
{
    //Singleton Pattern
    public class TmdbApi: IDisposable
    {

        private static ITmdbApi _Instance;

        private TmdbApi()
        {

        }

        //todo: Single responsability principle - Não é responsabilidade do Service saber de nada que não seja dele, apenas o getgenres, upcomingmovies e getmovie
        public static ITmdbApi GetApi()
        {

            if (_Instance != null)
                return _Instance;

            /*obsolete  Warning resolved
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
                };

                var refitSettings = new RefitSettings { JsonSerializerSettings = jsonSerializerSettings };
            */


            var refitSettings = new RefitSettings
            {
                ContentSerializer = new JsonContentSerializer(
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
                    }
            )
            };

            _Instance = RestService.For<ITmdbApi>(Constants.API_URL, refitSettings);
            return _Instance;
        }

        /// <summary>
        /// IDisposable Implementation Region
        /// reference from: https://docs.microsoft.com/pt-br/dotnet/api/system.idisposable?view=netframework-4.7.2
        /// </summary>
        #region IDisposable Implementation Region

       

        // Track whether Dispose has been called.
        private bool disposed = false;

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                   
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.

                //free singleton instance
                _Instance = null;


                // Note disposing has been done.
                disposed = true;

            }
        }

        #endregion

    }
}
