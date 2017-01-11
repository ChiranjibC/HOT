using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Contracts
{
    public interface IProduct
    {
        IEnumerable<ProductViewModel> GetProducts(int pageSize, int pageNo);
        ProductViewModel UpdateProduct(ProductViewModel product);
        ProductViewModel GetDetails(Guid id);
    }
}
