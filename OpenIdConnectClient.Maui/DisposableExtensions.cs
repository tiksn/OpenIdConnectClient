using System;

namespace OpenIdConnectClient.Maui
{
    public static class DisposableExtensions
    {
        public static T DisposeWith<T>(this T item, ReactiveUI.Primitives.Disposables.MultipleDisposable disposables) where T : IDisposable
        {
            disposables.Add(item);
            return item;
        }
    }
}
