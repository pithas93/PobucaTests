using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JPB_Tests
{
    [TestClass]
    public class Class1
    {
        [TestMethod]
        public void testmethod()
        {
            string a = "Ab";
            string b = "Bb";
            var x1 = String.CompareOrdinal(a, b);
            var x2 = String.CompareOrdinal(b, a);
            var x3 = String.CompareOrdinal(a, a);
            Thread.Sleep(100);
        }
    }
}

//Ab Bb -1
//Bb Ab 1
//Ab Ab 0