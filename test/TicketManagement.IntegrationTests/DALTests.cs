using NUnit.Framework;
using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Model;

namespace TicketManagement.IntegrationTests
{

    // with real data base
    [TestFixture]
    public class DALTests
    {
        [Test]
        public void ReedTest(int a, int b)
        {

            string str = @"Data Source=.\SQLEXPRESS;Initial Catalog=Example;Integrated Security=True";

            var repository = new Repository<Etable>(str);

            Etable et = repository.GetById(1);
            Etable e = new Etable() { id = 1, name = "alise", age = 15 };

            Assert.AreSame(et, e);

            Assert.AreEqual(a, b);

        }




    }
}
