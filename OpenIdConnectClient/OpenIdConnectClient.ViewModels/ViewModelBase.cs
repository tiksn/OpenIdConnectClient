// Copyright (c) 2019 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Reactive;
using OpenIdConnectClient.Models;
using ReactiveUI;

namespace OpenIdConnectClient.ViewModels
{
    /// <summary>
    /// A base for all the different view models used throughout the application.
    /// </summary>
    public abstract class ViewModelBase : ReactiveObject, IRoutableViewModel, IActivatableViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="urlPathSegments">The title of the view model for routing purposes.</param>
        /// <param name="schedulers"></param>
        /// <param name="hostScreen">The screen used for routing purposes.</param>
        protected ViewModelBase(IEnumerable<string> urlPathSegments, ISchedulers schedulers, IScreen hostScreen)
        {
            if (urlPathSegments is null)
            {
                throw new ArgumentNullException(nameof(urlPathSegments));
            }

            if (urlPathSegments.IsEmpty())
            {
                throw new ArgumentOutOfRangeException(nameof(urlPathSegments));
            }


            if (schedulers is null)
            {
                throw new ArgumentNullException(nameof(schedulers));
            }

            UrlPathSegment = string.Join('/', urlPathSegments);
            HostScreen = hostScreen ?? throw new ArgumentNullException(nameof(hostScreen));

            ShowAlert = new Interaction<AlertViewModel, Unit>(schedulers.MainThreadScheduler);
            OpenBrowser = new Interaction<string, Unit>(schedulers.MainThreadScheduler);
        }

        /// <summary>
        /// Gets the current page path.
        /// </summary>
        public string UrlPathSegment { get; }

        /// <summary>
        /// Gets the screen used for routing operations.
        /// </summary>
        public IScreen HostScreen { get; }

        /// <summary>
        /// Gets the activator which contains context information for use in activation of the view model.
        /// </summary>
        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        /// <summary>
        /// Gets a interaction which will show an alert.
        /// </summary>
        public Interaction<AlertViewModel, Unit> ShowAlert { get; }

        /// <summary>
        /// Gets an interaction which will open a browser window.
        /// </summary>
        public Interaction<string, Unit> OpenBrowser { get; }
    }
}
