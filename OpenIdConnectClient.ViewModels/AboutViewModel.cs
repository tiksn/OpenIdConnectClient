// Copyright (c) 2019 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using OpenIdConnectClient.Models;
using ReactiveUI;
using System.Reactive;
using static LanguageExt.Prelude;

namespace OpenIdConnectClient.ViewModels
{
    /// <summary>
    /// A view model which shows information about the application.
    /// </summary>
    public class AboutViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewModel"/> class.
        /// </summary>
        /// <param name="schedulers">Known Schedulers</param>
        /// <param name="hostScreen">The main screen for routing.</param>
        public AboutViewModel(
                ISchedulers schedulers,
                IScreen hostScreen)
            : base(Seq1("About"), schedulers, hostScreen)
        {
            ShowIconCredits = ReactiveCommand.CreateFromObservable<string, Unit>(url => OpenBrowser.Handle(url));
            ShowIconCredits.Subscribe();
        }

        /// <summary>
        /// Gets a command which will show the icon credits.
        /// </summary>
        public ReactiveCommand<string, Unit> ShowIconCredits
        {
            get;
        }
    }
}