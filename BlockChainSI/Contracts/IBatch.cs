using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Contracts
{
    public interface IBatch
    {
        IEnumerable<BatchViewModel> GetBatchItems(int pageSize, int pageNo);
        BatchViewModel UpdateBatch(BatchViewModel batch);
        BatchViewModel GetDetails(Guid id);
    }
}
