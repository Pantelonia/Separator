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
    public class MenuUnitTest
    {
        [TestMethod]

        public void TestMethod1()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("4")
                .Returns("5");
            Menu menu = new Menu(consoleMok.Object);
            Assert.AreEqual(0, menu.StartMenu());
        }
    }
}
