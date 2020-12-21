using System;
using System.Threading;

namespace TournamentsEnhanced
{
    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random _random;

        public static Random ThisThreadsRandom
        {
            get { return _random ?? (_random = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
}
