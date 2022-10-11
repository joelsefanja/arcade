namespace Arcade.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestTotDeMacht_4_5()
        {
            //Arrange
            int b = 4;
            int e = 5;
            int expected = 1024; // 4 ^ 5 == 1024 
            int actual;
            //Act
            actual = Program.TotDeMacht(b, e);
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}