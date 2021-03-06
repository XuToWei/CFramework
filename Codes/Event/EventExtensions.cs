using System;
using GameFramework.Event;

namespace Hotfix
{
    public static class EventExtensions
    {
        public static void FireReleaseNow(EventHandler<HotfixGameEventArgs> handler,object sender,HotfixGameEventArgs eventArgs)
        {
            handler.Invoke(sender,eventArgs);
            ReferencePool.Release(eventArgs);
        }
    }
}