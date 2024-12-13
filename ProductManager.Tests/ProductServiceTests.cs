using Microsoft.EntityFrameworkCore;
using ProductManager.BLL.Services;
using ProductManager.DAL;
using ProductManager.DAL.Entities;


namespace ProductManager.Tests
{

    public class ProductServiceTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddProduct_ShouldAddProductSuccessfully()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            var productService = new ProductService(dbContext);
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 10.5m,
                Stock = 5
            };

            // Act
            await productService.AddProductAsync(product);
            var products = await dbContext.Products.ToListAsync();

            // Assert
            Assert.Single(products);
            Assert.Equal("Test Product", products.First().Name);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnProducts()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            dbContext.Products.Add(new Product { Name = "Product 1", Description = "Desc 1", Price = 5.0m, Stock = 10 });
            dbContext.Products.Add(new Product { Name = "Product 2", Description = "Desc 2", Price = 15.0m, Stock = 20 });
            await dbContext.SaveChangesAsync();

            var productService = new ProductService(dbContext);

            // Act
            var products = await productService.GetAllProductsAsync(null, 1, 10);

            // Assert
            Assert.Equal(2, products.Count());
        }

        [Fact]
        public async Task GetAllProducts_WithSearchQuery_ShouldReturnFilteredResults()
        {
            // Arrange
            var dbContext = GetInMemoryDbContext();
            dbContext.Products.Add(new Product { Name = "Laptop", Description = "High-end Laptop", Price = 1500, Stock = 5 });
            dbContext.Products.Add(new Product { Name = "Mouse", Description = "Wireless Mouse", Price = 20, Stock = 50 });
            await dbContext.SaveChangesAsync();

            var productService = new ProductService(dbContext);

            // Act
            var filteredProducts = await productService.GetAllProductsAsync("Laptop", 1, 10);

            // Assert
            Assert.Single(filteredProducts);
            Assert.Equal("Laptop", filteredProducts.First().Name);
        }
    }
}