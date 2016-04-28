using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker.Tests.BLL
{
    public static class AssertHelper
    {
        public static void Throws(Action callback)
        {
            try
            {
                callback();
                Assert.Fail("Call should have thrown an exception.");
            }
            catch { }
        }
    }
}
