using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PokerTracker.Tests.DAL
{
    public static class AssertHelper
    {
        public static void Throws(Action callback)
        {
            try
            {
                callback();
                Assert.Fail("Should have thrown exception.");
            }
            catch
            { }
        }
    }
}
