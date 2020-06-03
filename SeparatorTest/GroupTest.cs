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
    public class GroupTest
    {
        [TestMethod]
        public void CreateGroup()
        {
            Group group = new Group();
            Assert.AreEqual("Unnamed", group.Name);
        }

        [TestMethod]
        public void AddNewFriend()
        {
            Group group = new Group("Voisco");
            group.AddNewFriend("general");
            group.Print_all_member();
            Assert.AreEqual("General", group.Friends[0].Name);
        }

        [TestMethod]
        public void DeleteFriend()
        {
            Group group = new Group("Voisco");
            group.AddNewFriend("general");
            group.AddNewFriend("king");
            group.Print_all_member();
            group.DeleteFriend(group.Friends.Find(x => x.Name.Equals("King")));
            group.Print_all_member();
            Assert.AreEqual("General", group.Friends[0].Name);
        }

        [TestMethod]
        public void AddNewPersonalDish()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("1")
                .Returns("Eggs")
                .Returns("55");
            Group group = new Group("Voisco", consoleMok.Object);
            group.AddNewFriend("general");
            group.AddNewFriend("king");
            group.Create_personal_dish();
            Assert.AreEqual("Eggs", group.Friends[0].dishes[0].Name);            
           
        }

        [TestMethod]
        public void AddNewComunalDish()
        {
            var consoleMok = new Mock<MyConsole>().As<IConsole>();
            consoleMok.SetupSequence(c => c.ReadLine())
                .Returns("Eggs")
                .Returns("55");
            Group group = new Group("Voisco", consoleMok.Object);
            group.AddNewFriend("general");
            group.AddNewFriend("king");
            group.Create_communal_dish();
            Assert.AreEqual("Eggs", group.Friends[0].dishes[0].Name);
            Assert.AreEqual("Eggs", group.Friends[1].dishes[0].Name);


        }



    }
}
