using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.modules;

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
            Assert.AreEqual((j.Difficulty , 3.6f);

        }
    }
}
