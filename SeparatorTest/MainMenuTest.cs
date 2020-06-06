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
    public class MainMenuTest
    {
        [TestMethod]
        public void AddNewFriendTest()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("1")
                .Returns("Lea")
                .Returns("8");
            Input.myConsole = consoleMok.Object;
            Group group = new Group("Pokorili");
            group.AddNewFriend("Blabla");
            Main_page main = new Main_page(group);
            var output  = main.Start();
            Assert.AreEqual(0, output);
            Assert.AreEqual("Lea", main.Group.Friends[1].Name);

        }

        [TestMethod]
        public void DeleteFriendTest()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("5")
                .Returns("4")
                .Returns("1")
                .Returns("5")
                .Returns("8");
            Input.myConsole = consoleMok.Object;
            Group group = new Group("Pokorili");
            group.AddNewFriend("Blabla");
            group.AddNewFriend("Lea");
            Main_page main = new Main_page(group);
            Friend friend = group.Friends[0];
            var output = main.Start();
            Assert.AreEqual(0, output);
            Assert.AreEqual(false, main.Group.Friends.Contains(friend));

        }

        [TestMethod]
        public void AddDishTest()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("2")
                .Returns("1")
                .Returns("1")
                .Returns("Tea")
                .Returns("45")
                .Returns("2")
                .Returns("2")
                .Returns("Pizza")
                .Returns("500")
                .Returns("8");
            Input.myConsole = consoleMok.Object;
            Group group = new Group("Pokorili");
            group.AddNewFriend("Blabla");
            group.AddNewFriend("Lea");
            Main_page main = new Main_page(group);
            var output = main.Start();
            Assert.AreEqual(0, output);
            Assert.AreEqual("Tea", main.Group.Friends[0].dishes[0].Name);

        }
        [TestMethod]
        public void SeparateBillTest()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("2")
                .Returns("1")
                .Returns("1")
                .Returns("Tea")
                .Returns("45")
                .Returns("2")
                .Returns("2")
                .Returns("Pizza")
                .Returns("500")
                .Returns("6")
                .Returns("7")
                .Returns("8");
            Input.myConsole = consoleMok.Object;
            Group group = new Group("Pokorili");
            group.AddNewFriend("Blabla");
            group.AddNewFriend("Lea");
            Main_page main = new Main_page(group);
            var output = main.Start();
            Assert.AreEqual(0, output);
            Assert.AreEqual(545, main.Group.Total_cost);
            Assert.AreEqual(295, main.Group.Friends[0].TakeCost());

        }
        [TestMethod]
        public void ShowAllDish()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("2")
                .Returns("1")
                .Returns("1")
                .Returns("Tea")
                .Returns("45")
                .Returns("2")
                .Returns("2")
                .Returns("Pizza")
                .Returns("500")
                .Returns("3")
                .Returns("8");
            Input.myConsole = consoleMok.Object;
            Group group = new Group("Pokorili");
            group.AddNewFriend("Blabla");
            group.AddNewFriend("Lea");
            Main_page main = new Main_page(group);
            var output = main.Start();
            Assert.AreEqual(0, output);
        }




    }
}
