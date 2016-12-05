using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChainSI.Models;

namespace BlockChainSI.Mock
{
    public class MockProduct : MockData, IProduct
    {
        public IEnumerable<ProductViewModel> GetProducts(int pageSize, int pageNo)
        {
            return GetProducts(pageSize);
        }

        public ProductViewModel UpdateProduct(ProductViewModel product)
        {
            product.ProductId = Guid.NewGuid();
            return product;
        }

        private IEnumerable<ProductViewModel> GetProducts(int count)
        {
            var productLists = new List<ProductViewModel>();
            for (int i = 0; i < count; i++)
            {
                productLists.Add(GetProduct());
            }
            return productLists;
        }

        private ProductViewModel GetProduct()
        {
            var product = new ProductViewModel()
            {
                ProductDesc = "Product details_" + GetRand(),
                ProductId = Guid.NewGuid(),
            };
            return product;
        }        
    }
}
