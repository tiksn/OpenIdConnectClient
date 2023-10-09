﻿using ReactiveUI;

namespace OpenIdConnectClient.ViewModels;

public abstract class ViewModel : ReactiveObject
{
    protected readonly IMessageBus messageBus;

    protected ViewModel(IMessageBus messageBus)
    {
        this.messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
    }
}