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
    public class FriendTest
    {
        [TestMethod]
        public void CreateFriend()
        {
            Friend friend = new Friend("paul");
            Assert.AreEqual("Paul", friend.Name);
        }

        [TestMethod]
        public void AddReadyDish()
        {
            Dish dish = new Dish("Pizza", 100);
            Friend friend = new Friend("paul");
            friend.Add_dish(dish);
            Assert.AreEqual("Pizza", friend.dishes[0].Name);
        }

        [TestMethod]
        public void AddNewDish()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("Pizza")
                .Returns("100");
            Input.myConsole = consoleMok.Object;
            Friend friend = new Friend("paul");
            friend.Add_dish();
            Assert.AreEqual("Pizza", friend.dishes[0].Name);
        }

        [TestMethod]
        public void TakeCost()
        {
            Dish dish = new Dish("Pizza", 100);
            Friend friend = new Friend("paul");
            friend.Add_dish(dish);
            Assert.AreEqual(100, friend.TakeCost());
        }

        [TestMethod]
        public void ShowDish()
        {
            Dish dish = new Dish();
            Friend friend = new Friend("paul");
            friend.Add_dish(dish);
            friend.Print_all_dishes();
            Assert.AreEqual("Unname", friend.dishes[0].Name);
        }

    }
}
