using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Separator;

namespace UnitTestSeparator
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckNameConstructorTest()
        {
            Friend friend = new Friend("paul");
            Assert.AreEqual("Paul", friend.Name);
        }
       
    }
}
