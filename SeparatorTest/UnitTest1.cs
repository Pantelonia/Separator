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
            Input.myConsole = consoleMok.Object;
            Menu menu = new Menu();
            Assert.AreEqual(0, menu.StartMenu());
        }
        [TestMethod]
        public void CreateNewGroup()
        {

            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("Gr")
                .Returns("Pl");
            Input.myConsole = consoleMok.Object;
            Menu menu = new Menu();
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
        [TestMethod]
        public void FindGroupTest()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("group")
                .Returns("paul")
                .Returns("1");
            Input.myConsole = consoleMok.Object;
            Menu menu = new Menu();
            menu.CreateGroup();
            menu.FindGroup();
            Group group;
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Group>("current_group");
                group = col.FindOne(x => x.Name.Equals("Group"));
            }
            Group group2 = new Group("Group");
            Assert.AreEqual(group2.Name, group.Name);
        }
        [TestMethod]
        public void FindInitGroupTest()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("4")
                .Returns("2")
                .Returns("group")
                .Returns("paul")
                .Returns("7")
                .Returns("5");
            Input.myConsole = consoleMok.Object;
            Menu menu = new Menu();
            menu.StartMenu();
            Group group;
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Group>("current_group");
                group = col.FindOne(x => x.Name.Equals("Group"));
            }
            Group group2 = new Group("Group");
            Assert.AreEqual(group2.Name, group.Name);
        }


    }
}
