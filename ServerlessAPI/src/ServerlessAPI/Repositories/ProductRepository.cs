using ServerlessAPI.Entities;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace ServerlessAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDynamoDBContext context;
        private readonly ILogger<ProductRepository> logger;

        public ProductRepository(IDynamoDBContext context, ILogger<ProductRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<IList<Product>> GetProductsAsync(int limit = 10)
        {
            //return a set of test products for testing
            return new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1", Price = 10.0m },
                new Product { Id = Guid.NewGuid(), Name = "Product 2", Price = 20.0m },
                new Product { Id = Guid.NewGuid(), Name = "Product 3", Price = 30.0m },
                new Product { Id = Guid.NewGuid(), Name = "Product 4", Price = 40.0m },
                new Product { Id = Guid.NewGuid(), Name = "Product 5", Price = 50.0m }
            };

            //its okay that this is unreachable code, as we are just testing the API
            var result = new List<Product>();

            try
            {
                if (limit <= 0)
                {
                    return result;
                }

                var filter = new ScanFilter();
                filter.AddCondition("Id", ScanOperator.IsNotNull);
                var scanConfig = new ScanOperationConfig()
                {
                    Limit = limit,
                    Filter = filter
                };
                var queryResult = context.FromScanAsync<Product>(scanConfig);

                do
                {
                    result.AddRange(await queryResult.GetNextSetAsync());
                }
                while (!queryResult.IsDone && result.Count < limit);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "fail to list books from DynamoDb Table");
                return new List<Product>();
            }

            return result;
        }

        public async Task DeleteAllProductsAsync()
        {
            try
            {
                var conditions = new List<ScanCondition>();

                var search = context.ScanAsync<Product>(conditions);
                var items = await search.GetNextSetAsync();

                foreach (var item in items)
                {
                    await context.DeleteAsync<Product>(item.Id);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "fail to clear products from DynamoDb Table");
            }
        }

        public async Task AddProductsAsync(IList<Entities.Product> products)
        {
            try
            {
                foreach (var product in products)
                {
                    await context.SaveAsync(product);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "fail to add products to DynamoDb Table");
            }
        }
    }
}
