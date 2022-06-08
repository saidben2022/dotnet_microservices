using Catalog.Api.Models;
using Catalog.Api.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("Api/v1/[controller]")]
    public class CatalogController:ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly ILogger<CatalogController> logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            this.productRepository = productRepository;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await productRepository.GetAllAsync());
        }
        [HttpGet]

        [ProducesResponseType(typeof(IEnumerable<Product>), (int)StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)StatusCodes.Status404NotFound)]
        [Route("{id:length(24)}", Name = "GetProduct")]

        public async Task<ActionResult<Product>> GetProductByID(string id)
        {
            var result = await productRepository.GetProductById(id);
            if (result == null)
            {
                logger.LogError("Product not found with id:" + id);

                return NotFound();
            }
            else
                return Ok(result);
        }
        [HttpGet]

        [ProducesResponseType(typeof(IEnumerable<Product>), (int)StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)StatusCodes.Status404NotFound)]
        [Route("[action]/{name}", Name = "GetProductByName")]

        public async Task<ActionResult<Product>> GetProductByName(string name)
        {
            var result = await productRepository.GetProductByName(name);
            if (result == null)
            {
                logger.LogError("Product not found with name:" + name);

                return NotFound();
            }
            else
                return Ok(result);
        }
        [HttpGet]

        [ProducesResponseType(typeof(IEnumerable<Product>), (int)StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)StatusCodes.Status404NotFound)]
        [Route("[action]/{category}", Name = "GetProductByCategory")]

        public async Task<ActionResult<Product>> GetProductByCategory(string category)
        {
            var result = await productRepository.GetProductByCategory(category);
            if (result == null)
            {
                logger.LogError("Product not found with category:" + category);
                return NotFound();
            }
            else
                return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await productRepository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);

        }
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await productRepository.UpdateProduct(product));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await productRepository.DeleteProduct(id));
        }
    }
}
