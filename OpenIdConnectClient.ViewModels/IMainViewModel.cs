﻿namespace OpenIdConnectClient.ViewModels;

public interface IMainViewModel
{
    IClientOptionsViewModel ClientOptions { get; }

    IActionsViewModel Actions { get; }

    IResultsViewModel Results { get; }
}