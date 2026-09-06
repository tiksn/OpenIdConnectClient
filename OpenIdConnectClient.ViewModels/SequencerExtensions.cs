using System;
using ReactiveUI.Primitives.Concurrency;

namespace OpenIdConnectClient.ViewModels
{
    public static class SequencerExtensions
    {
        public static IObservable<T> ObserveOn<T>(this IObservable<T> source, ISequencer sequencer) where T : notnull
        {
            return ReactiveUI.Primitives.LinqExtensions.ObserveOn(source, sequencer);
        }
    }
}
