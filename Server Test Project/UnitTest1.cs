using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.modules;
using Server.services;

namespace Server_Test_Project
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCompetitionDifficulty()
        {
            int JumpHeight = 3;
            string JumpCode = "2,0,7,C";

            Jump j = new Jump();

            j = Server.services.JumpHelper.ParseDifficulty(JumpCode, JumpHeight);

            Assert.AreEqual(j.Name , "Back 3 1/2 Somersaults C");
            Assert.AreEqual(j.Difficulty , 3.6f);

        }


        [TestMethod]
        public void TestCryptoService()
        {
            string Password = "EdgeIsAGoodBrowser";

            User user1 = crypto.GenerateSaltHash(Password);
            User user2 = crypto.GenerateSaltHash(Password);


            Assert.AreNotEqual(user1.Salt, user2.Salt);
            Assert.AreNotEqual(user1.Hash, user2.Hash);
        }



    }
}
