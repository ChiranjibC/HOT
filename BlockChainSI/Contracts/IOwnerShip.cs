using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Contracts
{
    public interface IOwnership
    {
        IList<BatchOwnershipHistoryViewModel> GetOwnerShipDetails(int pageSize, int pageNo);
        IList<BatchOwnershipHistoryViewModel> GetOwnerShipDetails(int pageSize, int pageNo, string batchId);
        string Update(BatchOwnershipHistoryViewModel ownerShipItem);
        BatchOwnershipHistoryViewModel GetDetails(string batchId);
    }
}
