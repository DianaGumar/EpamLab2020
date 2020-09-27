using Moq;
using NUnit.Framework;

namespace TicketManagement.UnitTests
{
    public class TestMoqTests
    {
        [Test]
        public void SomeTestMethod()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(a => a.GetComputerList()).Returns(new List<Computer>());
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }
    }
}
