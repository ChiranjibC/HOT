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
        private static List<ProductViewModel> _productList = null;
        public static List<ProductViewModel> productList
        {
            get
            { if (_productList == null)
                {
                    _productList = GetProducts(15).ToList();
                }
                return _productList;
            }
        }
        public IEnumerable<ProductViewModel> GetProducts(int pageSize, int pageNo)
        {
            return productList;
        }

        public ProductViewModel UpdateProduct(ProductViewModel product)
        {
            if(product.ProductId == Guid.Empty)
            {
                product.ProductId = Guid.NewGuid();
                productList.Add(product);
            }
            return product;
        }

        private static IEnumerable<ProductViewModel> GetProducts(int count)
        {
            var productLists = new List<ProductViewModel>();
            for (int i = 0; i < count; i++)
            {
                productLists.Add(GetProduct());
            }
            return productLists;
        }

        private static ProductViewModel GetProduct()
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
