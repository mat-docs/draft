// <copyright file="CancellationTokenExtensions.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TCPRecorder.Client.Extensions
{
    public static class CancellationTokenExtensions
    {
        public static CancellationTokenAwaiter GetAwaiter(this CancellationToken cancellationToken)
        {
            return new CancellationTokenAwaiter(cancellationToken);
        }

        public readonly struct CancellationTokenAwaiter : INotifyCompletion
        {
            private readonly CancellationToken cancellationToken;

            public CancellationTokenAwaiter(CancellationToken cancellationToken)
            {
                this.cancellationToken = cancellationToken;
            }

            public bool IsCompleted => this.cancellationToken.IsCancellationRequested;

            public void OnCompleted(Action continuation) => this.cancellationToken.Register(continuation);

            public void GetResult() => this.cancellationToken.WaitHandle.WaitOne();
        }
    }
}