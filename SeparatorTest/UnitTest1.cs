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
                .Returns("group") // name of group
                .Returns("paul") // name of first friend
                .Returns("1");// find 1 group
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
                .Returns("4")// delete all group
                .Returns("2") //try find some group, but create new group because groupd  not vreated yet
                .Returns("group") // name of group
                .Returns("paul")// name of friend
                .Returns("7") // exit from main page
                .Returns("5"); // exit
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

        [TestMethod]
        public void TryInputErrorInt()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("7")//input outofarray
                .Returns("5");//input correct number and exit
            Input.myConsole = consoleMok.Object;
            Menu menu = new Menu();
            Assert.AreEqual(0, menu.StartMenu());
        }

        [TestMethod]
        public void DeleteSelectedGroup()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("4") //Delete all
                .Returns("1")//try find some group, but create new group because groupd  not vreated yet
                .Returns("groups") // name of group
                .Returns("paul")// name of friend
                .Returns("7") // exit from main page
                .Returns("3")// delete all group
                .Returns("1")// delete all group
                .Returns("7") // exit from main pag
                .Returns("5"); // exit
            Input.myConsole = consoleMok.Object;
            Menu menu = new Menu();
            menu.StartMenu();
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Group>("group");
                var group = col.FindOne(x => x.Name.Equals("Groups"));
                Assert.AreEqual(null, group);

            }
        }


    }
}
