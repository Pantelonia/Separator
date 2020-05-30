using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Separator;
using Separator.Menu;
using Moq;


namespace SeparatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var consoleMok = new Mock<MyConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
            .Returns("5"); // add new group          
            Assert.AreEqual(0, new Menu(consoleMok.Object));
        }
    }
}
