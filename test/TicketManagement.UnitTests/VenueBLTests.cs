using NUnit.Framework;
using TicketManagement.BusinessLogic;
using TicketManagement.BusinessLogic.BusinessLogicLayer;

namespace TicketManagement.UnitTests
{
    public class VenueBLTests
    {
        private readonly string _str = @"Data Source=.\SQLEXPRESS;Initial Catalog=TicketManagement.Database;Integrated Security=True";

        [Test]
        public void DeleteLableTest()
        {
            IVenueService venueService = new VenueService(_str);
            ITMLayoutService tmlayoutService = new TMLayoutService(_str);
            IAreaService areaService = new AreaService(_str);
            ISeatService seatService = new SeatService(_str);

            VenueBL venueBl = new VenueBL(venueService, tmlayoutService, areaService, seatService);

            venueBl.RemoveLayout(26);

            Assert.AreEqual(1, 2);
        }




        public UnitTest1()
        {
            // create some mock products to play with
            IList<Product> products = new List<Product>
                {
                    new Product { ProductId = 1, Name = "C# Unleashed",
                        Description = "Short description here", Price = 49.99 },
                    new Product { ProductId = 2, Name = "ASP.Net Unleashed",
                        Description = "Short description here", Price = 59.99 },
                    new Product { ProductId = 3, Name = "Silverlight Unleashed",
                        Description = "Short description here", Price = 29.99 }
                };

            // Mock the Products Repository using Moq
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();

            // Return all the products
            mockProductRepository.Setup(mr => mr.FindAll()).Returns(products);

            // return a product by Id
            mockProductRepository.Setup(mr => mr.FindById(
                It.IsAny<int>())).Returns((int i) => products.Where(
                x => x.ProductId == i).Single());

            // return a product by Name
            mockProductRepository.Setup(mr => mr.FindByName(
                It.IsAny<string>())).Returns((string s) => products.Where(
                x => x.Name == s).Single());

            // Allows us to test saving a product
            mockProductRepository.Setup(mr => mr.Save(It.IsAny<Product>())).Returns(
                (Product target) =>
                {
                    DateTime now = DateTime.Now;

                    if (target.ProductId.Equals(default(int)))
                    {
                        target.DateCreated = now;
                        target.DateModified = now;
                        target.ProductId = products.Count() + 1;
                        products.Add(target);
                    }
                    else
                    {
                        var original = products.Where(
                            q => q.ProductId == target.ProductId).Single();

                        if (original == null)
                        {
                            return false;
                        }

                        original.Name = target.Name;
                        original.Price = target.Price;
                        original.Description = target.Description;
                        original.DateModified = now;
                    }

                    return true;
                });

            // Complete the setup of our Mock Product Repository
            this.MockProductsRepository = mockProductRepository.Object;
        }

        // Gets or sets the test context which provides
        // information about and functionality for the current test run.
        public TestContext TestContext { get; set; }

        public readonly IProductRepository MockProductsRepository;

        [TestMethod]
        public void CanReturnProductById()
        {
            // Try finding a product by id
            Product testProduct = this.MockProductsRepository.FindById(2);

            Assert.IsNotNull(testProduct); // Test if null
            Assert.IsInstanceOfType(testProduct, typeof(Product)); // Test type
            Assert.AreEqual("ASP.Net Unleashed", testProduct.Name); // Verify it is the right product
        }

        [TestMethod]
        public void CanReturnProductByName()
        {
            // Try finding a product by Name
            Product testProduct = this.MockProductsRepository.FindByName("Silverlight Unleashed");

            Assert.IsNotNull(testProduct); // Test if null
            Assert.IsInstanceOfType(testProduct, typeof(Product)); // Test type
            Assert.AreEqual(3, testProduct.ProductId); // Verify it is the right product
        }

        [TestMethod]
        public void CanReturnAllProducts()
        {
            // Try finding all products
            IList<Product> testProducts = this.MockProductsRepository.FindAll();

            Assert.IsNotNull(testProducts); // Test if null
            Assert.AreEqual(3, testProducts.Count); // Verify the correct Number
        }

        [TestMethod]
        public void CanInsertProduct()
        {
            // Create a new product, not I do not supply an id
            Product newProduct = new Product
            { Name = "Pro C#", Description = "Short description here", Price = 39.99 };

            int productCount = this.MockProductsRepository.FindAll().Count;
            Assert.AreEqual(3, productCount); // Verify the expected Number pre-insert

            // try saving our new product
            this.MockProductsRepository.Save(newProduct);

            // demand a recount
            productCount = this.MockProductsRepository.FindAll().Count;
            Assert.AreEqual(4, productCount); // Verify the expected Number post-insert

            // verify that our new product has been saved
            Product testProduct = this.MockProductsRepository.FindByName("Pro C#");
            Assert.IsNotNull(testProduct); // Test if null
            Assert.IsInstanceOfType(testProduct, typeof(Product)); // Test type
            Assert.AreEqual(4, testProduct.ProductId); // Verify it has the expected productid
        }

        [TestMethod]
        public void CanUpdateProduct()
        {
            // Find a product by id
            Product testProduct = this.MockProductsRepository.FindById(1);

            // Change one of its properties
            testProduct.Name = "C# 3.5 Unleashed";

            // Save our changes.
            this.MockProductsRepository.Save(testProduct);

            // Verify the change
            Assert.AreEqual("C# 3.5 Unleashed", this.MockProductsRepository.FindById(1).Name);
        }




    }
}
