using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PokerTracker.Services;

namespace PokerTracker.Tests
{
    public static class AssertHelper
    {
        public static void Throws(Action callback)
        {
            try
            {
                callback();
                Assert.Fail("Should have thrown an exception.");
            }
            catch
            { }
        }
    }
}
