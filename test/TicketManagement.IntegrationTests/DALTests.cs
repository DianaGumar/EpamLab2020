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
        public void ReedTest()
        {

            string str = @"Data Source=.\SQLEXPRESS;Initial Catalog=Example;Integrated Security=True";

            var repository = new Repository<Etable>(str);

            Etable et = repository.GetById(1);
            Etable e = new Etable() { id = 1, name = "alise", age = 15 };

            Assert.AreEqual(et, e);

        }

        [Test]
        public void ReedAllTest()
        {

            string str = @"Data Source=.\SQLEXPRESS;Initial Catalog=Example;Integrated Security=True";

            var repository = new Repository<Etable>(str);

            ICollection<Etable> et = (ICollection<Etable>)repository.GetAll();

            Assert.AreEqual(et.Count, 2);

        }

        [Test]
        public void CreateTest()
        {

            string str = @"Data Source=.\SQLEXPRESS;Initial Catalog=Example;Integrated Security=True";

            var repository = new Repository<Etable>(str);

            Etable e = new Etable() { name = "man", age = 22 };
            int i = repository.Create(e);

            Assert.AreEqual(i, 1);

        }

        [Test]
        public void UpdateTest()
        {

            string str = @"Data Source=.\SQLEXPRESS;Initial Catalog=Example;Integrated Security=True";

            var repository = new Repository<Etable>(str);

            Etable e = new Etable() { id = 3, name = "maaaan", age = 22 };
            int i = repository.Update(e);

            Assert.AreEqual(i, 1);

        }

        [Test]
        public void DeleteTest()
        {
            string str = @"Data Source=.\SQLEXPRESS;Initial Catalog=Example;Integrated Security=True";

            var repository = new Repository<Etable>(str);

            Etable e = new Etable { id = 1, name = "alise", age = 15 };
            int i = repository.Remove(e);

            Assert.AreEqual(i, 1);
        }
    }
}
