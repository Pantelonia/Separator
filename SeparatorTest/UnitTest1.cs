using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Separator;
using Separator.Menu;
using Moq;
using LiteDB;



namespace SeparatorTest
{
    [TestClass]
    public class MenuUnitTest
    {
        [TestMethod]
        public void DeleteAllGroup()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("4")
                .Returns("5");
            Menu menu = new Menu(consoleMok.Object);
            Assert.AreEqual(0, menu.StartMenu());
        }
        [TestMethod]
        public void CreateNewGroup()
        {

            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("Gr")
                .Returns("Pl");
            Menu menu = new Menu(consoleMok.Object);
            menu.CreateGroup();
            Group group;
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Group>("group");
                group = col.FindOne(x => x.Name.Equals("Gr"));
            }
            Group group2 = new Group("Gr");
            Assert.AreEqual(group2.Name, group.Name);
        }

       
    }
}
